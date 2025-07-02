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

        int CourseUpdatePhysics = 0x27;

        int CourseTimerStartOffset = 0x70;
        int CourseTimerEndOffset = 0x71;

        int CourseScrollSettingsOffset = 0x72;

        int DownloadedCourseOffset = 0x20;
        int RemovedCourseOffset = 0x21;
        int UploadedCourseOffset = 0x6E;
        int ClearCheckOffset = 0x6F;

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
                    LABEL_LastItemPlaced.Text = $"Last item placed (memory): {itemID} ({itemID:X2})";
                }

                TB_CourseName.Enabled = true;
                NUMERIC_CourseTimer.Enabled = true;
                GroupBox_Scroll_Settings.Enabled = true;
                CHECK_OldPhysics.Enabled = true;
                CHECK_RemoveFlags.Enabled = true;
                CHECK_UploadReady.Enabled = true;
                BUTTON_Cancel.Enabled = true;
                BUTTON_SaveFile.Enabled = true;

                if(CourseScroll == 0) RADIO_Scroll_None.Checked = true;
                
                else if(CourseScroll == 1) RADIO_Scroll_Turtle.Checked = true;
                
                else if(CourseScroll == 2) RADIO_Scroll_Rabbit.Checked = true;
                
                else if(CourseScroll == 3) RADIO_Scroll_Cheetah.Checked = true;
                
                else if(CourseScroll == 4) RADIO_Scroll_Lock.Checked = true;
                else CourseScroll = 0;

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
            if (string.IsNullOrEmpty(currentFilePath))
            {
                return;
            }

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

            if (CHECK_RemoveFlags.Checked)
            {
                fileBytes[DownloadedCourseOffset] = 0x00;
                fileBytes[RemovedCourseOffset] = 0x00;
                fileBytes[UploadedCourseOffset] = 0x00;
                fileBytes[ClearCheckOffset] = 0x00;
            }

            if (CHECK_UploadReady.Checked)
            {
                fileBytes[ClearCheckOffset] = 0x01;
            }

            //There are physics from 00 to 07 (offset 0x27)
            if (CHECK_OldPhysics.Checked)
            {
                fileBytes[CourseUpdatePhysics] = 0x00;
            }

            byte scrollValue = 0;
            if (RADIO_Scroll_Turtle.Checked) scrollValue = 1;
            else if (RADIO_Scroll_Rabbit.Checked) scrollValue = 2;
            else if (RADIO_Scroll_Cheetah.Checked) scrollValue = 3;
            else if (RADIO_Scroll_Lock.Checked) scrollValue = 4;
            else scrollValue = 0;

            //Insert scroll byte value to the file
            fileBytes[CourseScrollSettingsOffset] = scrollValue;

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

        private void CleanUI()
        {
            TB_CourseName.Enabled = false;
            NUMERIC_CourseTimer.Enabled = false;
            CHECK_RemoveFlags.Enabled = false;
            CHECK_UploadReady.Enabled = false;
            CHECK_OldPhysics.Enabled = false;
            GroupBox_Scroll_Settings.Enabled = false;
            BUTTON_Cancel.Enabled = false;
            BUTTON_SaveFile.Enabled = false;

            TB_CourseName.Text = "";
            LABEL_LastItemPlaced.Text = "Last item placed (memory):";
            NUMERIC_CourseTimer.Value = 0;
            CHECK_RemoveFlags.Checked = false;
            CHECK_UploadReady.Checked = false;
            CHECK_OldPhysics.Checked = false;
        }
    }
}
