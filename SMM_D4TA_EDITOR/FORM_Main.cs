using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SMM_D4TA_EDITOR.SMM1FileFormats;

namespace SMM_D4TA_EDITOR
{
    public partial class FORM_Main : BaseForm
    {
        public FORM_Main()
        {
            InitializeComponent();
        }

        DataTable MiiData_XML;
        string tmpMiiBase64;

        private string currentFilePath = "";

        private void FORM_Main_Load(object sender, EventArgs e)
        {
            LanguageManager.ApplyToContainer(this, "FORM_Main");
            ComboBox_Physics_Settings.Items.Clear();
            ComboBox_Physics_Settings.Items.AddRange(LanguageManager.GetList("ComboBox_Physics").ToArray());
            ComboBox_Theme_Settings.Items.AddRange(LanguageManager.GetList("ComboBox_Theme").ToArray());
            ComboBox_Scroll_Settings.Items.AddRange(LanguageManager.GetList("ComboBox_Scroll").ToArray());
            ComboBox_OfficialCourse.Items.AddRange(LanguageManager.GetList("ComboBox_OfficialCourse").ToArray());
            LoadComboSelectMii();

            Activate();
        }

        public void LoadComboSelectMii()
        {
            ComboBox_SelectMii.Items.Clear();
            ComboBox_SelectMii.Items.AddRange(LanguageManager.GetList("ComboBox_SelectMii").ToArray());
            ComboBox_SelectMii.SelectedIndex = 0;

            MiiData_XML = new DataTable("MiiData");
            MiiData_XML.Columns.Add("SaveName");
            MiiData_XML.Columns.Add("MiiBase64");
            MiiData_XML.Columns.Add("CountryID");
            MiiData_XML.ReadXml("Data.xml");

            for (int i = 0, loopTo = MiiData_XML.Rows.Count - 1; i <= loopTo; i++)
            ComboBox_SelectMii.Items.Add(MiiData_XML.Rows[i][0]);
        }

