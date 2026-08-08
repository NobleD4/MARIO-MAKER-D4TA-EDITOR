using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMM_D4TA_EDITOR
{
    public static class SMM1FileFormats
    {
        public const string KeyAccessSMM1 = "9f2b4678";

        public const ushort CourseDateYearStartOffset = 0x10;
        public const ushort CourseDateYearEndOffset = 0x11;
        public const ushort CourseDateMonthOffset = 0x12; //01 to 0C
        public const ushort CourseDateDayOffset = 0x13; //01 to 1F
        public const ushort CourseDateHourOffset = 0x14; //00 to 17
        public const ushort CourseDateMinuteOffset = 0x15; //00 to 3B

        //ID: PRFX-SFX1-SFX2-SFX3 (PREFIX-SUBFFIX)
        public const ushort CourseIDsuffixStartOffset = 0x1A;
        public const ushort CourseIDsuffixEndOffset = 0x1F;

        public const ushort CourseUpdatePhysicsOffset = 0x27; //There are physics from 00 to 07

        public const ushort CourseNameStartOffset = 0x28;
        public const ushort CourseNameEndOffset = 0x67;

        public const ushort CourseMiiOffset = 0x78;
        public const ushort CourseMiiSize = 96;
        public const ushort CourseCreatorStartOffset = 0x92;

        //VALUES: [4D 31 = M1] [4D 33 = M3] [4D 57 = MW] [57 55 = WU]
        public const ushort CourseStyleStartOffset = 0x6A;
        public const ushort CourseStyleEndOffset = 0x6B;

        public const ushort CourseTimerStartOffset = 0x70;
        public const ushort CourseTimerEndOffset = 0x71;

        public const ushort CourseScrollSettingsOffset = 0x72;

        public const ushort CourseLengthStartOffset = 0x76;
        public const ushort CourseLengthEndOffset = 0x77;

        public const ushort OfficialCourseStatusOffset = 0x17; //00 to 08 or 0x1D
        public const ushort DownloadedCourseOffset = 0x20;
        public const ushort RemovedCourseOffset = 0x21;
        public const ushort UploadedCourseOffset = 0x6E;
        public const ushort ClearCheckOffset = 0x6F;

        //VALUES: 00 = Ground, 01 Underground, 02 Castle, 03 Airship, 04 Underwater, 05 Ghost house
        public const ushort CourseThemeOffset = 0x6D;

        public const ushort CourseCountryOffset = 0xDB; //From 000 to 195 represents a country

        public const ushort CourseFirstItemOffset = 0x108;
        public const int CourseLastItemOffset = 0x145EF;

        public const int CourseFirstSoundOffset = 0x145F0;
        public const int CourseLastSoundOffset = 0x14F4F;

        static public void ReadSMM1Course(ref byte[] tmpfileBytes,
            ref ushort CourseDateYear, ref ushort CourseDateMonth, ref ushort CourseDateDay, ref ushort CourseDateHour, ref ushort CourseDateMinute,
            ref ushort CourseUpdatePhysics,
            ref string CourseID, ref string CourseName, ref string CourseStyle,
            ref ushort CourseTheme, ref ushort CourseTimer, ref ushort CourseScroll, ref ushort CourseLength,
            ref string CourseCreator, ref ushort CourseCountry,
            ref string LastItemPlaced, ref string LastSFXplaced)
        {
            //Set file path and read data
            //currentFilePath = OpenFileDialog_cdtFile.FileName;
            //tmpfileBytes = File.ReadAllBytes(currentFilePath);

            //Extract date year bytes (from offset 0x10 to 0x11)
            int CourseDateYearBytesLength = CourseDateYearEndOffset - CourseDateYearStartOffset + 1;
            byte[] CourseDateYearBytes = new byte[CourseDateYearBytesLength];
            Array.Copy(tmpfileBytes, CourseDateYearStartOffset, CourseDateYearBytes, 0, CourseDateYearBytesLength);
            CourseDateYear = (ushort)((CourseDateYearBytes[0] << 8) | CourseDateYearBytes[1]);

            //Extract date month bytes offset 0x12)
            CourseDateMonth = ExctractBytesFromOffset(tmpfileBytes, CourseDateMonthOffset);

            //Extract date day bytes offset 0x13)
            CourseDateDay = ExctractBytesFromOffset(tmpfileBytes, CourseDateDayOffset);

            //Extract date hour bytes offset 0x14)
            CourseDateHour = ExctractBytesFromOffset(tmpfileBytes, CourseDateHourOffset);

            //Extract date minute bytes offset 0x15)
            CourseDateMinute = ExctractBytesFromOffset(tmpfileBytes, CourseDateMinuteOffset); //This one used to have bytes from month for some reason before making function

            //Extract course physics setting byte (offset 0x27)
            CourseUpdatePhysics = ExctractBytesFromOffset(tmpfileBytes, CourseUpdatePhysicsOffset);

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
            CourseID = $"{prefix}{CourseIDsuffix:X12}";

            //Extract course name bytes (from offset 0x28 to 0x67)
            int CourseNameBytesLength = CourseNameEndOffset - CourseNameStartOffset + 1;
            byte[] CourseNameBytes = new byte[CourseNameBytesLength];
            Array.Copy(tmpfileBytes, CourseNameStartOffset, CourseNameBytes, 0, CourseNameBytesLength);
            Array.Reverse(CourseNameBytes); //For some reason reversing this displays correctly chars
                                            //Convert bytes to a char array using UTF-16LE encode
            char[] charArray = Encoding.Unicode.GetString(CourseNameBytes).TrimEnd('\0').ToArray();
            Array.Reverse(charArray); //To make sure the course name is not reversed
            CourseName = new string(charArray); //CourseName works!

            //Extract course style bytes (from offset 0x6A to 0x6B)
            int CourseStyleBytesLength = CourseStyleEndOffset - CourseStyleStartOffset + 1;
            byte[] CourseStyleBytes = new byte[CourseStyleBytesLength];
            Array.Copy(tmpfileBytes, CourseStyleStartOffset, CourseStyleBytes, 0, CourseStyleBytesLength);
            //Convert bytes to string using ASCII encode
            CourseStyle = Encoding.ASCII.GetString(CourseStyleBytes);

            //Extract course theme setting byte (offset 0x6D)
            CourseTheme = ExctractBytesFromOffset(tmpfileBytes, CourseThemeOffset);

            //Extract course timer bytes (from offset 0x70 to 0x71)
            int CourseTimerBytesLength = CourseTimerEndOffset - CourseTimerStartOffset + 1;
            byte[] CourseTimerBytes = new byte[CourseTimerBytesLength];
            Array.Copy(tmpfileBytes, CourseTimerStartOffset, CourseTimerBytes, 0, CourseTimerBytesLength);
            CourseTimer = (ushort)((CourseTimerBytes[0] << 8) | CourseTimerBytes[1]);

            //Extract course autoscroll setting byte (offset 0x72)
            CourseScroll = ExctractBytesFromOffset(tmpfileBytes, CourseScrollSettingsOffset);

            //Extract course length bytes (from offset 0x76 to 0x77)
            int CourseLengthBytesLENGTH = CourseLengthEndOffset - CourseLengthStartOffset + 1;
            byte[] CourseLengthBytes = new byte[CourseLengthBytesLENGTH];
            Array.Copy(tmpfileBytes, CourseLengthStartOffset, CourseLengthBytes, 0, CourseLengthBytesLENGTH);
            CourseLength = (ushort)((CourseLengthBytes[0] << 8) | CourseLengthBytes[1]);

            //Extract course creator bytes (from offset 0x92 to ...)

            //TRUST ME, IF THE "+ 1" IS IN THIS LINE INSTEAD OF NEXT ONE CRASHES THANKS TO WHATEVER I DID, BUT HOPEFULLY WORKS RIGHT NOW    (May 8th 2026: I'm finally changing this part and understanding whatever I tried to did here when I had less experience)
            //Epic hardcode to add an extra index to array, because for some that previously mentioned "+ 1" works here                     (May 8th 2026: Which "+1"? I've just deleted)
            //Number 1 here because I want starts the copy on index 1 of array instead of index 0 comparing it with other chunk reads       (May 8th 2026: Bad decision, it needs to be a 0 because it's were index starts to copy bytes from "OpenFileDialog_cdtFile.FileName")
            //Right now that empty extra index is at the end of array because I need last index as 0
            //So extra index allows to have a properly encoding of first char because game reads first the char code number and then a zero, but if this zero doesn't exists encodes a totally different char
            //What if actually there's an easier way to read these little endian and big endian things and I'm complicating myself?
            //I did exactly the same thing as course name  //I was also thinking these parts could be a function because do almost the same thing with different values, but nahhhh, it works right now so I shouldn't change this

            //May 8th 2026: I completely re-wrote the creator bytes extraction, but the previous comments are still funny
            byte[] CourseCreatorBytes = new byte[20];
            Array.Copy(tmpfileBytes, CourseCreatorStartOffset, CourseCreatorBytes, 0, CourseCreatorBytes.Length);
            char[] charCreatorArray = Encoding.Unicode.GetString(CourseCreatorBytes).TrimEnd('\0').ToArray();
            CourseCreator = new string(charCreatorArray);

            //Extract creator country bytes (offset 0xDB)
            CourseCountry = ExctractBytesFromOffset(tmpfileBytes, CourseCountryOffset);

            const int Jump0x20 = 0x20;  //Basically because there's a 0x20 sized space between each item placed
            int lastItemOffset = -1; //Will throw a -1 if this value doesn't change
            int itemID = -1;

            lastItemOffset = GetLastPlacedOffset(tmpfileBytes, CourseFirstItemOffset, CourseLastItemOffset, Jump0x20, 0x00, true);
            itemID = GetLastPlacedOffset(tmpfileBytes, CourseFirstItemOffset, CourseLastItemOffset, Jump0x20, 0x00, false);

            string lastItemPlacedLang = LanguageManager.Get("FORM_Main", "LABEL_LastItemPlaced");
            string lastItemOffsetLang = LanguageManager.Get("FORM_Main", "LABEL_LastItemOffset");

            if (itemID != -1)
            {
                LastItemPlaced = $"{lastItemPlacedLang} {itemID:000} (0x{itemID:X2})    "
                + $"{lastItemOffsetLang} 0x{lastItemOffset:X2}";
            }
            else
            {
                string textNoData = LanguageManager.Get("FORM_Main", "msgNoData");
                LastItemPlaced = $"{lastItemPlacedLang} {textNoData}    "
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
                LastSFXplaced = $"{lastSFXplacedLang} {SoundID:000} (0x{SoundID:X2})    "
                + $"{lastSFXoffsetLang} 0x{lastSFXoffset:X2}";
            }
            else
            {
                string textNoData = LanguageManager.Get("FORM_Main", "msgNoData");
                LastSFXplaced = $"{lastSFXplacedLang} {textNoData}    "
                + $"{lastSFXoffsetLang} {textNoData}";
            }
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
