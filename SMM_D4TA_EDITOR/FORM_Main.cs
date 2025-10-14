using SLN_SMM_D4TA_EDITOR;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM_D4TA_EDITOR
{
    public partial class FORM_Main : BaseForm
    {
        public FORM_Main()
        {
            InitializeComponent();
        }

        private void FORM_Main_Load(object sender, EventArgs e)
        {
            LanguageManager.ApplyToContainer(this, "FORM_Main");
            ComboBox_Physics_Settings.Items.Clear();
            ComboBox_Physics_Settings.Items.AddRange(LanguageManager.GetList("ComboBox_Physics").ToArray());
            ComboBox_Theme_Settings.Items.AddRange(LanguageManager.GetList("ComboBox_Theme").ToArray());
            Activate();
        }

        public static Encoding DefEnc = Encoding.GetEncoding("Shift-JIS");
        private string currentFilePath = "";

        const string KeyAccessSMM1 = "9f2b4678";

        const int CourseDateYearStartOffset = 0x10;
        const int CourseDateYearEndOffset = 0x11;
        const int CourseDateMonthOffset = 0x12; //01 to 0C
        const int CourseDateDayOffset = 0x13; //01 to 1F
        const int CourseDateHourOffset = 0x14; //00 to 17
        const int CourseDateMinuteOffset = 0x15; //00 to 3B

        //ID: PRFX-SFX1-SFX2-SFX3 (PREFIX-SUBFFIX)
        const int CourseIDsuffixStartOffset = 0x1A;
        const int CourseIDsuffixEndOffset = 0x1F;

        const int CourseUpdatePhysicsOffset = 0x27; //There are physics from 00 to 07

        const int CourseNameStartOffset = 0x28;
        const int CourseNameEndOffset = 0x67;

        const int CourseCreatorStartOffset = 0x92;
        const int CourseCreatorEndOffset = 0xA5;

        //VALUES: [4D 31 = M1] [4D 33 = M3] [4D 57 = MW] [57 55 = WU]
        const int CourseStyleStartOffset = 0x6A;
        const int CourseStyleEndOffset = 0x6B;

        const int CourseTimerStartOffset = 0x70;
        const int CourseTimerEndOffset = 0x71;

        const int CourseScrollSettingsOffset = 0x72;

        const int DownloadedCourseOffset = 0x20;
        const int RemovedCourseOffset = 0x21;
        const int UploadedCourseOffset = 0x6E;
        const int ClearCheckOffset = 0x6F;

        //VALUES: 00 = Ground, 01 Underground, 02 Castle, 03 Airship, 04 Underwater, 05 Ghost house
        const int CourseThemeOffset = 0x6D;

        const int CourseFirstItemOffset = 0x108;
        const int CourseLastItemOffset = 0x145E9;

        const int CourseFirstSoundOffset = 0x145F0;
        const int CourseLastSoundOffset = 0x14F50;

        private void ToolStripMenuItem_BYML_To_XML_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog_BYML_To_XML.ShowDialog() == DialogResult.OK
            && SaveFileDialog_BYML_To_XML.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(SaveFileDialog_BYML_To_XML.FileName,
                BymlConverter.GetXml(File.ReadAllBytes(OpenFileDialog_BYML_To_XML.FileName)), DefEnc);
            }
        }

        private void ToolStripMenuItem_XML_To_BYML_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog_XML_To_BYML.ShowDialog() == DialogResult.OK
            && SaveFileDialog_XML_To_BYML.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(SaveFileDialog_XML_To_BYML.FileName,
                BymlConverter.GetByml(File.ReadAllText(OpenFileDialog_XML_To_BYML.FileName, DefEnc)));
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

        private void ToolStripMenuItem_SelectFile_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog_cdtFile.ShowDialog() == DialogResult.OK)
            {
                //Set file path and read data
                currentFilePath = OpenFileDialog_cdtFile.FileName;
                byte[] fileBytes = File.ReadAllBytes(currentFilePath);

                //Extract date year bytes (from offset 0x10 to 0x11)
                int CourseDateYearBytesLength = CourseDateYearEndOffset - CourseDateYearStartOffset + 1;
                byte[] CourseDateYearBytes = new byte[CourseDateYearBytesLength];
                Array.Copy(fileBytes, CourseDateYearStartOffset, CourseDateYearBytes, 0, CourseDateYearBytesLength);
                ushort CourseDateYear = (ushort)((CourseDateYearBytes[0] << 8) | CourseDateYearBytes[1]);

                //Extract date month bytes offset 0x12)
                byte[] CourseDateMonthByte = new byte[1];
                Array.Copy(fileBytes, CourseDateMonthOffset, CourseDateMonthByte, 0, 1);
                ushort CourseDateMonth = (ushort)(CourseDateMonthByte[0]);

                //Extract date day bytes offset 0x13)
                byte[] CourseDateDayByte = new byte[1];
                Array.Copy(fileBytes, CourseDateDayOffset, CourseDateDayByte, 0, 1);
                ushort CourseDateDay = (ushort)(CourseDateDayByte[0]);

                //Extract date hour bytes offset 0x14)
                byte[] CourseDateHourByte = new byte[1];
                Array.Copy(fileBytes, CourseDateHourOffset, CourseDateHourByte, 0, 1);
                ushort CourseDateHour = (ushort)(CourseDateHourByte[0]);

                //Extract date minute bytes offset 0x15)
                byte[] CourseDateMinuteByte = new byte[1];
                Array.Copy(fileBytes, CourseDateMinuteOffset, CourseDateMonthByte, 0, 1);
                ushort CourseDateMinute = (ushort)(CourseDateMonthByte[0]);

                //Extract course physics setting byte (offset 0x27)
                byte[] CourseUpdatePhysicsByte = new byte[1];
                Array.Copy(fileBytes, CourseUpdatePhysicsOffset, CourseUpdatePhysicsByte, 0, 1);
                int CourseUpdatePhysics = Convert.ToInt32(CourseUpdatePhysicsByte[0]);

                //Extract course ID suffix byte (from offset 0x1A to 0x1F)
                int CourseIDsuffixbytesLength = CourseIDsuffixEndOffset - CourseIDsuffixStartOffset + 1;
                byte[] CourseIDsuffixBytes = new byte[CourseIDsuffixbytesLength];
                Array.Copy(fileBytes, CourseIDsuffixStartOffset, CourseIDsuffixBytes, 0, CourseIDsuffixbytesLength);
                //Fill to 8 bytes
                byte[] paddedBytes = new byte[8];
                Array.Reverse(CourseIDsuffixBytes);
                Array.Copy(CourseIDsuffixBytes, paddedBytes, CourseIDsuffixbytesLength);
                ulong CourseIDsuffix = BitConverter.ToUInt64(paddedBytes, 0);
                string prefix = GenerateCourseIdPrefix(CourseIDsuffix);
                string CourseID = $"{prefix}{CourseIDsuffix:X12}";

                //Extract course name bytes (from offset 0x28 to 0x67)
                int CourseNameBytesLength = CourseNameEndOffset - CourseNameStartOffset + 1;
                byte[] CourseNameBytes = new byte[CourseNameBytesLength];
                Array.Copy(fileBytes, CourseNameStartOffset, CourseNameBytes, 0, CourseNameBytesLength);
                Array.Reverse(CourseNameBytes); //For some reason reversing this displays correctly chars
                //Convert bytes to a char array using UTF-16LE encode
                char[] charArray = Encoding.Unicode.GetString(CourseNameBytes).TrimEnd('\0').ToArray();
                Array.Reverse(charArray); //To make sure the course name is not reversed
                string CourseName = new string(charArray); //CourseName works!

                //Extract course style bytes (from offset 0x6A to 0x6B)
                int CourseStyleBytesLength = CourseStyleEndOffset - CourseStyleStartOffset + 1;
                byte[] CourseStyleBytes = new byte[CourseStyleBytesLength];
                Array.Copy(fileBytes, CourseStyleStartOffset, CourseStyleBytes, 0, CourseStyleBytesLength);
                //Convert bytes to string using ASCII encode
                string CourseStyle = Encoding.ASCII.GetString(CourseStyleBytes);

                //Extract course physics setting byte (offset 0x6D)
                byte[] CourseThemeByte = new byte[1];
                Array.Copy(fileBytes, CourseThemeOffset, CourseThemeByte, 0, 1);
                int CourseTheme = Convert.ToInt32(CourseThemeByte[0]);

                //Extract course timer bytes (from offset 0x70 to 0x71)
                int CourseTimerBytesLength = CourseTimerEndOffset - CourseTimerStartOffset + 1;
                byte[] CourseTimerBytes = new byte[CourseTimerBytesLength];
                Array.Copy(fileBytes, CourseTimerStartOffset, CourseTimerBytes, 0, CourseTimerBytesLength);
                ushort CourseTimer = (ushort)((CourseTimerBytes[0] << 8) | CourseTimerBytes[1]);

                //Extract course autoscroll setting byte (offset 0x72)
                byte[] CourseScrollByte = new byte[1];
                Array.Copy(fileBytes, CourseScrollSettingsOffset, CourseScrollByte, 0, 1);
                int CourseScroll = Convert.ToInt32(CourseScrollByte[0]);

                //Extract course creator bytes (from offset 0x92 to 0xA4)
                int CourseCreatorBytesLength = CourseCreatorEndOffset - CourseCreatorStartOffset;                   //TRUST ME, IF THE "+ 1" IS IN THIS LINE INSTEAD OF NEXT ONE CRASHES THANKS TO WHATEVER I DID, BUT HOPEFULLY WORKS RIGHT NOW
                byte[] CourseCreatorBytes = new byte[CourseCreatorBytesLength + 1];                                 //Epic hardcode to add an extra index to array, because for some that previously mentioned "+ 1" works here
                Array.Copy(fileBytes, CourseCreatorStartOffset, CourseCreatorBytes, 1, CourseCreatorBytesLength);   //Number 1 here because I want starts the copy on index 1 of array instead of index 0 comparing it with other chunk reads
                Array.Reverse(CourseCreatorBytes);                                                                  //Right now that empty extra index is at the end of array because I need last index as 0
                char[] charCreatorArray = Encoding.Unicode.GetString(CourseCreatorBytes).TrimEnd('\0').ToArray();   //So extra index allows to have a properly encoding of first char because game reads first the char code number and then a zero, but if this zero doesn't exists encodes a totally different char
                Array.Reverse(charCreatorArray);                                                                    //What if actually there's an easier way to read these little endian and big endian things and I'm complicating myself?
                string CourseCreator = new string(charCreatorArray); //I did exactly the same thing as course name  //I was also thinking these parts could be a function because do almost the same thing with different values, but nahhhh, it works right now so I shouldn't change this

                int Jump0x20 = 0x20;  //Basically because there's a 0x20 sized space between each item placed
                int lastItemOffset = -1; //Will throw a -1 if this value doesn't change
                int itemID = -1;

                for (int offset = CourseFirstItemOffset; offset < CourseLastItemOffset; offset += Jump0x20)
                {
                    bool isEmpty = true;

                    // Check if the 0x20-sized block is entirely 0
                    for (int i = 0; i < Jump0x20; i++)
                    {
                        if (fileBytes[offset + i] != 0x00)
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    //Stop if there's an empty block
                    if (isEmpty) break;

                    //Update if isn't empty
                    itemID = fileBytes[offset];
                    lastItemOffset = offset;
                }

                if (lastItemOffset != -1)
                {
                    string lastItemPlacedLang = LanguageManager.Get("FORM_Main", "LABEL_LastItemPlaced");
                    string lastItemOffsetLang = LanguageManager.Get("FORM_Main", "LABEL_LastItemOffset");
                    LABEL_LastItemPlaced.Text = lastItemPlacedLang + $" {itemID} (0x{itemID:X2})";
                    LABEL_LastItemOffset.Text = lastItemOffsetLang + $" 0x{lastItemOffset:X2}"; //OH WOW, THIS WASN'T HEX IN PREVIOUS RELEASE
                }

                UIstate(true);

                if (CourseScroll == 0) RADIO_Scroll_None.Checked = true;
                else if (CourseScroll == 1) RADIO_Scroll_Turtle.Checked = true;
                else if (CourseScroll == 2) RADIO_Scroll_Rabbit.Checked = true;
                else if (CourseScroll == 3) RADIO_Scroll_Cheetah.Checked = true;
                else if (CourseScroll == 4) RADIO_Scroll_Lock.Checked = true;
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
                if (fileBytes[ClearCheckOffset] == 0x01) LABEL_ClearCheckStatus.Text = clearCheckStatus1;
                else LABEL_ClearCheckStatus.Text = clearCheckStatus0;

                //This course status section used to be for only read, but not for write at all
                //The problem was basically every status is a completely different byte offset
                //Every of them from 00 to 01 to set status, probably not very efficient but works in the game
                //I don't know yet what would happen if I set all of these bytes offsets to 01 at the same time
                if(fileBytes[DownloadedCourseOffset] == 0x01) CHECK_CourseStatusDownloaded.Checked = true;
                else CHECK_CourseStatusDownloaded.Checked = false;
                if (fileBytes[UploadedCourseOffset] == 0x01) CHECK_CourseStatusUploaded.Checked = true;
                else CHECK_CourseStatusUploaded.Checked = false;
                if (fileBytes[RemovedCourseOffset] == 0x01) CHECK_CourseStatusRemoved.Checked = true;
                else CHECK_CourseStatusRemoved.Checked = false;

                TB_CourseName.Text = CourseName;
                TB_CourseCreator.Text = CourseCreator;
                TB_CourseIDprefix.Text = CourseID.Substring(0, 4);
                TB_CourseIDsuffix1.Text = CourseID.Substring(4, 4);
                TB_CourseIDsuffix2.Text = CourseID.Substring(8, 4);
                TB_CourseIDsuffix3.Text = CourseID.Substring(12, 4);
                NUMERIC_CourseTimer.Value = CourseTimer;
                NUMERIC_CourseYear.Value = CourseDateYear;
                NUMERIC_CourseMonth.Value = CourseDateMonth;
                NUMERIC_CourseDay.Value = CourseDateDay;
                NUMERIC_CourseHour.Value = CourseDateHour;
                NUMERIC_CourseMinute.Value = CourseDateMinute;
            }
        }

        static string GenerateCourseIdPrefix(ulong suffix)
        {
            byte[] baseKey = Encoding.ASCII.GetBytes(KeyAccessSMM1);
            //MD5 to baseKey
            using (var md5 = MD5.Create())
            {
                baseKey = md5.ComputeHash(baseKey);
            }
            //Little-endian suffix
            byte[] data = BitConverter.GetBytes(suffix);
            //HMAC-MD5
            byte[] checksum;
            using (var hmac = new HMACMD5(baseKey))
            {
                checksum = hmac.ComputeHash(data);
            }
            //Checksum[3:1:-1], bytes 3 & 2 reversed
            string prefix = checksum[3].ToString("X2") + checksum[2].ToString("X2");
            return prefix;
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

            //Write course creator, very similar to previous block of code above
            int NewCourseCreatorBytesLength = CourseCreatorEndOffset - CourseCreatorStartOffset + 1;
            //Direct encoding (UTF-16 little endian)
            byte[] NewCourseCreatorBytes = new byte[NewCourseCreatorBytesLength];
            NewCourseCreatorBytes = Encoding.Unicode.GetBytes(TB_CourseCreator.Text);
            //Create a 20 bytes array filled with zeros
            byte[] paddedCreatorBytes = new byte[20];
            Array.Clear(paddedCreatorBytes, 0, 20);
            //Copy course creator bytes to beginning of array
            Array.Copy(NewCourseCreatorBytes, paddedCreatorBytes, NewCourseCreatorBytes.Length);
            //Insert those bytes to the file
            Array.Copy(paddedCreatorBytes, 0, fileBytes, CourseCreatorStartOffset, 20);

            ushort NewCourseTimer = (ushort)NUMERIC_CourseTimer.Value;
            fileBytes[CourseTimerStartOffset] = (byte)(NewCourseTimer >> 8); //MSB
            fileBytes[CourseTimerEndOffset] = (byte)(NewCourseTimer & 0xFF); //LSB

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
            if (ComboBox_Physics_Settings.SelectedIndex == 0) physicsValue = 0;
            else if (ComboBox_Physics_Settings.SelectedIndex == 1) physicsValue = 1;
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
            if (RADIO_Scroll_Turtle.Checked) scrollValue = 1;
            else if (RADIO_Scroll_Rabbit.Checked) scrollValue = 2;
            else if (RADIO_Scroll_Cheetah.Checked) scrollValue = 3;
            else if (RADIO_Scroll_Lock.Checked) scrollValue = 4;
            else scrollValue = 0;
            //Insert scroll byte value to the file
            fileBytes[CourseScrollSettingsOffset] = scrollValue;

            byte themeValue = 0;

            if (ComboBox_Theme_Settings.SelectedIndex == 0) themeValue = 0;
            else if (ComboBox_Theme_Settings.SelectedIndex == 1) themeValue = 1;
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
            TB_CourseIDsuffix1.Enabled = state;
            TB_CourseIDsuffix2.Enabled = state;
            TB_CourseIDsuffix3.Enabled = state;
            NUMERIC_CourseTimer.Enabled = state;
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
            GroupBox_Scroll_Settings.Enabled = state;
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
                LABEL_LastItemOffset.Text = LanguageManager.Get("FORM_Main", "LABEL_LastItemOffset");
                NUMERIC_CourseTimer.Value = 0;
                NUMERIC_CourseYear.Value = 0;
                NUMERIC_CourseMonth.Value = 0;
                NUMERIC_CourseDay.Value = 0;
                NUMERIC_CourseHour.Value = 0;
                NUMERIC_CourseMinute.Value = 0;
                CHECK_UploadReady.Checked = false;
                CHECK_SetDateTimeNow.Checked = false;
                CHECK_UploadReady.Checked = false;
                CHECK_CourseStatusDownloaded.Checked = false;
                CHECK_CourseStatusUploaded.Checked = false;
                CHECK_CourseStatusRemoved.Checked = false;
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

                //Set file path and read data
                currentFilePath = OpenFileDialog_cdtFile.FileName;
                byte[] fileBytes = File.ReadAllBytes(currentFilePath);

                //Extract date month bytes offset 0x12)
                byte[] CourseDateMonthByte = new byte[1];
                Array.Copy(fileBytes, CourseDateMonthOffset, CourseDateMonthByte, 0, 1);
                ushort CourseDateMonth = (ushort)(CourseDateMonthByte[0]);

                //Extract date day bytes offset 0x13)
                byte[] CourseDateDayByte = new byte[1];
                Array.Copy(fileBytes, CourseDateDayOffset, CourseDateDayByte, 0, 1);
                ushort CourseDateDay = (ushort)(CourseDateDayByte[0]);

                //Extract date hour bytes offset 0x14)
                byte[] CourseDateHourByte = new byte[1];
                Array.Copy(fileBytes, CourseDateHourOffset, CourseDateHourByte, 0, 1);
                ushort CourseDateHour = (ushort)(CourseDateHourByte[0]);

                //Extract date minute bytes offset 0x15)
                byte[] CourseDateMinuteByte = new byte[1];
                Array.Copy(fileBytes, CourseDateMinuteOffset, CourseDateMonthByte, 0, 1);
                ushort CourseDateMinute = (ushort)(CourseDateMonthByte[0]);

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
    }
}