        private void ToolStripMenuItem_BYML_To_XML_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog_BYML_To_XML.ShowDialog() == DialogResult.OK
            && SaveFileDialog_BYML_To_XML.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveFileDialog_BYML_To_XML.FileName,
                BymlConverter.GetXml(File.ReadAllBytes(OpenFileDialog_BYML_To_XML.FileName)), Encoding.GetEncoding("Shift-JIS"));
            }
        }

        private void ToolStripMenuItem_XML_To_BYML_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog_XML_To_BYML.ShowDialog() == DialogResult.OK
            && SaveFileDialog_XML_To_BYML.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(SaveFileDialog_XML_To_BYML.FileName,
                BymlConverter.GetByml(File.ReadAllText(OpenFileDialog_XML_To_BYML.FileName, Encoding.GetEncoding("Shift-JIS"))));
            }
        }

        private void ToolStripMenuItem_IMAGE_To_TNL_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog_IMAGE_To_TNL.ShowDialog() == DialogResult.OK)
            {
                //Load in memory as BitMap
                Bitmap originalBmp = new Bitmap(OpenFileDialog_IMAGE_To_TNL.FileName);

                bool validResolution = (originalBmp.Width == 320 && originalBmp.Height == 240)
                || (originalBmp.Width <= 720 && originalBmp.Height == 80);

                if (!validResolution) //Validate resolution
                {
                    string text = LanguageManager.Get("FORM_Main", "msgInvalidResolution");
                    MessageBox.Show(text);
                    return;
                }

                //Remove PNG Alpha channel
                Bitmap bmp = new Bitmap(originalBmp.Width, originalBmp.Height, PixelFormat.Format24bppRgb);
                using (Graphics graphics = Graphics.FromImage(bmp)) { graphics.DrawImage(originalBmp, 0, 0, bmp.Width, bmp.Height); }
                originalBmp.Dispose(); //Reserve memory

                //Read original bytes
                byte[] ImageBytes = RecompressJpeg(bmp, 100);

                if (ImageBytes.Length > 0xC7F8)
                {
                    bool compressedSuccessfully = false;

                    for (long quality = 80; quality >= 30; quality -= 10)
                    {
                        ImageBytes = RecompressJpeg(bmp, quality);
                        if (ImageBytes.Length <= 0xC7F8)
                        {
                            compressedSuccessfully = true;
                            break;
                        }
                    }

                    if (!compressedSuccessfully)
                    {
                        string text = LanguageManager.Get("FORM_Main", "msgTooLargeForCompression");
                        MessageBox.Show(text);
                        return;
                    }
                }

                if (SaveFileDialog_IMAGE_To_TNL.ShowDialog() == DialogResult.OK)
                {
                    byte[] ImageLengthBytes = BitConverter.GetBytes(ImageBytes.Length);
                    Array.Reverse(ImageLengthBytes); //Big endian order

                    int totalSize = 0xC800; //51200 Bytes
                    int payloadSize = 4 + ImageBytes.Length + (totalSize - 8 - ImageBytes.Length);
                    byte[] payload = new byte[payloadSize];

                    //Insert [size (4 bytes)] + [jpeg]
                    Array.Copy(ImageLengthBytes, 0, payload, 0, 4);
                    Array.Copy(ImageBytes, 0, payload, 4, ImageBytes.Length);
                    //The rest of payload is already zero-initialized (padding)

                    //CRC32
                    Crc32 crc32 = new Crc32();
                    byte[] crc = crc32.ComputeChecksumBytes(payload, 0, payload.Length);
                    Array.Reverse(crc); //Big endian order

                    //Create TNL
                    byte[] tnlData = new byte[crc.Length + payload.Length];
                    Array.Copy(crc, 0, tnlData, 0, crc.Length);
                    Array.Copy(payload, 0, tnlData, crc.Length, payload.Length);

                    File.WriteAllBytes(SaveFileDialog_IMAGE_To_TNL.FileName, tnlData);
                    string TNLcreated = LanguageManager.Get("FORM_Main", "msgTNLcreated");
                    MessageBox.Show(TNLcreated);
                }
            }
        }

        private void ToolStripMenuItem_TNL_To_IMAGE_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog_TNL_To_IMAGE.ShowDialog() == DialogResult.OK)
            {
                byte[] tnlBytes = File.ReadAllBytes(OpenFileDialog_TNL_To_IMAGE.FileName);

                if (tnlBytes.Length < 12)
                {
                    string text = LanguageManager.Get("FORM_Main", "msgTNLtooShort");
                    MessageBox.Show(text);
                    return;
                }

                //Get JPEG length (offsets 0x04 to 0x07)
                byte[] jpegLengthBytes = new byte[4];
                Array.Copy(tnlBytes, 4, jpegLengthBytes, 0, 4);
                Array.Reverse(jpegLengthBytes); //Little endian order
                int jpegLength = BitConverter.ToInt32(jpegLengthBytes, 0);

                if (jpegLength <= 0 || jpegLength > 0xC7F8 || tnlBytes.Length < 8 + jpegLength)
                {
                    string text = LanguageManager.Get("FORM_Main", "msgInvalidJPEG");
                    MessageBox.Show(text);
                    return;
                }

                byte[] jpegBytes = new byte[jpegLength];
                Array.Copy(tnlBytes, 8, jpegBytes, 0, jpegLength);

                if (SaveFileDialog_TNL_To_IMAGE.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(SaveFileDialog_TNL_To_IMAGE.FileName, jpegBytes);
                    string JPEGextracted = LanguageManager.Get("FORM_Main", "msgJPEGextracted");
                    MessageBox.Show(JPEGextracted);
                }
            }
        }

        private void BUTTON_TimerMinimum_Click(object sender, EventArgs e)
        {
            NUMERIC_CourseTimer.Value = 0;
        }

        private void BUTTON_TimerMaximum_Click(object sender, EventArgs e)
        {
            NUMERIC_CourseTimer.Value = 65535;
        }
        
        //I'm going to use this in ComboBox value changed function, so for now will be global
        byte[] tmpfileBytes;

        private void ToolStripMenuItem_SelectFile_Click(object sender, EventArgs e)
        {
            ushort CourseScroll = 0;
            ushort CourseUpdatePhysics = 0;
            ushort CourseTheme = 0;
            string CourseStyle = "";

            string CourseName = "";
            string CourseCreator = "";
            string CourseID = "";
            ushort CourseCountry = 0;
            ushort CourseTimer = 0;
            ushort CourseLength = 0;

            ushort CourseDateYear = 0;
            ushort CourseDateMonth = 0;
            ushort CourseDateDay = 0;
            ushort CourseDateHour = 0;
            ushort CourseDateMinute = 0;

            string LastItemPlaced = "";
            string LastSFXplaced = "";

            if (OpenFileDialog_cdtFile.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = OpenFileDialog_cdtFile.FileName;
                tmpfileBytes = File.ReadAllBytes(currentFilePath);

                LoadComboSelectMii();
                ReadSMM1Course(ref tmpfileBytes,
                ref CourseDateYear, ref CourseDateMonth, ref CourseDateDay, ref CourseDateHour, ref CourseDateMinute,
                ref CourseUpdatePhysics,
                ref CourseID, ref CourseName, ref CourseStyle,
                ref CourseTheme, ref CourseTimer, ref CourseScroll, ref CourseLength,
                ref CourseCreator, ref CourseCountry,
                ref LastItemPlaced, ref LastSFXplaced);
                UIstate(true);

                LABEL_LastItemPlaced.Text = LastItemPlaced;
                LABEL_LastSFXplaced.Text = LastSFXplaced;

                if (CourseScroll == 0) ComboBox_Scroll_Settings.SelectedIndex = 0;
                else if (CourseScroll == 1) ComboBox_Scroll_Settings.SelectedIndex = 1;
                else if (CourseScroll == 2) ComboBox_Scroll_Settings.SelectedIndex = 2;
                else if (CourseScroll == 3) ComboBox_Scroll_Settings.SelectedIndex = 3;
                else if (CourseScroll == 4) ComboBox_Scroll_Settings.SelectedIndex = 4;
                else CourseScroll = 0;

                if (CourseUpdatePhysics == 0) ComboBox_Physics_Settings.SelectedIndex = 0;
                else if (CourseUpdatePhysics == 1) ComboBox_Physics_Settings.SelectedIndex = 1;
                else if (CourseUpdatePhysics == 2) ComboBox_Physics_Settings.SelectedIndex = 2;
                else if (CourseUpdatePhysics == 3) ComboBox_Physics_Settings.SelectedIndex = 3;
                else if (CourseUpdatePhysics == 4) ComboBox_Physics_Settings.SelectedIndex = 4;
                else if (CourseUpdatePhysics == 5) ComboBox_Physics_Settings.SelectedIndex = 5;
                else if (CourseUpdatePhysics == 6) ComboBox_Physics_Settings.SelectedIndex = 6;
                else if (CourseUpdatePhysics == 7) ComboBox_Physics_Settings.SelectedIndex = 7;
                else CourseUpdatePhysics = 0;

                if (CourseTheme == 0) ComboBox_Theme_Settings.SelectedIndex = 0;
                else if (CourseTheme == 1) ComboBox_Theme_Settings.SelectedIndex = 1;
                else if (CourseTheme == 2) ComboBox_Theme_Settings.SelectedIndex = 2;
                else if (CourseTheme == 3) ComboBox_Theme_Settings.SelectedIndex = 3;
                else if (CourseTheme == 4) ComboBox_Theme_Settings.SelectedIndex = 4;
                else if (CourseTheme == 5) ComboBox_Theme_Settings.SelectedIndex = 5;
                else CourseTheme = 0;

                if (CourseStyle == "M1") ComboBox_Style_Settings.SelectedIndex = 0; 
                else if (CourseStyle == "M3") ComboBox_Style_Settings.SelectedIndex = 1; 
                else if (CourseStyle == "MW") ComboBox_Style_Settings.SelectedIndex = 2; 
                else if (CourseStyle == "WU") ComboBox_Style_Settings.SelectedIndex = 3; 
                else CourseStyle = "M1";

                string clearCheckStatus0 = LanguageManager.Get("FORM_Main", "ClearCheckStatus0");
                string clearCheckStatus1 = LanguageManager.Get("FORM_Main", "ClearCheckStatus1");
                if (tmpfileBytes[ClearCheckOffset] == 0x01) LABEL_ClearCheckStatus.Text = clearCheckStatus1;
                else LABEL_ClearCheckStatus.Text = clearCheckStatus0;

                //This course status section used to be for only read, but not for write at all
                //The problem was basically every status is a completely different byte offset
                //Every of them from 00 to 01 to set status (except OfficialCourse), probably not very efficient but works in the game
                //I don't know yet what would happen if I set all of these bytes offsets to 01 at the same time
                //EDIT: You can overlap every status and works in the game
                if (tmpfileBytes[OfficialCourseStatusOffset] == 1) ComboBox_OfficialCourse.SelectedIndex = 1;
                else if (tmpfileBytes[OfficialCourseStatusOffset] == 2) ComboBox_OfficialCourse.SelectedIndex = 2;
                else if (tmpfileBytes[OfficialCourseStatusOffset] == 3) ComboBox_OfficialCourse.SelectedIndex = 3;
                else if (tmpfileBytes[OfficialCourseStatusOffset] == 4) ComboBox_OfficialCourse.SelectedIndex = 4;
                else if (tmpfileBytes[OfficialCourseStatusOffset] == 5) ComboBox_OfficialCourse.SelectedIndex = 5;
                else if (tmpfileBytes[OfficialCourseStatusOffset] == 6) ComboBox_OfficialCourse.SelectedIndex = 6;
                else if (tmpfileBytes[OfficialCourseStatusOffset] == 7) ComboBox_OfficialCourse.SelectedIndex = 7;
                else if (tmpfileBytes[OfficialCourseStatusOffset] == 8) ComboBox_OfficialCourse.SelectedIndex = 8;
                else if (tmpfileBytes[OfficialCourseStatusOffset] == 0x1D) ComboBox_OfficialCourse.SelectedIndex = 9;
                else ComboBox_OfficialCourse.SelectedIndex = 0;

                if (tmpfileBytes[DownloadedCourseOffset] == 0x01) CHECK_CourseStatusDownloaded.Checked = true;
                else CHECK_CourseStatusDownloaded.Checked = false;
                if (tmpfileBytes[UploadedCourseOffset] == 0x01) CHECK_CourseStatusUploaded.Checked = true;
                else CHECK_CourseStatusUploaded.Checked = false;
                if (tmpfileBytes[RemovedCourseOffset] == 0x01) CHECK_CourseStatusRemoved.Checked = true;
                else CHECK_CourseStatusRemoved.Checked = false;

                TB_CourseName.Text = CourseName;
                TB_CourseCreator.Text = CourseCreator;
                TB_CourseIDprefix.Text = CourseID.Substring(0, 4);
                TB_CourseIDsuffix1.Text = CourseID.Substring(4, 4);
                TB_CourseIDsuffix2.Text = CourseID.Substring(8, 4);
                TB_CourseIDsuffix3.Text = CourseID.Substring(12, 4);
                NUMERIC_CountryCode.Value = CourseCountry;
                NUMERIC_CourseTimer.Value = CourseTimer;
                NUMERIC_Length.Value = CourseLength;
                LABEL_CourseLengthDisplay.Text = $"0x{CourseLength:X3}";
                NUMERIC_CourseYear.Value = CourseDateYear;
                NUMERIC_CourseMonth.Value = CourseDateMonth;
                NUMERIC_CourseDay.Value = CourseDateDay;
                NUMERIC_CourseHour.Value = CourseDateHour;
                NUMERIC_CourseMinute.Value = CourseDateMinute;
            }
        }

        private void BUTTON_Cancel_Click(object sender, EventArgs e)
        {
            UIstate(false);
        }

        private void BUTTON_SaveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath)) return;

            //Set file path and read data
            byte[] fileBytes = File.ReadAllBytes(currentFilePath);

            //This writes the 6 suffix bytes for course ID
            byte[] NewIDsuffix1Bytes = new byte[2];
            byte[] NewIDsuffix2Bytes = new byte[2];
            byte[] NewIDsuffix3Bytes = new byte[2];
            int.TryParse(TB_CourseIDsuffix1.Text, System.Globalization.NumberStyles.HexNumber, null, out int CourseIDsuffix1);
            int.TryParse(TB_CourseIDsuffix2.Text, System.Globalization.NumberStyles.HexNumber, null, out int CourseIDsuffix2);
            int.TryParse(TB_CourseIDsuffix3.Text, System.Globalization.NumberStyles.HexNumber, null, out int CourseIDsuffix3);
            //I think is easier right now to add the offsets manually for this specifically
            fileBytes[CourseIDsuffixStartOffset] = (byte)(CourseIDsuffix1 >> 8);
            fileBytes[0x1B] = (byte)(CourseIDsuffix1 & 0xFF);
            fileBytes[0x1C] = (byte)(CourseIDsuffix2 >> 8);
            fileBytes[0x1D] = (byte)(CourseIDsuffix2 & 0xFF);
            fileBytes[0x1E] = (byte)(CourseIDsuffix3 >> 8);
            fileBytes[CourseIDsuffixEndOffset] = (byte)(CourseIDsuffix3 & 0xFF);

            //To write a new course name correctly, I'm doing in reverse whatever I did to read
            int NewCourseNameBytesLength = CourseNameEndOffset - CourseNameStartOffset + 1;
            char[] charArray = TB_CourseName.Text.ToArray();
            Array.Reverse(charArray);
            byte[] NewCourseNameBytes = new byte[NewCourseNameBytesLength];
            NewCourseNameBytes = Encoding.Unicode.GetBytes(charArray);
            Array.Reverse(NewCourseNameBytes);
            //Create a 64 bytes array filled with zeros
            byte[] paddedNameBytes = new byte[64]; //64 bytes (32 * 2)
            Array.Clear(paddedNameBytes, 0, 64);
            //Copy course name bytes to beginning of array
            Array.Copy(NewCourseNameBytes, paddedNameBytes, NewCourseNameBytes.Length);
            //Insert those bytes to the file
            Array.Copy(paddedNameBytes, 0, fileBytes, CourseNameStartOffset, 64);

            byte[] MiiFileBytes = Convert.FromBase64String(tmpMiiBase64);
            Array.Copy(MiiFileBytes, 0, fileBytes, CourseMiiOffset, CourseMiiSize); //Creator Mii writing instead of creator name

            ushort NewCourseTimer = (ushort)NUMERIC_CourseTimer.Value;
            fileBytes[CourseTimerStartOffset] = (byte)(NewCourseTimer >> 8); //MSB
            fileBytes[CourseTimerEndOffset] = (byte)(NewCourseTimer & 0xFF); //LSB

            ushort NewCourseLength = (ushort)NUMERIC_Length.Value;
            fileBytes[CourseLengthStartOffset] = (byte)(NewCourseLength >> 8); //MSB
            fileBytes[CourseLengthEndOffset] = (byte)(NewCourseLength & 0xFF); //LSB

            ushort NewCourseCountry = (ushort)NUMERIC_CountryCode.Value;
            fileBytes[CourseCountryOffset] = (byte)(NewCourseCountry);

            ushort NewCourseDateYear = (ushort)NUMERIC_CourseYear.Value;
            fileBytes[CourseDateYearStartOffset] = (byte)(NewCourseDateYear >> 8);
            fileBytes[CourseDateYearEndOffset] = (byte)(NewCourseDateYear & 0xFF);

            ushort NewCourseDateMonth = (ushort)NUMERIC_CourseMonth.Value;
            fileBytes[CourseDateMonthOffset] = (byte)(NewCourseDateMonth);

            ushort NewCourseDateDay = (ushort)NUMERIC_CourseDay.Value;
            fileBytes[CourseDateDayOffset] = (byte)(NewCourseDateDay);

            ushort NewCourseDateHour = (ushort)NUMERIC_CourseHour.Value;
            fileBytes[CourseDateHourOffset] = (byte)(NewCourseDateHour);

            ushort NewCourseDateMinute = (ushort)NUMERIC_CourseMinute.Value;
            fileBytes[CourseDateMinuteOffset] = (byte)(NewCourseDateMinute);

            if (CHECK_UploadReady.Checked)
            {
                fileBytes[ClearCheckOffset] = 0x01;
            }

            byte physicsValue = 0;
            if (ComboBox_Physics_Settings.SelectedIndex == 1) physicsValue = 1;
            else if (ComboBox_Physics_Settings.SelectedIndex == 2) physicsValue = 2;
            else if (ComboBox_Physics_Settings.SelectedIndex == 3) physicsValue = 3;
            else if (ComboBox_Physics_Settings.SelectedIndex == 4) physicsValue = 4;
            else if (ComboBox_Physics_Settings.SelectedIndex == 5) physicsValue = 5;
            else if (ComboBox_Physics_Settings.SelectedIndex == 6) physicsValue = 6;
            else if (ComboBox_Physics_Settings.SelectedIndex == 7) physicsValue = 7;
            else physicsValue = 0;
            //Insert physics byte value to the file
            fileBytes[CourseUpdatePhysicsOffset] = physicsValue;

            byte scrollValue = 0;
            if (ComboBox_Scroll_Settings.SelectedIndex == 1) scrollValue = 1;
            else if (ComboBox_Scroll_Settings.SelectedIndex == 2) scrollValue = 2;
            else if (ComboBox_Scroll_Settings.SelectedIndex == 3) scrollValue = 3;
            else if (ComboBox_Scroll_Settings.SelectedIndex == 4) scrollValue = 4;
            else scrollValue = 0;
            //Insert scroll byte value to the file
            fileBytes[CourseScrollSettingsOffset] = scrollValue;

            byte themeValue = 0;
            if (ComboBox_Theme_Settings.SelectedIndex == 1) themeValue = 1;
            else if (ComboBox_Theme_Settings.SelectedIndex == 2) themeValue = 2;
            else if (ComboBox_Theme_Settings.SelectedIndex == 3) themeValue = 3;
            else if (ComboBox_Theme_Settings.SelectedIndex == 4) themeValue = 4;
            else if (ComboBox_Theme_Settings.SelectedIndex == 5) themeValue = 5;
            else themeValue = 0;
            //Insert theme byte value to the file
            fileBytes[CourseThemeOffset] = themeValue;

            string styleValue;

            if (ComboBox_Style_Settings.SelectedIndex == 0) styleValue = "M1";
            else if (ComboBox_Style_Settings.SelectedIndex == 1) styleValue = "M3";
            else if (ComboBox_Style_Settings.SelectedIndex == 2) styleValue = "MW";
            else if (ComboBox_Style_Settings.SelectedIndex == 3) styleValue = "WU";
            else styleValue = "M1";
            //Insert style byte value to the file
            byte[] styleBytes = Encoding.ASCII.GetBytes(styleValue);
            fileBytes[CourseStyleStartOffset] = styleBytes[0];
            fileBytes[CourseStyleEndOffset] = styleBytes[1];

            if (ComboBox_OfficialCourse.SelectedIndex == 1) fileBytes[OfficialCourseStatusOffset] = 1;
            else if (ComboBox_OfficialCourse.SelectedIndex == 2) fileBytes[OfficialCourseStatusOffset] = 2;
            else if(ComboBox_OfficialCourse.SelectedIndex == 3) fileBytes[OfficialCourseStatusOffset] = 3;
            else if(ComboBox_OfficialCourse.SelectedIndex == 4) fileBytes[OfficialCourseStatusOffset] = 4;
            else if(ComboBox_OfficialCourse.SelectedIndex == 5) fileBytes[OfficialCourseStatusOffset] = 5;
            else if(ComboBox_OfficialCourse.SelectedIndex == 6) fileBytes[OfficialCourseStatusOffset] = 6;
            else if(ComboBox_OfficialCourse.SelectedIndex == 7) fileBytes[OfficialCourseStatusOffset] = 7;
            else if(ComboBox_OfficialCourse.SelectedIndex == 8) fileBytes[OfficialCourseStatusOffset] = 8;
            else if(ComboBox_OfficialCourse.SelectedIndex == 9) fileBytes[OfficialCourseStatusOffset] = 0x1D;
            else fileBytes[OfficialCourseStatusOffset] = 0x0;

            if (CHECK_CourseStatusDownloaded.Checked) fileBytes[DownloadedCourseOffset] = 0x01;
            else fileBytes[DownloadedCourseOffset] = 0x00;
            if (CHECK_CourseStatusUploaded.Checked) fileBytes[UploadedCourseOffset] = 0x01;
            else fileBytes[UploadedCourseOffset] = 0x00;
            if (CHECK_CourseStatusRemoved.Checked) fileBytes[RemovedCourseOffset] = 0x01; //Here used to be an "UploadedCourseOffset" instead of "RemovedCourseOffset", hopefully I'm testing it to see if there's any bug
            else fileBytes[RemovedCourseOffset] = 0x00;

            //Calculate and write the 4 bytes CRC-32 checksum on offsets from 0x08 to 0x0B
            Crc32 crc32 = new Crc32();
            byte[] checksum = crc32.ComputeChecksumBytes(fileBytes, 0x10, fileBytes.Length - 0x10);
            Array.Reverse(checksum); //Parse to big-endian order
            Array.Copy(checksum, 0, fileBytes, 0x08, 4);

            //Save and overwrites .cdt file
            File.WriteAllBytes(currentFilePath, fileBytes);
            string caption = LanguageManager.Get("FORM_Main", "cdtFileSaveTitle");
            string text = LanguageManager.Get("FORM_Main", "cdtFileSave");
            MessageBox.Show(text + currentFilePath, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            UIstate(false);
        }

        private void UIstate(bool state)
        {
            TB_CourseName.Enabled = state;
            TB_CourseCreator.Enabled = state;
            TB_CourseIDprefix.Enabled = state;
            TB_CourseIDsuffix1.Enabled = state;
            TB_CourseIDsuffix2.Enabled = state;
            TB_CourseIDsuffix3.Enabled = state;
            NUMERIC_CountryCode.Enabled = state;
            NUMERIC_CourseTimer.Enabled = state;
            NUMERIC_Length.Enabled = state;
            NUMERIC_CourseYear.Enabled = state;
            NUMERIC_CourseMonth.Enabled = state;
            NUMERIC_CourseDay.Enabled = state;
            NUMERIC_CourseHour.Enabled = state;
            NUMERIC_CourseMinute.Enabled = state;
            CHECK_SetDateTimeNow.Enabled = state;
            CHECK_UploadReady.Enabled = state;
            ComboBox_Physics_Settings.Enabled = state;
            ComboBox_Style_Settings.Enabled = state;
            ComboBox_Theme_Settings.Enabled = state;
            ComboBox_Scroll_Settings.Enabled = state;
            ComboBox_OfficialCourse.Enabled = state;
            ComboBox_SelectMii.Enabled = state;
            BUTTON_ExtractMii.Enabled = state;
            BUTTON_TimerMinimum.Enabled = state;
            BUTTON_TimerMaximum.Enabled = state;
            CHECK_CourseStatusDownloaded.Enabled = state;
            CHECK_CourseStatusUploaded.Enabled = state;
            CHECK_CourseStatusRemoved.Enabled = state;
            BUTTON_Cancel.Enabled = state;
            BUTTON_SaveFile.Enabled = state;
            BUTTON_CopyID.Enabled = state;

            if (state == false)
            {
                currentFilePath = "";
                TB_CourseName.Text = "";
                TB_CourseCreator.Text = "";
                TB_CourseIDprefix.Text = "";
                TB_CourseIDsuffix1.Text = "";
                TB_CourseIDsuffix2.Text = "";
                TB_CourseIDsuffix3.Text = "";
                LABEL_LastItemPlaced.Text = LanguageManager.Get("FORM_Main", "LABEL_LastItemPlaced");
                LABEL_LastSFXplaced.Text = LanguageManager.Get("FORM_Main", "LABEL_LastSFXplaced");
                NUMERIC_CountryCode.Value = 0;
                NUMERIC_CourseTimer.Value = 0;
                NUMERIC_CourseYear.Value = 0;
                NUMERIC_CourseMonth.Value = 0;
                NUMERIC_CourseDay.Value = 0;
                NUMERIC_CourseHour.Value = 0;
                NUMERIC_CourseMinute.Value = 0;
                NUMERIC_Length.Value = 384;
                LABEL_CourseLengthDisplay.Text = "0x180";
                //ComboBox_Physics_Settings.Text = ""; //I don't have idea why this doesn't cleans the text, so I commented it
                //ComboBox_Style_Settings.Text = "";
                //ComboBox_Theme_Settings.Text = "";
                //ComboBox_Scroll_Settings.Text = "";
                //ComboBox_OfficialCourse.Text = "";
                //ComboBox_SelectMii.Text = "";
                CHECK_SetDateTimeNow.Checked = state;
                CHECK_UploadReady.Checked = state;
                CHECK_CourseStatusDownloaded.Checked = state;
                CHECK_CourseStatusUploaded.Checked = state;
                CHECK_CourseStatusRemoved.Checked = state;
            }
        }

        public byte[] RecompressJpeg(Bitmap image, long quality)
        {
            using (var ms = new MemoryStream())
            {
                ImageCodecInfo jpgEncoder = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);

                EncoderParameters encParams = new EncoderParameters(1);
                encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                image.Save(ms, jpgEncoder, encParams);
                return ms.ToArray();
            }
        }

        private void CHECK_SetDateTimeNow_CheckedChanged(object sender, EventArgs e)
        {
            if (CHECK_SetDateTimeNow.Checked)
            {
                NUMERIC_CourseMonth.Enabled = false;
                NUMERIC_CourseDay.Enabled = false;
                NUMERIC_CourseHour.Enabled = false;
                NUMERIC_CourseMinute.Enabled = false;

                NUMERIC_CourseMonth.Value = DateTime.Now.Month;
                NUMERIC_CourseDay.Value = DateTime.Now.Day;
                NUMERIC_CourseHour.Value = DateTime.Now.Hour;
                NUMERIC_CourseMinute.Value = DateTime.Now.Minute;
            }
            else
            {
                //I literally copy and pasted some of the same lines from select file section, so... Later I'll create a function or something
                //November 14th 2025: I created the function, but could be better
                //May 5th 2026: What is this??? I'm not going to optimize this right now

                //Set file path and read data
                currentFilePath = OpenFileDialog_cdtFile.FileName;
                byte[] fileBytes = File.ReadAllBytes(currentFilePath);

                //Extract date month bytes offset 0x12)
                ushort CourseDateMonth = ExctractBytesFromOffset(fileBytes, CourseDateMonthOffset);

                //Extract date day bytes offset 0x13)
                ushort CourseDateDay = ExctractBytesFromOffset(fileBytes, CourseDateDayOffset);

                //Extract date hour bytes offset 0x14)
                ushort CourseDateHour = ExctractBytesFromOffset(fileBytes, CourseDateHourOffset);

                //Extract date minute bytes offset 0x15)
                ushort CourseDateMinute = ExctractBytesFromOffset(fileBytes, CourseDateMinuteOffset);

                NUMERIC_CourseMonth.Enabled = true;
                NUMERIC_CourseDay.Enabled = true;
                NUMERIC_CourseHour.Enabled = true;
                NUMERIC_CourseMinute.Enabled = true;

                NUMERIC_CourseMonth.Value = CourseDateMonth;
                NUMERIC_CourseDay.Value = CourseDateDay;
                NUMERIC_CourseHour.Value = CourseDateHour;
                NUMERIC_CourseMinute.Value = CourseDateMinute;
            }
        }

        private void BUTTON_CopyID_Click(object sender, EventArgs e)
        {
            //You can copy only if ID is fully filled
            if (!string.IsNullOrEmpty(TB_CourseIDprefix.Text) &&
            !string.IsNullOrEmpty(TB_CourseIDsuffix1.Text) &&
            !string.IsNullOrEmpty(TB_CourseIDsuffix2.Text) &&
            !string.IsNullOrEmpty(TB_CourseIDsuffix3.Text)) {
                string textToCopy = (TB_CourseIDprefix.Text + "-"
                + TB_CourseIDsuffix1.Text + "-"
                + TB_CourseIDsuffix2.Text + "-"
                + TB_CourseIDsuffix3.Text).Trim();

                Thread thread = new Thread(() => Clipboard.SetText(textToCopy));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
        }

        private void NUMERIC_Length_ValueChanged(object sender, EventArgs e)
        {
            LABEL_CourseLengthDisplay.Text = $"0X{Convert.ToInt32(NUMERIC_Length.Value):X3}";
        }

        private void BUTTON_ExtractMii_Click(object sender, EventArgs e)
        {
            byte[] fileBytes = File.ReadAllBytes(OpenFileDialog_cdtFile.FileName);
            byte[] MiiFileBytes = new byte[CourseMiiSize];
            Array.Copy(fileBytes, CourseMiiOffset, MiiFileBytes, 0, CourseMiiSize);

            string MiiBase64 = Convert.ToBase64String(MiiFileBytes);

            var x = new DIALOG_ExtractMii(MiiBase64, TB_CourseCreator.Text, (ushort)NUMERIC_CountryCode.Value, true);
            x.ShowDialog();

            LoadComboSelectMii();
        }

        private void ToolStripMenuItem_ImportFFSD_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog_ffsdFile.ShowDialog() == DialogResult.OK)
            {   
                byte[] MiiFileBytes = File.ReadAllBytes(OpenFileDialog_ffsdFile.FileName);

                if(MiiFileBytes.Length != CourseMiiSize)
                {
                    string text = LanguageManager.Get("FORM_Main", "msgInvalidMiiFile");
                    MessageBox.Show(text);
                    ToolStripMenuItem_ImportFFSD_Click(sender, e); //First time in my life using recursive function while knowing what I'm doing
                }
                else
                {
                    string MiiBase64 = Convert.ToBase64String(MiiFileBytes);

                    var x = new DIALOG_ExtractMii(MiiBase64, OpenFileDialog_ffsdFile.SafeFileName, (ushort)NUMERIC_CountryCode.Value, false);
                    x.ShowDialog();

                    LoadComboSelectMii();
                }
            }
        }

        private void ComboBox_SelectMii_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_SelectMii.SelectedIndex != 0)
            {
                MiiData_XML = new DataTable("MiiData");
                MiiData_XML.Columns.Add("SaveName");
                MiiData_XML.Columns.Add("MiiBase64");
                MiiData_XML.Columns.Add("CountryID");
                MiiData_XML.ReadXml("Data.xml");

                tmpMiiBase64 = MiiData_XML.Rows[ComboBox_SelectMii.SelectedIndex - 1][1].ToString();
                NUMERIC_CountryCode.Value = Convert.ToInt32(MiiData_XML.Rows[ComboBox_SelectMii.SelectedIndex - 1][2]);

                //I hope the whole next block of code is going to be a temp solution
                byte[] MiiFileBytes = Convert.FromBase64String(tmpMiiBase64);
                Array.Copy(MiiFileBytes, 0, tmpfileBytes, CourseMiiOffset, CourseMiiSize); //Creator Mii writing instead of creator name

                byte[] CourseCreatorBytes = new byte[20];
                Array.Copy(tmpfileBytes, CourseCreatorStartOffset, CourseCreatorBytes, 0, CourseCreatorBytes.Length);
                char[] charCreatorArray = Encoding.Unicode.GetString(CourseCreatorBytes).TrimEnd('\0').ToArray();
                string CourseCreator = new string(charCreatorArray);

                TB_CourseCreator.Text = CourseCreator;
            }
            else
            {
                //Also the next block is a temp solution
                if (File.Exists(OpenFileDialog_cdtFile.FileName)) {
                    tmpfileBytes = File.ReadAllBytes(OpenFileDialog_cdtFile.FileName);

                    byte[] CourseCreatorBytes = new byte[20];
                    Array.Copy(tmpfileBytes, CourseCreatorStartOffset, CourseCreatorBytes, 0, CourseCreatorBytes.Length);
                    char[] charCreatorArray = Encoding.Unicode.GetString(CourseCreatorBytes).TrimEnd('\0').ToArray();
                    string CourseCreator = new string(charCreatorArray);

                    TB_CourseCreator.Text = CourseCreator;

                    byte[] MiiBytes = new byte[96];
                    Array.Copy(tmpfileBytes, CourseMiiOffset, MiiBytes, 0, MiiBytes.Length);

                    tmpMiiBase64 = Convert.ToBase64String(MiiBytes);

                    //Extract creator country bytes (offset 0xDB)
                    ushort CourseCountry = ExctractBytesFromOffset(tmpfileBytes, CourseCountryOffset);
                    NUMERIC_CountryCode.Value = CourseCountry;
                }

                return;
            }
        }
    }
}