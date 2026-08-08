using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM_D4TA_EDITOR
{
    public static class SMM1FileFormats
    {
        public const string KeyAccessSMM1 = "9f2b4678";

        public const byte CRC32FileOffset = 0x08;
        public const byte CRC32FileSize = 4;

        public const byte CourseDateYearStartOffset = 0x10;
        public const byte CourseDateYearEndOffset = 0x11;
        public const byte CourseDateMonthOffset = 0x12; //01 to 0C
        public const byte CourseDateDayOffset = 0x13; //01 to 1F
        public const byte CourseDateHourOffset = 0x14; //00 to 17
        public const byte CourseDateMinuteOffset = 0x15; //00 to 3B

        //ID: PRFX-SFX1-SFX2-SFX3
        public const byte CourseIDsuffixStartOffset = 0x1A;
        public const byte CourseIDsuffixEndOffset = 0x1F;

        public const byte CourseUpdatePhysicsOffset = 0x27; //There are physics from 00 to 07

        public const byte CourseNameStartOffset = 0x28;
        public const byte CourseNameEndOffset = 0x67;

        public const byte CourseMiiOffset = 0x78;
        public const byte CourseMiiSize = 96;
        public const byte CourseCreatorStartOffset = 0x92;

        //VALUES: [4D 31 = M1] [4D 33 = M3] [4D 57 = MW] [57 55 = WU]
        public const byte CourseStyleStartOffset = 0x6A;
        public const byte CourseStyleEndOffset = 0x6B;

        public const byte CourseTimerStartOffset = 0x70;
        public const byte CourseTimerEndOffset = 0x71;

        public const byte CourseScrollSettingsOffset = 0x72;

        public const byte CourseLengthStartOffset = 0x76;
        public const byte CourseLengthEndOffset = 0x77;

        public const byte OfficialCourseStatusOffset = 0x17; //00 to 08 or 0x1D
        public const byte DownloadedCourseOffset = 0x20;
        public const byte RemovedCourseOffset = 0x21;
        public const byte UploadedCourseOffset = 0x6E;
        public const byte ClearCheckOffset = 0x6F;

        //VALUES: 00 = Ground, 01 Underground, 02 Castle, 03 Airship, 04 Underwater, 05 Ghost house
        public const byte CourseThemeOffset = 0x6D;

        public const byte CourseCountryOffset = 0xDB; //From 000 to 195 represents a country

        public const ushort CourseFirstItemOffset = 0x108;
        public const int CourseLastItemOffset = 0x145EF;

        public const int CourseFirstSoundOffset = 0x145F0;
        public const int CourseLastSoundOffset = 0x14F4F;

        static public void ReadSMM1Course(ref byte[] tmpfileBytes,
            ref NumericUpDown CourseDateYear, ref NumericUpDown CourseDateMonth, ref NumericUpDown CourseDateDay,
            ref NumericUpDown CourseDateHour, ref NumericUpDown CourseDateMinute,
            ref CheckBox SetDateTimeNow,
            ref ComboBox CourseUpdatePhysics,
            ref TextBox CourseIDprefix, ref TextBox CourseIDsuffix1, ref TextBox CourseIDsuffix2, ref TextBox CourseIDsuffix3,
            ref TextBox CourseName, ref ComboBox CourseStyleSettings,
            ref ComboBox CourseTheme, ref NumericUpDown CourseTimer, ref ComboBox CourseScroll,
            ref NumericUpDown CourseLength,
            ref TextBox CourseCreator, ref NumericUpDown CourseCountry,
            ref Label LastItemPlaced, ref Label LastSFXplaced,
            ref ComboBox OfficialCourse,
            ref CheckBox CourseStatusDownloaded,
            ref CheckBox CourseStatusUploaded,
            ref CheckBox CourseStatusRemoved,
            ref Label ClearCheckStatus
        )
        {
            //Set file path and read data
            //currentFilePath = OpenFileDialog_cdtFile.FileName;
            //tmpfileBytes = File.ReadAllBytes(currentFilePath);

            //Extract date year bytes (from offset 0x10 to 0x11)
            int CourseDateYearBytesLength = CourseDateYearEndOffset - CourseDateYearStartOffset + 1;
            byte[] CourseDateYearBytes = new byte[CourseDateYearBytesLength];
            Array.Copy(tmpfileBytes, CourseDateYearStartOffset, CourseDateYearBytes, 0, CourseDateYearBytesLength);
            CourseDateYear.Value = (ushort)((CourseDateYearBytes[0] << 8) | CourseDateYearBytes[1]);

            //Extract date month bytes offset 0x12)
            CourseDateMonth.Value = ExctractBytesFromOffset(tmpfileBytes, CourseDateMonthOffset);

            //Extract date day bytes offset 0x13)
            CourseDateDay.Value = ExctractBytesFromOffset(tmpfileBytes, CourseDateDayOffset);

            //Extract date hour bytes offset 0x14)
            CourseDateHour.Value = ExctractBytesFromOffset(tmpfileBytes, CourseDateHourOffset);

            //Extract date minute bytes offset 0x15)
            CourseDateMinute.Value = ExctractBytesFromOffset(tmpfileBytes, CourseDateMinuteOffset); //This one used to have bytes from month for some reason before making function

            //Extract course physics setting byte (offset 0x27)
            CourseUpdatePhysics.SelectedIndex = ExctractBytesFromOffset(tmpfileBytes, CourseUpdatePhysicsOffset);

            //Extract course ID suffix byte (from offset 0x1A to 0x1F)
            int CourseIDsuffixbytesLength = CourseIDsuffixEndOffset - CourseIDsuffixStartOffset + 1;
            byte[] CourseIDsuffixBytes = new byte[CourseIDsuffixbytesLength];
            Array.Copy(tmpfileBytes, CourseIDsuffixStartOffset, CourseIDsuffixBytes, 0, CourseIDsuffixbytesLength);
            //Fill to 8 bytes
            byte[] paddedBytes = new byte[8];
            Array.Reverse(CourseIDsuffixBytes);
            Array.Copy(CourseIDsuffixBytes, paddedBytes, CourseIDsuffixbytesLength);
            ulong CourseIDsuffix = BitConverter.ToUInt64(paddedBytes, 0);
            string prefix = GenerateCourseIdPrefix(CourseIDsuffix);
            string CourseID = $"{prefix}{CourseIDsuffix:X12}";

            CourseIDprefix.Text = CourseID.Substring(0, 4);
            CourseIDsuffix1.Text = CourseID.Substring(4, 4);
            CourseIDsuffix2.Text = CourseID.Substring(8, 4);
            CourseIDsuffix3.Text = CourseID.Substring(12, 4);

            //Extract course name bytes (from offset 0x28 to 0x67)
            int CourseNameBytesLength = CourseNameEndOffset - CourseNameStartOffset + 1;
            byte[] CourseNameBytes = new byte[CourseNameBytesLength];
            Array.Copy(tmpfileBytes, CourseNameStartOffset, CourseNameBytes, 0, CourseNameBytesLength);
            Array.Reverse(CourseNameBytes); //For some reason reversing this displays correctly chars
                                            //Convert bytes to a char array using UTF-16LE encode
            char[] charArray = Encoding.Unicode.GetString(CourseNameBytes).TrimEnd('\0').ToArray();
            Array.Reverse(charArray); //To make sure the course name is not reversed
            CourseName.Text = new string(charArray); //CourseName works!

            //Extract course style bytes (from offset 0x6A to 0x6B)
            int CourseStyleBytesLength = CourseStyleEndOffset - CourseStyleStartOffset + 1;
            byte[] CourseStyleBytes = new byte[CourseStyleBytesLength];
            Array.Copy(tmpfileBytes, CourseStyleStartOffset, CourseStyleBytes, 0, CourseStyleBytesLength);
            //Convert bytes to string using ASCII encode
            string CourseStyle = Encoding.ASCII.GetString(CourseStyleBytes);

            if (CourseStyle == "M1") CourseStyleSettings.SelectedIndex = 0;
            else if (CourseStyle == "M3") CourseStyleSettings.SelectedIndex = 1;
            else if (CourseStyle == "MW") CourseStyleSettings.SelectedIndex = 2;
            else if (CourseStyle == "WU") CourseStyleSettings.SelectedIndex = 3;
            else CourseStyle = "M1";

            //Extract course theme setting byte (offset 0x6D)
            CourseTheme.SelectedIndex = ExctractBytesFromOffset(tmpfileBytes, CourseThemeOffset);

            //Extract course timer bytes (from offset 0x70 to 0x71)
            int CourseTimerBytesLength = CourseTimerEndOffset - CourseTimerStartOffset + 1;
            byte[] CourseTimerBytes = new byte[CourseTimerBytesLength];
            Array.Copy(tmpfileBytes, CourseTimerStartOffset, CourseTimerBytes, 0, CourseTimerBytesLength);
            CourseTimer.Value = (ushort)((CourseTimerBytes[0] << 8) | CourseTimerBytes[1]);

            //Extract course autoscroll setting byte (offset 0x72)
            CourseScroll.SelectedIndex = ExctractBytesFromOffset(tmpfileBytes, CourseScrollSettingsOffset);

            //Extract course length bytes (from offset 0x76 to 0x77)
            int CourseLengthBytesLENGTH = CourseLengthEndOffset - CourseLengthStartOffset + 1;
            byte[] CourseLengthBytes = new byte[CourseLengthBytesLENGTH];
            Array.Copy(tmpfileBytes, CourseLengthStartOffset, CourseLengthBytes, 0, CourseLengthBytesLENGTH);
            CourseLength.Value = (ushort)((CourseLengthBytes[0] << 8) | CourseLengthBytes[1]);
            //CourseLengthHex.Text = $"0X{Convert.ToInt32(NUMERIC_Length.Value):X3}";

            //Extract course creator bytes (from offset 0x92 to ...)

            //TRUST ME, IF THE "+ 1" IS IN THIS LINE INSTEAD OF NEXT ONE CRASHES THANKS TO WHATEVER I DID, BUT HOPEFULLY WORKS RIGHT NOW    (May 8th 2026: I'm finally changing this part and understanding whatever I tried to did here when I had less experience)
            //Epic hardcode to add an extra index to array, because for some that previously mentioned "+ 1" works here                     (May 8th 2026: Which "+1"? I've just deleted)
            //Number 1 here because I want starts the copy on index 1 of array instead of index 0 comparing it with other chunk reads       (May 8th 2026: Bad decision, it needs to be a 0 because it's were index starts to copy bytes from "OpenFileDialog_cdtFile.FileName")
            //Right now that empty extra index is at the end of array because I need last index as 0
            //So extra index allows to have a properly encoding of first char because game reads first the char code number and then a zero, but if this zero doesn't exists encodes a totally different char
            //What if actually there's an easier way to read these little endian and big endian things and I'm complicating myself?
            //I did exactly the same thing as course name  //I was also thinking these parts could be a function because do almost the same thing with different values, but nahhhh, it works right now so I shouldn't change this

            //May 8th 2026: I completely re-wrote the creator bytes extraction, but the previous comments are still funny
            //Aug 8th 2026: I would like to know the exact date from those previous comments, I'm like ??? right now
            byte[] CourseCreatorBytes = new byte[20];
            Array.Copy(tmpfileBytes, CourseCreatorStartOffset, CourseCreatorBytes, 0, CourseCreatorBytes.Length);
            char[] charCreatorArray = Encoding.Unicode.GetString(CourseCreatorBytes).TrimEnd('\0').ToArray();
            CourseCreator.Text = new string(charCreatorArray);

            //Extract creator country bytes (offset 0xDB)
            CourseCountry.Value = ExctractBytesFromOffset(tmpfileBytes, CourseCountryOffset);

            const int Jump0x20 = 0x20;  //Basically because there's a 0x20 sized space between each item placed
            int lastItemOffset = -1; //Will throw a -1 if this value doesn't change
            int itemID = -1;

            lastItemOffset = GetLastPlacedOffset(tmpfileBytes, CourseFirstItemOffset, CourseLastItemOffset, Jump0x20, 0x00, true);
            itemID = GetLastPlacedOffset(tmpfileBytes, CourseFirstItemOffset, CourseLastItemOffset, Jump0x20, 0x00, false);

            string lastItemPlacedLang = LanguageManager.Get("FORM_Main", "LABEL_LastItemPlaced");
            string lastItemOffsetLang = LanguageManager.Get("FORM_Main", "LABEL_LastItemOffset");

            if (itemID != -1)
            {
                LastItemPlaced.Text = $"{lastItemPlacedLang} {itemID:000} (0x{itemID:X2})    "
                + $"{lastItemOffsetLang} 0x{lastItemOffset:X2}";
            }
            else
            {
                string textNoData = LanguageManager.Get("FORM_Main", "msgNoData");
                LastItemPlaced.Text = $"{lastItemPlacedLang} {textNoData}    "
                + $"{lastItemOffsetLang} {textNoData}";
            }

            const int Jump0x08 = 0x08;  //Basically because there's a 0x08 sized space between each sound placed
            int lastSFXoffset = -1; //Will throw a -1 if this value doesn't change
            int SoundID = -1;

            lastSFXoffset = GetLastPlacedOffset(tmpfileBytes, CourseFirstSoundOffset, CourseLastSoundOffset, Jump0x08, 0xFF, true);
            SoundID = GetLastPlacedOffset(tmpfileBytes, CourseFirstSoundOffset, CourseLastSoundOffset, Jump0x08, 0xFF, false);

            string lastSFXplacedLang = LanguageManager.Get("FORM_Main", "LABEL_LastSFXplaced");
            string lastSFXoffsetLang = LanguageManager.Get("FORM_Main", "LABEL_LastSFXoffset");

            if (SoundID != -1)
            {
                LastSFXplaced.Text = $"{lastSFXplacedLang} {SoundID:000} (0x{SoundID:X2})    "
                + $"{lastSFXoffsetLang} 0x{lastSFXoffset:X2}";
            }
            else
            {
                string textNoData = LanguageManager.Get("FORM_Main", "msgNoData");
                LastSFXplaced.Text = $"{lastSFXplacedLang} {textNoData}    "
                + $"{lastSFXoffsetLang} {textNoData}";
            }

            string clearCheckStatus0 = LanguageManager.Get("FORM_Main", "ClearCheckStatus0");
            string clearCheckStatus1 = LanguageManager.Get("FORM_Main", "ClearCheckStatus1");
            if (tmpfileBytes[ClearCheckOffset] == 0x01) ClearCheckStatus.Text = clearCheckStatus1;
            else ClearCheckStatus.Text = clearCheckStatus0;

            if (tmpfileBytes[OfficialCourseStatusOffset] == 1) OfficialCourse.SelectedIndex = 1;
            else if (tmpfileBytes[OfficialCourseStatusOffset] == 2) OfficialCourse.SelectedIndex = 2;
            else if (tmpfileBytes[OfficialCourseStatusOffset] == 3) OfficialCourse.SelectedIndex = 3;
            else if (tmpfileBytes[OfficialCourseStatusOffset] == 4) OfficialCourse.SelectedIndex = 4;
            else if (tmpfileBytes[OfficialCourseStatusOffset] == 5) OfficialCourse.SelectedIndex = 5;
            else if (tmpfileBytes[OfficialCourseStatusOffset] == 6) OfficialCourse.SelectedIndex = 6;
            else if (tmpfileBytes[OfficialCourseStatusOffset] == 7) OfficialCourse.SelectedIndex = 7;
            else if (tmpfileBytes[OfficialCourseStatusOffset] == 8) OfficialCourse.SelectedIndex = 8;
            else if (tmpfileBytes[OfficialCourseStatusOffset] == 0x1D) OfficialCourse.SelectedIndex = 9;
            else OfficialCourse.SelectedIndex = 0;

            if (tmpfileBytes[DownloadedCourseOffset] == 0x01) CourseStatusDownloaded.Checked = true;
            else CourseStatusDownloaded.Checked = false;
            if (tmpfileBytes[UploadedCourseOffset] == 0x01) CourseStatusUploaded.Checked = true;
            else CourseStatusUploaded.Checked = false;
            if (tmpfileBytes[RemovedCourseOffset] == 0x01) CourseStatusRemoved.Checked = true;
            else CourseStatusRemoved.Checked = false;
        }

        static public void WriteSMM1Course(ref string currentFilePath,
            ref NumericUpDown CourseDateYear, ref NumericUpDown CourseDateMonth, ref NumericUpDown CourseDateDay,
            ref NumericUpDown CourseDateHour, ref NumericUpDown CourseDateMinute,
            ref ComboBox CourseUpdatePhysics,
            ref TextBox CourseIDprefix, ref TextBox CourseIDsuffix1, ref TextBox CourseIDsuffix2, ref TextBox CourseIDsuffix3,
            ref TextBox CourseName, ref ComboBox CourseStyleSettings,
            ref ComboBox CourseTheme, ref NumericUpDown CourseTimer, ref ComboBox CourseScroll,
            ref NumericUpDown CourseLength,
            ref string CourseCreator, ref NumericUpDown CourseCountry,
            ref ComboBox OfficialCourse,
            ref CheckBox CourseStatusDownloaded,
            ref CheckBox CourseStatusUploaded,
            ref CheckBox CourseStatusRemoved,
            ref CheckBox ClearCheckStatus
        )
        {
            //Set file path and read data
            byte[] tmpfileBytes = File.ReadAllBytes(currentFilePath);

            ushort NewCourseDateYear = (ushort)CourseDateYear.Value;
            tmpfileBytes[CourseDateYearStartOffset] = (byte)(NewCourseDateYear >> 8);
            tmpfileBytes[CourseDateYearEndOffset] = (byte)(NewCourseDateYear & 0xFF);

            ushort NewCourseDateMonth = (ushort)CourseDateMonth.Value;
            tmpfileBytes[CourseDateMonthOffset] = (byte)(NewCourseDateMonth);

            ushort NewCourseDateDay = (ushort)CourseDateDay.Value;
            tmpfileBytes[CourseDateDayOffset] = (byte)(NewCourseDateDay);

            ushort NewCourseDateHour = (ushort)CourseDateHour.Value;
            tmpfileBytes[CourseDateHourOffset] = (byte)(NewCourseDateHour);

            ushort NewCourseDateMinute = (ushort)CourseDateMinute.Value;
            tmpfileBytes[CourseDateMinuteOffset] = (byte)(NewCourseDateMinute);

            byte physicsValue = 0;
            if (CourseUpdatePhysics.SelectedIndex == 1) physicsValue = 1;
            else if (CourseUpdatePhysics.SelectedIndex == 2) physicsValue = 2;
            else if (CourseUpdatePhysics.SelectedIndex == 3) physicsValue = 3;
            else if (CourseUpdatePhysics.SelectedIndex == 4) physicsValue = 4;
            else if (CourseUpdatePhysics.SelectedIndex == 5) physicsValue = 5;
            else if (CourseUpdatePhysics.SelectedIndex == 6) physicsValue = 6;
            else if (CourseUpdatePhysics.SelectedIndex == 7) physicsValue = 7;
            else physicsValue = 0;
            //Insert physics byte value to the file
            tmpfileBytes[CourseUpdatePhysicsOffset] = physicsValue;

            //This writes the 6 suffix bytes for course ID
            byte[] NewIDsuffix1Bytes = new byte[2];
            byte[] NewIDsuffix2Bytes = new byte[2];
            byte[] NewIDsuffix3Bytes = new byte[2];
            int.TryParse(CourseIDsuffix1.Text, System.Globalization.NumberStyles.HexNumber, null, out int _CourseIDsuffix1);
            int.TryParse(CourseIDsuffix2.Text, System.Globalization.NumberStyles.HexNumber, null, out int _CourseIDsuffix2);
            int.TryParse(CourseIDsuffix3.Text, System.Globalization.NumberStyles.HexNumber, null, out int _CourseIDsuffix3);
            //I think is easier right now to add the offsets manually for this specifically
            tmpfileBytes[CourseIDsuffixStartOffset] = (byte)(_CourseIDsuffix1 >> 8);
            tmpfileBytes[0x1B] = (byte)(_CourseIDsuffix1 & 0xFF);
            tmpfileBytes[0x1C] = (byte)(_CourseIDsuffix2 >> 8);
            tmpfileBytes[0x1D] = (byte)(_CourseIDsuffix2 & 0xFF);
            tmpfileBytes[0x1E] = (byte)(_CourseIDsuffix3 >> 8);
            tmpfileBytes[CourseIDsuffixEndOffset] = (byte)(_CourseIDsuffix3 & 0xFF);

            //To write a new course name correctly, I'm doing in reverse whatever I did to read
            int NewCourseNameBytesLength = CourseNameEndOffset - CourseNameStartOffset + 1;
            char[] charArray = CourseName.Text.ToArray();
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
            Array.Copy(paddedNameBytes, 0, tmpfileBytes, CourseNameStartOffset, 64);

            string styleValue;
            if (CourseStyleSettings.SelectedIndex == 0) styleValue = "M1";
            else if (CourseStyleSettings.SelectedIndex == 1) styleValue = "M3";
            else if (CourseStyleSettings.SelectedIndex == 2) styleValue = "MW";
            else if (CourseStyleSettings.SelectedIndex == 3) styleValue = "WU";
            else styleValue = "M1";
            //Insert style byte value to the file
            byte[] styleBytes = Encoding.ASCII.GetBytes(styleValue);
            tmpfileBytes[CourseStyleStartOffset] = styleBytes[0];
            tmpfileBytes[CourseStyleEndOffset] = styleBytes[1];

            byte themeValue = 0;
            if (CourseTheme.SelectedIndex == 1) themeValue = 1;
            else if (CourseTheme.SelectedIndex == 2) themeValue = 2;
            else if (CourseTheme.SelectedIndex == 3) themeValue = 3;
            else if (CourseTheme.SelectedIndex == 4) themeValue = 4;
            else if (CourseTheme.SelectedIndex == 5) themeValue = 5;
            else themeValue = 0;
            //Insert theme byte value to the file
            tmpfileBytes[CourseThemeOffset] = themeValue;

            ushort NewCourseTimer = (ushort)CourseTimer.Value;
            tmpfileBytes[CourseTimerStartOffset] = (byte)(NewCourseTimer >> 8);
            tmpfileBytes[CourseTimerEndOffset] = (byte)(NewCourseTimer & 0xFF);

            byte scrollValue = 0;
            if (CourseScroll.SelectedIndex == 1) scrollValue = 1;
            else if (CourseScroll.SelectedIndex == 2) scrollValue = 2;
            else if (CourseScroll.SelectedIndex == 3) scrollValue = 3;
            else if (CourseScroll.SelectedIndex == 4) scrollValue = 4;
            else scrollValue = 0;
            //Insert scroll byte value to the file
            tmpfileBytes[CourseScrollSettingsOffset] = scrollValue;

            ushort NewCourseLength = (ushort)CourseLength.Value;
            tmpfileBytes[CourseLengthStartOffset] = (byte)(NewCourseLength >> 8);
            tmpfileBytes[CourseLengthEndOffset] = (byte)(NewCourseLength & 0xFF);

            byte[] MiiFileBytes = Convert.FromBase64String(CourseCreator);
            Array.Copy(MiiFileBytes, 0, tmpfileBytes, CourseMiiOffset, CourseMiiSize); //Creator Mii writing instead of creator name

            ushort NewCourseCountry = (ushort)CourseCountry.Value;
            tmpfileBytes[CourseCountryOffset] = (byte)(NewCourseCountry);

            if (OfficialCourse.SelectedIndex == 1) tmpfileBytes[OfficialCourseStatusOffset] = 1;
            else if (OfficialCourse.SelectedIndex == 2) tmpfileBytes[OfficialCourseStatusOffset] = 2;
            else if (OfficialCourse.SelectedIndex == 3) tmpfileBytes[OfficialCourseStatusOffset] = 3;
            else if (OfficialCourse.SelectedIndex == 4) tmpfileBytes[OfficialCourseStatusOffset] = 4;
            else if (OfficialCourse.SelectedIndex == 5) tmpfileBytes[OfficialCourseStatusOffset] = 5;
            else if (OfficialCourse.SelectedIndex == 6) tmpfileBytes[OfficialCourseStatusOffset] = 6;
            else if (OfficialCourse.SelectedIndex == 7) tmpfileBytes[OfficialCourseStatusOffset] = 7;
            else if (OfficialCourse.SelectedIndex == 8) tmpfileBytes[OfficialCourseStatusOffset] = 8;
            else if (OfficialCourse.SelectedIndex == 9) tmpfileBytes[OfficialCourseStatusOffset] = 0x1D;
            else tmpfileBytes[OfficialCourseStatusOffset] = 0x0;

            if (CourseStatusDownloaded.Checked) tmpfileBytes[DownloadedCourseOffset] = 0x01;
            else tmpfileBytes[DownloadedCourseOffset] = 0x00;
            if (CourseStatusUploaded.Checked) tmpfileBytes[UploadedCourseOffset] = 0x01;
            else tmpfileBytes[UploadedCourseOffset] = 0x00;
            if (CourseStatusRemoved.Checked) tmpfileBytes[RemovedCourseOffset] = 0x01; //Here used to be an "UploadedCourseOffset" instead of "RemovedCourseOffset", hopefully I'm testing it to see if there's any bug
            else tmpfileBytes[RemovedCourseOffset] = 0x00;

            if (ClearCheckStatus.Checked)
            {
                tmpfileBytes[ClearCheckOffset] = 0x01;
            }
            else tmpfileBytes[ClearCheckOffset] = 0x00;

            WriteChecksumCRC32(tmpfileBytes);

            //Save and overwrites .cdt file
            File.WriteAllBytes(currentFilePath, tmpfileBytes);
            string caption = LanguageManager.Get("FORM_Main", "cdtFileSaveTitle");
            string text = LanguageManager.Get("FORM_Main", "cdtFileSave");
            MessageBox.Show(text + currentFilePath, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static public void WriteChecksumCRC32(byte[] fileBytes)
        {
            //Calculate and write the 4 bytes CRC-32 checksum on offsets from 0x08 to 0x0B
            Crc32 crc32 = new Crc32();
            byte[] checksum = crc32.ComputeChecksumBytes(fileBytes, 0x10, fileBytes.Length - 0x10);
            Array.Reverse(checksum); //Parse to big-endian order
            Array.Copy(checksum, 0, fileBytes, CRC32FileOffset, CRC32FileSize);
        }

        static public byte ExctractBytesFromOffset(byte[] fileBytes, int Offset) //I'll improve this function later
        {
            byte[] EntryByte = new byte[1];
            Array.Copy(fileBytes, Offset, EntryByte, 0, 1);
            ushort ResultByte = (ushort)(EntryByte[0]);

            return (byte)ResultByte; //Why am I creating a "Result Byte" as "ushort" type and then converting it to byte?!
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

        //Is now a function because Items and Sounds are separated inside .cdt but both works very similar
        //I used to have to do only once the for loop to get both values returned at the end
        //But after adding SFX section I have to call this function 4 times and would be epic to reduce it to 2 calls
        static int GetLastPlacedOffset(byte[] fileBytes, int FirstOffset, int LastOffset, int JumpToNextOffsetValue, int NullValueCompare, bool ReturnOffsetOrID)
        {
            int lastObjectOffset = -1; //Will throw a -1 if this value doesn't change
            int objectID = -1;

            for (int j = FirstOffset; FirstOffset < LastOffset; FirstOffset += JumpToNextOffsetValue) //Feels like use glue and tape because I don't see any "j" increment, but I made it work
            {
                bool isEmpty = true;

                //Check only a couple of times if current offset value equals entirely NullValueCompare
                for (int i = 0; i < 2; i++)
                {
                    if (fileBytes[FirstOffset + i] != NullValueCompare)
                    {
                        isEmpty = false;
                        break;
                    }
                }

                //Stop if there's an empty block
                if (isEmpty) break;

                //Update if isn't empty
                objectID = fileBytes[FirstOffset];
                lastObjectOffset = FirstOffset;
            }

            if (ReturnOffsetOrID)
            {
                return lastObjectOffset;
            }
            else
            {
                return objectID;
            }
        }
    }
}
