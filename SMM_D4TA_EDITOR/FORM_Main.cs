using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SMM_D4TA_EDITOR.SMM1FileFormats;
using static SMM_D4TA_EDITOR.SMM2FileFormats;

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
            if (OpenFileDialog_CourseFile.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = OpenFileDialog_CourseFile.FileName;
                tmpfileBytes = File.ReadAllBytes(currentFilePath);

                if (Path.GetExtension(currentFilePath) == ".cdt")
                {
                    ComboBox_Physics_Settings.Items.Clear();
                    ComboBox_Physics_Settings.Items.AddRange(LanguageManager.GetList("ComboBox_Physics").ToArray());
                    LoadComboSelectMii();
                    ReadSMM1Course(ref tmpfileBytes,
                        ref NUMERIC_CourseYear, ref NUMERIC_CourseMonth, ref NUMERIC_CourseDay,
                        ref NUMERIC_CourseHour, ref NUMERIC_CourseMinute,
                        ref CHECK_SetDateTimeNow,
                        ref NUMERIC_Physics_Settings,
                        ref TB_CourseIDprefix, ref TB_CourseIDsuffix1, ref TB_CourseIDsuffix2, ref TB_CourseIDsuffix3,
                        ref TB_CourseName, ref TB_Style_Settings,
                        ref NUMERIC_Theme_Settings, ref NUMERIC_CourseTimer, ref NUMERIC_Scroll_Settings,
                        ref NUMERIC_Length,
                        ref TB_CourseCreator, ref NUMERIC_CountryCode,
                        ref LABEL_LastItemPlaced, ref LABEL_LastSFXplaced,
                        ref NUMERIC_OfficialCourse,
                        ref NUMERIC_CourseStatusDownloaded,
                        ref NUMERIC_CourseStatusUploaded,
                        ref NUMERIC_CourseStatusRemoved,
                        ref LABEL_ClearCheckStatus,
                        ref NUMERIC_UploadReady
                    );
                    UIstate(true);
                }
                else if (Path.GetExtension(currentFilePath) == ".bcd")
                {
                    if (tmpfileBytes.Length == 0x5C000) //If is encrypted
                    {
                        DecryptSMM2Course(ref tmpfileBytes);
                    }

                    ComboBox_Physics_Settings.Items.Clear();
                    ComboBox_Physics_Settings.Items.AddRange(LanguageManager.GetList("ComboBox_GameVersionSMM2").ToArray());

                    ComboBox_GameVersion_ClearCheck.Items.Clear();
                    ComboBox_GameVersion_ClearCheck.Items.AddRange(LanguageManager.GetList("ComboBox_GameVersionSMM2").ToArray());

                    ReadSMM2Course(ref tmpfileBytes,
                        ref NUMERIC_CourseTimer,
                        ref NUMERIC_CourseYear, ref NUMERIC_CourseMonth, ref NUMERIC_CourseDay,
                        ref NUMERIC_CourseHour, ref NUMERIC_CourseMinute,
                        ref NUMERIC_ClearCheckAttempts,
                        ref NUMERIC_ClearCheckTime,
                        ref TB_CourseIDsuffix1, ref TB_CourseIDsuffix2, ref TB_CourseIDsuffix3,
                        ref ComboBox_GameVersion_ClearCheck,
                        ref ComboBox_Style_Settings,
                        ref TB_CourseName,
                        ref TB_CourseDescription
                    );
                    UIstate(true);
                }
                else
                {
                    MessageBox.Show("Invalid file " + Path.GetExtension(currentFilePath));
                }
            }
        }

        private void BUTTON_Cancel_Click(object sender, EventArgs e)
        {
            UIstate(false);
        }

        private void BUTTON_SaveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath)) return;

            if (Path.GetExtension(currentFilePath) == ".cdt")
            {
                WriteSMM1Course(ref currentFilePath,
                    ref NUMERIC_CourseYear, ref NUMERIC_CourseMonth, ref NUMERIC_CourseDay,
                    ref NUMERIC_CourseHour, ref NUMERIC_CourseMinute,
                    ref NUMERIC_Physics_Settings,
                    ref TB_CourseIDprefix, ref TB_CourseIDsuffix1, ref TB_CourseIDsuffix2, ref TB_CourseIDsuffix3,
                    ref TB_CourseName, ref TB_Style_Settings,
                    ref NUMERIC_Theme_Settings, ref NUMERIC_CourseTimer, ref NUMERIC_Scroll_Settings,
                    ref NUMERIC_Length,
                    ref tmpMiiBase64, ref NUMERIC_CountryCode,
                    ref NUMERIC_OfficialCourse,
                    ref NUMERIC_CourseStatusDownloaded,
                    ref NUMERIC_CourseStatusUploaded,
                    ref NUMERIC_CourseStatusRemoved,
                    ref CHECK_UploadReady,
                    ref NUMERIC_UploadReady
                );
            }
            else if (Path.GetExtension(currentFilePath) == ".bcd")
            {
                WriteSMM2Course(ref tmpfileBytes);
                EncryptSMM2Course(ref tmpfileBytes);
            }

            UIstate(false);
        }

        private void UIstate(bool state)
        {
            TB_CourseName.Enabled = state;
            TB_CourseDescription.Enabled = state;
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
            NUMERIC_ClearCheckTime.Enabled = state;
            NUMERIC_ClearCheckAttempts.Enabled = state;
            CHECK_SetDateTimeNow.Enabled = state;
            CHECK_UploadReady.Enabled = state;
            NUMERIC_UploadReady.Enabled = state;
            ComboBox_Physics_Settings.Enabled = state;
            NUMERIC_Physics_Settings.Enabled = state;
            ComboBox_GameVersion_ClearCheck.Enabled = state;
            NUMERIC_GameVersion_ClearCheck.Enabled = state;
            ComboBox_Style_Settings.Enabled = state;
            TB_Style_Settings.Enabled = state;
            ComboBox_Theme_Settings.Enabled = state;
            NUMERIC_Theme_Settings.Enabled = state;
            ComboBox_Scroll_Settings.Enabled = state;
            NUMERIC_Scroll_Settings.Enabled = state;
            ComboBox_OfficialCourse.Enabled = state;
            NUMERIC_OfficialCourse.Enabled = state;
            ComboBox_SelectMii.Enabled = state;
            BUTTON_ExtractMii.Enabled = state;
            BUTTON_TimerMinimum.Enabled = state;
            BUTTON_TimerMaximum.Enabled = state;
            CHECK_CourseStatusDownloaded.Enabled = state;
            NUMERIC_CourseStatusDownloaded.Enabled = state;
            CHECK_CourseStatusUploaded.Enabled = state;
            NUMERIC_CourseStatusUploaded.Enabled = state;
            CHECK_CourseStatusRemoved.Enabled = state;
            NUMERIC_CourseStatusRemoved.Enabled = state;
            BUTTON_Cancel.Enabled = state;
            BUTTON_SaveFile.Enabled = state;
            BUTTON_CopyID.Enabled = state;

            if (state == false)
            {
                currentFilePath = "";
                TB_CourseName.Text = "";
                TB_CourseDescription.Text = "";
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
                NUMERIC_ClearCheckTime.Value = 0;
                NUMERIC_ClearCheckAttempts.Value = 0;
                NUMERIC_Length.Value = 384;
                LABEL_CourseLengthDisplay.Text = "0x180";
                TB_Style_Settings.Text = "";
                NUMERIC_Physics_Settings.Value = 0;
                NUMERIC_GameVersion_ClearCheck.Value = 0;
                NUMERIC_Theme_Settings.Value = 0;
                NUMERIC_Scroll_Settings.Value = 0;
                NUMERIC_OfficialCourse.Value = 0;
                ComboBox_Physics_Settings.SelectedIndex = -1; //I don't have idea why this doesn't cleans the text, so I commented it //October 9th 2026: Finally fixed!
                ComboBox_Style_Settings.SelectedIndex = -1;
                ComboBox_Theme_Settings.SelectedIndex = -1;
                ComboBox_Scroll_Settings.SelectedIndex = -1;
                ComboBox_OfficialCourse.SelectedIndex = -1;
                CHECK_SetDateTimeNow.Checked = state;
                NUMERIC_CourseYear.Enabled = state;
                NUMERIC_CourseMonth.Enabled = state;
                NUMERIC_CourseDay.Enabled = state;
                NUMERIC_CourseHour.Enabled = state;
                NUMERIC_CourseMinute.Enabled = state;
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
                NUMERIC_CourseYear.Enabled = false;
                NUMERIC_CourseMonth.Enabled = false;
                NUMERIC_CourseDay.Enabled = false;
                NUMERIC_CourseHour.Enabled = false;
                NUMERIC_CourseMinute.Enabled = false;

                NUMERIC_CourseYear.Value = DateTime.Now.Year;
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
                currentFilePath = OpenFileDialog_CourseFile.FileName;
                byte[] fileBytes = File.ReadAllBytes(currentFilePath);

                //Extract date year bytes (from offset 0x10 to 0x11)
                int CourseDateYearBytesLength = CourseDateYearEndOffset - CourseDateYearStartOffset + 1;
                byte[] CourseDateYearBytes = new byte[CourseDateYearBytesLength];
                Array.Copy(tmpfileBytes, CourseDateYearStartOffset, CourseDateYearBytes, 0, CourseDateYearBytesLength);
                ushort CourseDateYear = (ushort)((CourseDateYearBytes[0] << 8) | CourseDateYearBytes[1]);

                //Extract date month bytes (offset 0x12)
                ushort CourseDateMonth = ExctractBytesFromOffset(fileBytes, CourseDateMonthOffset);

                //Extract date day bytes (offset 0x13)
                ushort CourseDateDay = ExctractBytesFromOffset(fileBytes, CourseDateDayOffset);

                //Extract date hour bytes (offset 0x14)
                ushort CourseDateHour = ExctractBytesFromOffset(fileBytes, CourseDateHourOffset);

                //Extract date minute bytes (offset 0x15)
                ushort CourseDateMinute = ExctractBytesFromOffset(fileBytes, CourseDateMinuteOffset);

                NUMERIC_CourseYear.Enabled = true;
                NUMERIC_CourseMonth.Enabled = true;
                NUMERIC_CourseDay.Enabled = true;
                NUMERIC_CourseHour.Enabled = true;
                NUMERIC_CourseMinute.Enabled = true;

                NUMERIC_CourseYear.Value = CourseDateYear;
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
            byte[] fileBytes = File.ReadAllBytes(OpenFileDialog_CourseFile.FileName);
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
                if (File.Exists(OpenFileDialog_CourseFile.FileName)) {
                    tmpfileBytes = File.ReadAllBytes(OpenFileDialog_CourseFile.FileName);

                    byte[] CourseCreatorBytes = new byte[20];
                    Array.Copy(tmpfileBytes, CourseCreatorStartOffset, CourseCreatorBytes, 0, CourseCreatorBytes.Length);
                    char[] charCreatorArray = Encoding.Unicode.GetString(CourseCreatorBytes).TrimEnd('\0').ToArray();
                    string CourseCreator = new string(charCreatorArray);

                    TB_CourseCreator.Text = CourseCreator;

                    byte[] MiiBytes = new byte[CourseMiiSize];
                    Array.Copy(tmpfileBytes, CourseMiiOffset, MiiBytes, 0, MiiBytes.Length);

                    tmpMiiBase64 = Convert.ToBase64String(MiiBytes);

                    //Extract creator country bytes (offset 0xDB)
                    ushort CourseCountry = ExctractBytesFromOffset(tmpfileBytes, CourseCountryOffset);
                    NUMERIC_CountryCode.Value = CourseCountry;
                }

                return;
            }
        }

        private void OutOfRange_Selection(ref NumericUpDown numeric, ref ComboBox combo, byte substractValue)
        {
            if (numeric.Value < combo.Items.Count - substractValue)
            {
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                combo.SelectedIndex = Convert.ToInt32(numeric.Value);
            }
            else
            {
                combo.DropDownStyle = ComboBoxStyle.DropDown;
                combo.Text = LanguageManager.Get("FORM_Main", "msgOutOfRange");

            }
        }

        private void NUMERIC_Physics_Settings_ValueChanged(object sender, EventArgs e)
        {
            OutOfRange_Selection(ref NUMERIC_Physics_Settings, ref ComboBox_Physics_Settings, 0);
        }


        private void NUMERIC_GameVersion_ClearCheck_ValueChanged(object sender, EventArgs e)
        {
            OutOfRange_Selection(ref NUMERIC_GameVersion_ClearCheck, ref ComboBox_GameVersion_ClearCheck, 0);
        }

        private void NUMERIC_Theme_Settings_ValueChanged(object sender, EventArgs e)
        {
            OutOfRange_Selection(ref NUMERIC_Theme_Settings, ref ComboBox_Theme_Settings, 0);
        }

        private void NUMERIC_Scroll_Settings_ValueChanged(object sender, EventArgs e)
        {
            OutOfRange_Selection(ref NUMERIC_Scroll_Settings, ref ComboBox_Scroll_Settings, 0);
        }

        private void NUMERIC_OfficialCourse_ValueChanged(object sender, EventArgs e)
        {
            OutOfRange_Selection(ref NUMERIC_OfficialCourse, ref ComboBox_OfficialCourse, 1);
            if(NUMERIC_OfficialCourse.Value == 0x1D)
            {
                ComboBox_OfficialCourse.DropDownStyle = ComboBoxStyle.DropDownList;
                ComboBox_OfficialCourse.SelectedIndex = 9;
            }
        }

        private void TB_Style_Settings_TextChanged(object sender, EventArgs e)
        {
            ComboBox_Style_Settings.DropDownStyle = ComboBoxStyle.DropDownList;
            if (TB_Style_Settings.Text == "M1") ComboBox_Style_Settings.SelectedIndex = 0;
            else if (TB_Style_Settings.Text == "M3") ComboBox_Style_Settings.SelectedIndex = 1;
            else if (TB_Style_Settings.Text == "MW") ComboBox_Style_Settings.SelectedIndex = 2;
            else if (TB_Style_Settings.Text == "WU") ComboBox_Style_Settings.SelectedIndex = 3;
            else if (TB_Style_Settings.Text == "3W") ComboBox_Style_Settings.SelectedIndex = 4;
            else
            {
                ComboBox_Style_Settings.DropDownStyle = ComboBoxStyle.DropDown;
                ComboBox_Style_Settings.Text = LanguageManager.Get("FORM_Main", "msgOutOfRange");
            }
        }

        private void NUMERIC_CourseStatusDownloaded_ValueChanged(object sender, EventArgs e)
        {
            if (NUMERIC_CourseStatusDownloaded.Value == 1) CHECK_CourseStatusDownloaded.Checked = true;
            else CHECK_CourseStatusDownloaded.Checked = false;
        }

        private void NUMERIC_CourseStatusUploaded_ValueChanged(object sender, EventArgs e)
        {
            if (NUMERIC_CourseStatusUploaded.Value == 1) CHECK_CourseStatusUploaded.Checked = true;
            else CHECK_CourseStatusUploaded.Checked = false;
        }

        private void NUMERIC_CourseStatusRemoved_ValueChanged(object sender, EventArgs e)
        {
            if (NUMERIC_CourseStatusRemoved.Value == 1) CHECK_CourseStatusRemoved.Checked = true;
            else CHECK_CourseStatusRemoved.Checked = false;
        }

        private void NUMERIC_UploadReady_ValueChanged(object sender, EventArgs e)
        {
            if (NUMERIC_UploadReady.Value == 1) CHECK_UploadReady.Checked = true;
            else CHECK_UploadReady.Checked = false;
        }

        private void ComboBox_Physics_Settings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Physics_Settings.SelectedIndex != -1)
            {
                NUMERIC_Physics_Settings.Value = ComboBox_Physics_Settings.SelectedIndex;
            }
        }

        private void ComboBox_GameVersion_ClearCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_GameVersion_ClearCheck.SelectedIndex != -1)
            {
                NUMERIC_GameVersion_ClearCheck.Value = ComboBox_GameVersion_ClearCheck.SelectedIndex;
            }
        }

        private void ComboBox_Style_Settings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Style_Settings.SelectedIndex != -1)
            {
                if (ComboBox_Style_Settings.SelectedIndex == 0) TB_Style_Settings.Text = "M1";
                else if (ComboBox_Style_Settings.SelectedIndex == 1) TB_Style_Settings.Text = "M3";
                else if (ComboBox_Style_Settings.SelectedIndex == 2) TB_Style_Settings.Text = "MW";
                else if (ComboBox_Style_Settings.SelectedIndex == 3) TB_Style_Settings.Text = "WU";
                else if (ComboBox_Style_Settings.SelectedIndex == 4) TB_Style_Settings.Text = "3W";
            }
        }

        private void ComboBox_Theme_Settings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Theme_Settings.SelectedIndex != -1)
            {
                NUMERIC_Theme_Settings.Value = ComboBox_Theme_Settings.SelectedIndex;
            }
        }

        private void ComboBox_Scroll_Settings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_Scroll_Settings.SelectedIndex != -1)
            {
                NUMERIC_Scroll_Settings.Value = ComboBox_Scroll_Settings.SelectedIndex;
            }
        }

        private void ComboBox_OfficialCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_OfficialCourse.SelectedIndex != -1)
            {
                if (ComboBox_OfficialCourse.SelectedIndex != 9) NUMERIC_OfficialCourse.Value = ComboBox_OfficialCourse.SelectedIndex;
                else NUMERIC_OfficialCourse.Value = 0x1D;
            }
        }

        private void CHECK_CourseStatusDownloaded_CheckedChanged(object sender, EventArgs e)
        {
            if (CHECK_CourseStatusDownloaded.Checked == true) NUMERIC_CourseStatusDownloaded.Value = 1;
        }

        private void CHECK_CourseStatusUploaded_CheckedChanged(object sender, EventArgs e)
        {
            if (CHECK_CourseStatusUploaded.Checked == true) NUMERIC_CourseStatusUploaded.Value = 1;
        }

        private void CHECK_CourseStatusRemoved_CheckedChanged(object sender, EventArgs e)
        {
            if (CHECK_CourseStatusRemoved.Checked == true) NUMERIC_CourseStatusRemoved.Value = 1;
        }
    }
}