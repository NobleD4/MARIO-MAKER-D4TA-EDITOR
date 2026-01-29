using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM_D4TA_EDITOR
{
    public partial class FORM_SMM1_SaveFile : BaseForm
    {
        public FORM_SMM1_SaveFile()
        {
            InitializeComponent();
        }

        private void FORM_SMM1_SaveFile_Load(object sender, EventArgs e)
        {
            LanguageManager.ApplyToContainer(this, "FORM_SMM1_SaveFile");
            Activate();
        }

        private string currentFilePath = "";

        //If you sum an 8 to exactly the first digit of the byte, item will have "new" icon (exclamation mark, event which only happens once per save file)
        //For example, 00 doesn't have "new" icon, but if you change it to 80 will have
        //Basically sum 80 allows to the item to be marked as "new"
        //Looks like locked elements have 7F value
        const int ItemsDelivery1StartOffstet = 0x4230;
        const int ItemsDelivery2StartOffstet = 0x423C;

        const int Settings_ControlsOffset = 0x4332; //00 DashJump, 01 JumpDash
        const int Settings_GamePad3DAudioOffset = 0x4337; //00 Off, 01 On
        const int Settings_NotifyCourseCreatorsOffset = 0x4338; //00 Off, 01 On
        const int Settings_NotifyMyCoursesOffset = 0x4339; //00 Off, 01 On

        //Offsets from all of 120 levels sorted in coursebot
        //If you never move a course to another slot, would be sorted
        const int CoursebotStartOffset = 0x4340;
        const int CoursebotEndOffset = 0x43B7;

        public class CoursebotEntry
        {
            public int CourseIndex { get; set; } //From course000 to course119
            public byte MemoryValue { get; set; } //From 0x00 to 0x77 (0 to 119)
            public string DisplayText { get; set; }
        }

        List<CoursebotEntry> CoursebotCoursesList = new List<CoursebotEntry>();

        List<byte> GetFreeMemoryValues(List<CoursebotEntry> list)
        {
            var used = list
                .Where(e => e.MemoryValue != 0xFF)
                .Select(e => e.MemoryValue)
                .ToHashSet();

            List<byte> free = new List<byte>();

            for (byte i = 0x00; i <= 0x77; i++) //0x00 to 0x77 (0 to 119)
            {
                if (!used.Contains(i))
                    free.Add(i);
            }

            return free;
        }

        private void LISTBOX_Coursebot_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void LISTBOX_Coursebot_MouseDown(object sender, MouseEventArgs e)
        {
            int index = LISTBOX_Coursebot.IndexFromPoint(e.Location);
            if (index < 0) return;

            LISTBOX_Coursebot.SelectedIndex = index;
            LISTBOX_Coursebot.DoDragDrop(index, DragDropEffects.Move);
        }

        private void LISTBOX_Coursebot_DragDrop(object sender, DragEventArgs e)
        {
            Point point = LISTBOX_Coursebot.PointToClient(new Point(e.X, e.Y));
            int targetIndex = LISTBOX_Coursebot.IndexFromPoint(point);
            if (targetIndex < 0) return;

            int sourceIndex = (int)e.Data.GetData(typeof(int));
            if (sourceIndex == targetIndex) return;

            var source = CoursebotCoursesList[sourceIndex];
            var target = CoursebotCoursesList[targetIndex];

            // Case 1: empty origin → don't move
            if (source.MemoryValue == 0xFF)
                return;

            // Case 2: empty destiny → move course
            if (target.MemoryValue == 0xFF)
            {
                var freeValues = GetFreeMemoryValues(CoursebotCoursesList);
                byte newValue = freeValues.First();

                source.MemoryValue = newValue;
            }
            // Case 3: courses swap
            else
            {
                byte temp = source.MemoryValue;
                source.MemoryValue = target.MemoryValue;
                target.MemoryValue = temp;
            }

            RefreshCoursebotList();
        }

        private void ToolStripMenuItem_SelectFile_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog_datFile.ShowDialog() == DialogResult.OK)
            {
                //Set file path and read data
                currentFilePath = OpenFileDialog_datFile.FileName;
                byte[] fileBytes = File.ReadAllBytes(currentFilePath);

                //Extract Item bytes (from offset 0x4230 to ???) Idk, I don't want to do this right now
                //EDIT: I don't remember what I was actually going to do here but I found a bug sorting courses when there's an empty slot with 0xFF value

                //Extract Coursebot 120 bytes (from offset 0x4340 to 0x43B7)
                const int CoursebotBytesLength = CoursebotEndOffset - CoursebotStartOffset + 1;
                //byte[] CoursebotBytes = new byte[CoursebotBytesLength];

                LISTBOX_Coursebot.Items.Clear();
                CoursebotCoursesList.Clear();

                // Read every slot
                for (int i = 0; i < CoursebotBytesLength; i++) //120 slots/levels
                {
                    int offset = CoursebotStartOffset + i;
                    byte memoryValue = fileBytes[offset]; //Level number in memory (from 0x00 to 0x77)
                    string itemText = null;

                    itemText = ListBoxText(i, memoryValue);

                    CoursebotCoursesList.Add(new CoursebotEntry
                    {
                        CourseIndex = i,
                        MemoryValue = memoryValue,
                        DisplayText = itemText
                    });

                    this.LISTBOX_Coursebot.Items.Add(itemText);
                }

                //Extract controls setting byte (offset 0x4332)
                byte[] SettingsControlsByte = new byte[1];
                Array.Copy(fileBytes, Settings_ControlsOffset, SettingsControlsByte, 0, 1);
                int SettingsControl = Convert.ToInt32(SettingsControlsByte[0]);

                //Extract GamePad 3D Audio setting byte (offset 0x4337)
                byte[] SettingsGamePad3DAudioByte = new byte[1];
                Array.Copy(fileBytes, Settings_GamePad3DAudioOffset, SettingsGamePad3DAudioByte, 0, 1);
                int SettingsGamePad3DAudio = Convert.ToInt32(SettingsGamePad3DAudioByte[0]);

                //Extract notify to course creators setting byte (offset 0x4338)
                byte[] SettingsNotifyCourseCreatorsByte = new byte[1];
                Array.Copy(fileBytes, Settings_NotifyCourseCreatorsOffset, SettingsNotifyCourseCreatorsByte, 0, 1);
                int SettingsNotifyCourseCreators = Convert.ToInt32(SettingsNotifyCourseCreatorsByte[0]);

                //Extract notify my course setting byte (offset 0x4339)
                byte[] SettingsNotifyMyCoursesByte = new byte[1];
                Array.Copy(fileBytes, Settings_NotifyMyCoursesOffset, SettingsNotifyMyCoursesByte, 0, 1);
                int SettingsNotifyMyCourses = Convert.ToInt32(SettingsNotifyMyCoursesByte[0]);

                UIstate(true);

                if (SettingsControl == 0) RADIO_DashJump.Checked = true;
                else if (SettingsControl == 1) RADIO_JumpDash.Checked = true;

                if (SettingsGamePad3DAudio == 0) RADIO_GamePad3DAudio_Off.Checked = true;
                else if (SettingsGamePad3DAudio == 1) RADIO_GamePad3DAudio_On.Checked = true;

                if (SettingsNotifyCourseCreators == 0) RADIO_NotifyCreators_Off.Checked = true;
                else if (SettingsNotifyCourseCreators == 1) RADIO_NotifyCreators_On.Checked = true;

                if (SettingsNotifyMyCourses == 0) RADIO_NotifyMyCourses_Off.Checked = true;
                else if (SettingsNotifyMyCourses == 1) RADIO_NotifyMyCourses_On.Checked = true;

                RADIO_SortByCourseNumber.Checked = true;
            }
        }

        private void BUTTON_SaveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath)) return;

            //Set file path and read data
            byte[] fileBytes = File.ReadAllBytes(currentFilePath);

            byte controlsSettingsValue = 0;
            if (RADIO_DashJump.Checked) controlsSettingsValue = 0;
            else if (RADIO_JumpDash.Checked) controlsSettingsValue = 1;
            //Insert controls byte value to the file
            fileBytes[Settings_ControlsOffset] = controlsSettingsValue;

            byte GamePad3DAudioSettingsValue = 0;
            if (RADIO_GamePad3DAudio_Off.Checked) GamePad3DAudioSettingsValue = 0;
            else if (RADIO_GamePad3DAudio_On.Checked) GamePad3DAudioSettingsValue = 1;
            //Insert GamePad 3D audio byte value to the file
            fileBytes[Settings_GamePad3DAudioOffset] = GamePad3DAudioSettingsValue;

            byte notifyCourseCreatorsValue = 0;
            if (RADIO_NotifyCreators_Off.Checked) notifyCourseCreatorsValue = 0;
            else if (RADIO_NotifyCreators_On.Checked) notifyCourseCreatorsValue = 1;
            //Insert notify creators byte value to the file
            fileBytes[Settings_NotifyCourseCreatorsOffset] = notifyCourseCreatorsValue;

            byte notifyMyCoursesValue = 0;
            if (RADIO_NotifyMyCourses_Off.Checked) notifyMyCoursesValue = 0;
            else if (RADIO_NotifyMyCourses_On.Checked) notifyMyCoursesValue = 1;
            //Insert notify my courses byte value to the file
            fileBytes[Settings_NotifyMyCoursesOffset] = notifyMyCoursesValue;

            TempCrc32Function(fileBytes);

            //Save and overwrites .dat file
            File.WriteAllBytes(currentFilePath, fileBytes);
            string caption = LanguageManager.Get("FORM_SMM1_SaveFile", "datFileSaveTitle");
            string text = LanguageManager.Get("FORM_SMM1_SaveFile", "datFileSave");
            MessageBox.Show(text + currentFilePath, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            UIstate(false);
            LISTBOX_Coursebot.Items.Clear();
        }

        private void UIstate(bool state)
        {
            GroupBox_Controls.Enabled = state;
            GroupBox_GamePad3DAudio.Enabled = state;
            GroupBox_NotifyCreators.Enabled = state;
            GroupBox_NotifyMyCourses.Enabled = state;
            GroupBox_SortCoursebot.Enabled = state;
            BUTTON_SaveFile.Enabled = state;
            LISTBOX_Coursebot.Enabled = state;
            BUTTON_DESC_Sort.Enabled = state;
            BUTTON_ASC_Sort.Enabled = state;
            BUTTON_RemoveCourse.Enabled = state;
            BUTTON_Cancel.Enabled = state;
            BUTTON_ConfirmSort.Enabled = state;
        }

        private void BUTTON_ASC_Sort_Click(object sender, EventArgs e)
        {
            LISTBOX_Coursebot.Items.Clear();

            if (RADIO_SortByCourseNumber.Checked)
            {
                CoursebotCoursesList = CoursebotCoursesList.OrderBy(entry => entry.CourseIndex).ToList();

                //Order by course number
                foreach (var entry in CoursebotCoursesList.OrderBy(entry => entry.CourseIndex))
                {
                    LISTBOX_Coursebot.Items.Add(entry.DisplayText);
                }
            }

            if (RADIO_SortBySlot.Checked)
            {
                CoursebotCoursesList = CoursebotCoursesList.OrderBy(entry => entry.MemoryValue).ToList();

                //Order by memory
                var sortedList = CoursebotCoursesList.OrderBy(entry => entry.MemoryValue).ToList();

                foreach (var entry in sortedList)
                {
                    LISTBOX_Coursebot.Items.Add(entry.DisplayText);
                }
            }
        }

        private void BUTTON_DESC_Sort_Click(object sender, EventArgs e)
        {
                        LISTBOX_Coursebot.Items.Clear();

            if (RADIO_SortByCourseNumber.Checked)
            {
                CoursebotCoursesList = CoursebotCoursesList.OrderByDescending(entry => entry.CourseIndex).ToList();

                //Order by course number
                foreach (var entry in CoursebotCoursesList.OrderByDescending(entry => entry.CourseIndex))
                {
                    LISTBOX_Coursebot.Items.Add(entry.DisplayText);
                }
            }

            if (RADIO_SortBySlot.Checked)
            {
                CoursebotCoursesList = CoursebotCoursesList.OrderByDescending(entry => entry.MemoryValue).ToList();

                //Order by memory
                var sortedList = CoursebotCoursesList.OrderByDescending(entry => entry.MemoryValue).ToList();

                foreach (var entry in sortedList)
                {
                    LISTBOX_Coursebot.Items.Add(entry.DisplayText);
                }
            }
        }

        private void BUTTON_Cancel_Click(object sender, EventArgs e)
        {
            UIstate(false);
            LISTBOX_Coursebot.Items.Clear();
        }

        private void BUTTON_ConfirmSort_Click(object sender, EventArgs e)
        {
            byte[] fileBytes = File.ReadAllBytes(currentFilePath);

            //Rewrite coursebot section
            for (int i = 0; i < 120; i++)
            {
                fileBytes[CoursebotStartOffset + i] = CoursebotCoursesList[i].MemoryValue;
            }

            TempCrc32Function(fileBytes);

            File.WriteAllBytes(currentFilePath, fileBytes);
            MessageBox.Show("Order overwrited\n" + currentFilePath, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

            UpdateListBoxCoursebot(fileBytes);
        }

        private void BUTTON_RemoveCourse_Click(object sender, EventArgs e)
        {
            if (LISTBOX_Coursebot.SelectedIndices.Count == 0)
            {
                MessageBox.Show("You have to select at least one level to remove", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            byte[] fileBytes = File.ReadAllBytes(currentFilePath);

            foreach (int selectedIndex in LISTBOX_Coursebot.SelectedIndices)
            {
                var entry = CoursebotCoursesList[selectedIndex];
                int offset = CoursebotStartOffset + selectedIndex;

                entry.MemoryValue = 0xFF;
                fileBytes[offset] = 0xFF;
                entry.DisplayText = null;

                entry.DisplayText = ListBoxText(entry.CourseIndex, entry.MemoryValue);
            }

            TempCrc32Function(fileBytes);

            File.WriteAllBytes(currentFilePath, fileBytes);
            MessageBox.Show("Course Removed from coursebot\n" + currentFilePath, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

            UpdateListBoxCoursebot(fileBytes);
        }

        //Actual plan is to create this as a global function but I don't exactly know right now how to use modules here in C#
        private void TempCrc32Function(byte[] fileBytes)
        {
            //Calculate and write the 4 bytes CRC-32 checksum on offsets from 0x08 to 0x0B
            Crc32 crc32 = new Crc32();
            byte[] checksum = crc32.ComputeChecksumBytes(fileBytes, 0x10, fileBytes.Length - 0x10);
            Array.Reverse(checksum); //Parse to big-endian order
            Array.Copy(checksum, 0, fileBytes, 0x08, 4);
        }

        private void UpdateListBoxCoursebot(byte[] fileBytes)
        {
            //Update LISTBOX_Coursebot with overwrited data
            LISTBOX_Coursebot.Items.Clear();
            CoursebotCoursesList.Clear();

            for (int i = 0; i < 120; i++)
            {
                int offset = CoursebotStartOffset + i;
                byte memoryValue = fileBytes[offset];
                string itemText = null;

                itemText = ListBoxText(i, memoryValue);

                CoursebotCoursesList.Add(new CoursebotEntry
                {
                    CourseIndex = i,
                    MemoryValue = memoryValue,
                    DisplayText = itemText
                });

                LISTBOX_Coursebot.Items.Add(itemText);
            }
        }

        void RefreshCoursebotList()
        {
            LISTBOX_Coursebot.Items.Clear();

            for (int i = 0; i < CoursebotCoursesList.Count; i++)
            {
                var entry = CoursebotCoursesList[i];
                entry.DisplayText = ListBoxText(entry.CourseIndex, entry.MemoryValue);
                LISTBOX_Coursebot.Items.Add(entry.DisplayText);
            }
        }

        string ListBoxText(int courseIndex, int memoryValue)
        {
            string CoursebotWorld = LanguageManager.Get("FORM_SMM1_SaveFile", "LISTBOX_CoursebotWorld");
            string CoursebotSlot = LanguageManager.Get("FORM_SMM1_SaveFile", "LISTBOX_CoursebotSlot");
            string CoursebotMemory = LanguageManager.Get("FORM_SMM1_SaveFile", "LISTBOX_CoursebotMemory");
            string CoursebotNoData = LanguageManager.Get("FORM_SMM1_SaveFile", "LISTBOX_CoursebotNoData");

            if (memoryValue == 0xFF) {
                string itemText2 = $"{CoursebotNoData}    " +
                $"course{courseIndex:000}    " +
                $"{CoursebotMemory} 0x{memoryValue:X2} ({memoryValue})";

                return itemText2;
            }
            else {
                string itemText1 = $"{CoursebotWorld}{(memoryValue / 4) + 1}-{(memoryValue % 4) + 1}\t" +
                $"{CoursebotSlot} {memoryValue + 1:000}: course{courseIndex:000}   " +
                $"{CoursebotMemory} 0x{memoryValue:X2} ({memoryValue})";
                
                return itemText1;
            }
        }
    }
}