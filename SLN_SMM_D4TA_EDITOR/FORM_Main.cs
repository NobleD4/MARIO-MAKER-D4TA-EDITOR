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

namespace MakerUtilidades
{
    public partial class FORM_Main : Form
    {
        public FORM_Main()
        {
            InitializeComponent();
        }

        public static Encoding DefEnc = Encoding.GetEncoding("Shift-JIS");
        private string currentFilePath = "";

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

                // Extract course name bytes (from offset 0x29 to 0x68)
                int CourseNameStartOffset = 0x29;
                int CourseNameEndOffset = 0x68;
                int nameLength = CourseNameEndOffset - CourseNameStartOffset + 1;
                byte[] nameBytes = new byte[nameLength];
                Array.Copy(fileBytes, CourseNameStartOffset, nameBytes, 0, nameLength);

                //Convert bytes to string using UTF-16LE encode
                string CourseName = Encoding.Unicode.GetString(nameBytes).TrimEnd('\0');

                TB_CourseName.Enabled = true;
                CHECK_RemoveFlags.Enabled = true;
                CHECK_UploadReady.Enabled = true;
                BUTTON_Cancel.Enabled = true;
                BUTTON_SaveFile.Enabled = true;

                TB_CourseName.Text = CourseName;
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
            int CourseNameStartOffset = 0x29;
            Array.Copy(paddedNameBytes, 0, fileBytes, CourseNameStartOffset, 64);

            if (CHECK_RemoveFlags.Checked)
            {
                fileBytes[0x20] = 0x00; //0x20 indicates if level was download from Course World
                fileBytes[0x21] = 0x00; //0x21 indicates if level was removed after having been uploaded
                fileBytes[0x6E] = 0x00; //0x6E indicates if level is currently uploaded
            }

            if (CHECK_UploadReady.Checked)
            {
                fileBytes[0x6F] = 0x01;
            }

            //Calculate and write the 4 bytes CRC-32 checksum on offsets from 0x08 to 0x0B
            Crc32 crc32 = new Crc32();
            byte[] checksum = crc32.ComputeChecksumBytes(fileBytes, 0x10, fileBytes.Length - 0x10);
            Array.Reverse(checksum); //Parse to big-endian order
            Array.Copy(checksum, 0, fileBytes, 0x08, 4);

            //Save and overwrites .cdt file
            File.WriteAllBytes(currentFilePath, fileBytes);
            MessageBox.Show("File saved successfully");

            CleanUI();
        }

        private void CleanUI()
        {
            TB_CourseName.Enabled = false;
            CHECK_RemoveFlags.Enabled = false;
            CHECK_UploadReady.Enabled = false;
            BUTTON_Cancel.Enabled = false;
            BUTTON_SaveFile.Enabled = false;

            TB_CourseName.Text = "";
            CHECK_RemoveFlags.Checked = false;
            CHECK_UploadReady.Checked = false;
        }
    }
}
