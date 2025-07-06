using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace SMM_D4TA_EDITOR
{
    public partial class FORM_Main : Form
    {
        public FORM_Main()
        {
            InitializeComponent();
        }

        public static Encoding DefEnc = Encoding.GetEncoding("Shift-JIS");
        private string currentFilePath = "";

        int CourseNameStartOffset = 0x29;
        int CourseNameEndOffset = 0x68;

        int CourseUpdatePhysicsOffset = 0x27; //There are physics from 00 to 07

        //VALUES: [4D 31 = M1] [4D 33 = M3] [4D 57 = MW] [57 55 = WU]
        int CourseStyleStartOffset = 0x6A;
        int CourseStyleEndOffset = 0x6B;

        int CourseTimerStartOffset = 0x70;
        int CourseTimerEndOffset = 0x71;

        int CourseScrollSettingsOffset = 0x72;

        int DownloadedCourseOffset = 0x20;
        int RemovedCourseOffset = 0x21;
        int UploadedCourseOffset = 0x6E;
        int ClearCheckOffset = 0x6F;

        //VALUES: 00 = Ground, 01 Underground, 02 Castle, 03 Airship, 04 Underwater, 05 Ghost house
        int CourseThemeOffset = 0x6D;

        int CourseFirstItemOffset = 0x108;
        int CourseLastItemOffset = 0x145F0;

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
                byte[] ImageBytes = File.ReadAllBytes(OpenFileDialog_IMAGE_To_TNL.FileName);

                if (ImageBytes.Length < 4 || ImageBytes[0] != 0xFF || ImageBytes[1] != 0xD8) //JPG image first 2 bytes starts with FF D8
                {
                    MessageBox.Show("Selected file is not a valid JPG/JPEG image");
                    return;
                }

                if (ImageBytes.Length > 0xC7F8)
                {
                    MessageBox.Show("JPG/JPEG is too long\nMaximum length is 50 KB"); //Should be less than 0xC7F8 bytes
                    return;
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
                    MessageBox.Show("TNL file created successfully.");
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
                    MessageBox.Show("TNL file too short or corrupted");
                    return;
                }

                //Get JPEG length (offsets 0x04 to 0x07)
                byte[] jpegLengthBytes = new byte[4];
                Array.Copy(tnlBytes, 4, jpegLengthBytes, 0, 4);
                Array.Reverse(jpegLengthBytes); //Little endian order
                int jpegLength = BitConverter.ToInt32(jpegLengthBytes, 0);

                if (jpegLength <= 0 || jpegLength > 0xC7F8 || tnlBytes.Length < 8 + jpegLength)
                {
                    MessageBox.Show("Invalid JPEG length in TNL file");
                    return;
                }

                byte[] jpegBytes = new byte[jpegLength];
                Array.Copy(tnlBytes, 8, jpegBytes, 0, jpegLength);

                if (SaveFileDialog_TNL_To_IMAGE.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(SaveFileDialog_TNL_To_IMAGE.FileName, jpegBytes);
                    MessageBox.Show("JPEG image extracted successfully");
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

                //Extract course name bytes (from offset 0x29 to 0x68)
                int CourseNameBytesLength = CourseNameEndOffset - CourseNameStartOffset + 1;
                byte[] CourseNameBytes = new byte[CourseNameBytesLength];
                Array.Copy(fileBytes, CourseNameStartOffset, CourseNameBytes, 0, CourseNameBytesLength);
                //Convert bytes to string using UTF-16LE encode
                string CourseName = Encoding.Unicode.GetString(CourseNameBytes).TrimEnd('\0');

                //Extract course timer bytes (from offset 0x70 to 0x71)
                int CourseTimerBytesLength = CourseTimerEndOffset - CourseTimerStartOffset + 1;
                byte[] CourseTimerBytes = new byte[CourseTimerBytesLength];
                Array.Copy(fileBytes, CourseTimerStartOffset, CourseTimerBytes, 0, CourseTimerBytesLength);
                ushort CourseTimer = (ushort)((CourseTimerBytes[0] << 8) | CourseTimerBytes[1]);

                //Extract course autoscroll setting byte (offset 0x72)
                byte[] CourseScrollByte = new byte[1];
                Array.Copy(fileBytes, CourseScrollSettingsOffset, CourseScrollByte, 0, 1);
                int CourseScroll = Convert.ToInt32(CourseScrollByte[0]);

                //Extract course physics setting byte (offset 0x27)
                byte[] CourseUpdatePhysicsByte = new byte[1];
                Array.Copy(fileBytes, CourseUpdatePhysicsOffset, CourseUpdatePhysicsByte, 0, 1);
                int CourseUpdatePhysics = Convert.ToInt32(CourseUpdatePhysicsByte[0]);

                //Extract course physics setting byte (offset 0x6D)
                byte[] CourseThemeByte = new byte[1];
                Array.Copy(fileBytes, CourseThemeOffset, CourseThemeByte, 0, 1);
                int CourseTheme = Convert.ToInt32(CourseThemeByte[0]);

                //Extract course style bytes (from offset 0x6A to 0x6B)
                int CourseStyleBytesLength = CourseStyleEndOffset - CourseStyleStartOffset + 1;
                byte[] CourseStyleBytes = new byte[CourseStyleBytesLength];
                Array.Copy(fileBytes, CourseStyleStartOffset, CourseStyleBytes, 0, CourseStyleBytesLength);
                //Convert bytes to string using ASCII encode
                string CourseStyle = Encoding.ASCII.GetString(CourseStyleBytes);

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
                    LABEL_LastItemPlaced.Text = $"Last item placed (memory): {itemID} ({itemID:X2}) at 0x{lastItemOffset}";
                }

                TB_CourseName.Enabled = true;
                NUMERIC_CourseTimer.Enabled = true;
                GroupBox_Scroll_Settings.Enabled = true;
                GroupBox_Physics_Settings.Enabled = true;
                GroupBox_Style.Enabled = true;
                GroupBox_Theme.Enabled = true;
                BUTTON_TimerMinimum.Enabled = true;
                BUTTON_TimerMaximum.Enabled = true;
                BUTTON_CourseStatusNone.Enabled = true;
                CHECK_UploadReady.Enabled = true;
                BUTTON_Cancel.Enabled = true;
                BUTTON_SaveFile.Enabled = true;

                if (CourseScroll == 0) RADIO_Scroll_None.Checked = true;
                else if (CourseScroll == 1) RADIO_Scroll_Turtle.Checked = true;
                else if (CourseScroll == 2) RADIO_Scroll_Rabbit.Checked = true;
                else if (CourseScroll == 3) RADIO_Scroll_Cheetah.Checked = true;
                else if (CourseScroll == 4) RADIO_Scroll_Lock.Checked = true;
                else CourseScroll = 0;

                if (CourseUpdatePhysics == 0) RADIO_Physics00.Checked = true;
                else if (CourseUpdatePhysics == 1) RADIO_Physics01.Checked = true;
                else if (CourseUpdatePhysics == 2) RADIO_Physics02.Checked = true;
                else if (CourseUpdatePhysics == 3) RADIO_Physics03.Checked = true;
                else if (CourseUpdatePhysics == 4) RADIO_Physics04.Checked = true;
                else if (CourseUpdatePhysics == 5) RADIO_Physics05.Checked = true;
                else if (CourseUpdatePhysics == 6) RADIO_Physics06.Checked = true;
                else if (CourseUpdatePhysics == 7) RADIO_Physics07.Checked = true;
                else CourseUpdatePhysics = 0;

                if (CourseTheme == 0) RADIO_Theme00.Checked = true;
                else if (CourseTheme == 1) RADIO_Theme01.Checked = true;
                else if (CourseTheme == 2) RADIO_Theme02.Checked = true;
                else if (CourseTheme == 3) RADIO_Theme03.Checked = true;
                else if (CourseTheme == 4) RADIO_Theme04.Checked = true;
                else if (CourseTheme == 5) RADIO_Theme05.Checked = true;
                else CourseTheme = 0;

                if (CourseStyle == "M1") RADIO_M1.Checked = true;
                else if (CourseStyle == "M3") RADIO_M3.Checked = true;
                else if (CourseStyle == "MW") RADIO_MW.Checked = true;
                else if (CourseStyle == "WU") RADIO_WU.Checked = true;
                else CourseStyle = "M1";

                if (fileBytes[ClearCheckOffset] == 0x01) LABEL_ClearCheckStatus.Text = "Cleared";
                else LABEL_ClearCheckStatus.Text = "Uncleared";

                if(fileBytes[DownloadedCourseOffset] == 0x01) RADIO_CourseStatusDownloaded.Checked = true;
                else if (fileBytes[UploadedCourseOffset] == 0x01) RADIO_CourseStatusUploaded.Checked = true;
                else if (fileBytes[RemovedCourseOffset] == 0x01) RADIO_CourseStatusRemoved.Checked = true;
                else RADIO_CourseStatusNone.Checked = true;

                TB_CourseName.Text = CourseName;
                NUMERIC_CourseTimer.Value = CourseTimer;
            }
        }

        private void BUTTON_Cancel_Click(object sender, EventArgs e)
        {
            currentFilePath = "";
            CleanUI();
        }

        private void BUTTON_SaveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath)) return;

            //Set file path and read data
            byte[] fileBytes = File.ReadAllBytes(currentFilePath);
            string NewCourseName = TB_CourseName.Text;

            byte[] nameBytes = Encoding.Unicode.GetBytes(NewCourseName); //64 bytes (32 * 2)

            //Create a 64 bytes array filled with zeros
            byte[] paddedNameBytes = new byte[64];
            Array.Clear(paddedNameBytes, 0, 64);

            //Copy course name bytes to beginning of array
            Array.Copy(nameBytes, paddedNameBytes, nameBytes.Length);

            //Insert those bytes to the file
            Array.Copy(paddedNameBytes, 0, fileBytes, CourseNameStartOffset, 64);

            ushort NewCourseTimer = (ushort)NUMERIC_CourseTimer.Value;
            fileBytes[CourseTimerStartOffset] = (byte)(NewCourseTimer >> 8); //MSB
            fileBytes[CourseTimerEndOffset] = (byte)(NewCourseTimer & 0xFF); //LSB

            if (CHECK_UploadReady.Checked)
            {
                fileBytes[ClearCheckOffset] = 0x01;
            }

            byte physicsValue = 0;
            if (RADIO_Physics00.Checked) physicsValue = 0;
            else if (RADIO_Physics01.Checked) physicsValue = 1;
            else if (RADIO_Physics02.Checked) physicsValue = 2;
            else if (RADIO_Physics03.Checked) physicsValue = 3;
            else if (RADIO_Physics04.Checked) physicsValue = 4;
            else if (RADIO_Physics05.Checked) physicsValue = 5;
            else if (RADIO_Physics06.Checked) physicsValue = 6;
            else if (RADIO_Physics07.Checked) physicsValue = 7;
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
            if (RADIO_Theme00.Checked) themeValue = 0;
            else if (RADIO_Theme01.Checked) themeValue = 1;
            else if (RADIO_Theme02.Checked) themeValue = 2;
            else if (RADIO_Theme03.Checked) themeValue = 3;
            else if (RADIO_Theme04.Checked) themeValue = 4;
            else if (RADIO_Theme05.Checked) themeValue = 5;
            else themeValue = 0;
            //Insert theme byte value to the file
            fileBytes[CourseThemeOffset] = themeValue;

            string styleValue;
            if (RADIO_M1.Checked) styleValue = "M1";
            else if (RADIO_M3.Checked) styleValue = "M3";
            else if (RADIO_MW.Checked) styleValue = "MW";
            else if (RADIO_WU.Checked) styleValue = "WU";
            else styleValue = "M1";
            //Insert style byte value to the file
            byte[] styleBytes = Encoding.ASCII.GetBytes(styleValue);
            fileBytes[CourseStyleStartOffset] = styleBytes[0];
            fileBytes[CourseStyleEndOffset] = styleBytes[1];

            //Calculate and write the 4 bytes CRC-32 checksum on offsets from 0x08 to 0x0B
            Crc32 crc32 = new Crc32();
            byte[] checksum = crc32.ComputeChecksumBytes(fileBytes, 0x10, fileBytes.Length - 0x10);
            Array.Reverse(checksum); //Parse to big-endian order
            Array.Copy(checksum, 0, fileBytes, 0x08, 4);

            //Save and overwrites .cdt file
            File.WriteAllBytes(currentFilePath, fileBytes);
            MessageBox.Show("File saved successfully\n" + currentFilePath);

            CleanUI();
        }

        private void BUTTON_CourseStatusNone_Click(object sender, EventArgs e)
        {
            byte[] fileBytes = File.ReadAllBytes(currentFilePath);

            fileBytes[DownloadedCourseOffset] = 0x00;
            fileBytes[RemovedCourseOffset] = 0x00;
            fileBytes[UploadedCourseOffset] = 0x00;
            fileBytes[ClearCheckOffset] = 0x00;

            LABEL_ClearCheckStatus.Text = "Uncleared";
            RADIO_CourseStatusNone.Checked = true;

            File.WriteAllBytes(currentFilePath, fileBytes);
        }

        private void CleanUI()
        {
            TB_CourseName.Enabled = false;
            NUMERIC_CourseTimer.Enabled = false;
            CHECK_UploadReady.Enabled = false;
            GroupBox_Scroll_Settings.Enabled = false;
            GroupBox_Physics_Settings.Enabled = false;
            GroupBox_Style.Enabled = false;
            GroupBox_Theme.Enabled = false;
            BUTTON_TimerMinimum.Enabled = false;
            BUTTON_TimerMaximum.Enabled = false;
            BUTTON_CourseStatusNone.Enabled = false;
            BUTTON_Cancel.Enabled = false;
            BUTTON_SaveFile.Enabled = false;

            TB_CourseName.Text = "";
            LABEL_LastItemPlaced.Text = "Last item placed (memory):";
            NUMERIC_CourseTimer.Value = 0;
            CHECK_UploadReady.Checked = false;
        }
    }
}