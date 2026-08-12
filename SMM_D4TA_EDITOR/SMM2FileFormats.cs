using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM_D4TA_EDITOR
{
    public static class SMM2FileFormats
    {
        public const byte StartY_Offset = 0x0;
        public const byte GoalY_Offset = 0x1;
        public const byte GoalX_Offset = 0x2;
        public const byte GoalX_Size = 2;

        public const byte SMM2CourseTimerOffset = 0x4;
        public const byte SMM2CourseTimerSize = 2;

        public const byte ClearConditionAmountOffset = 0x6;
        public const byte ClearConditionAmountSize = 2;

        public const byte SMM2CourseYearOffset = 0x8;
        public const byte SMM2CourseYearSize = 2;
        public const byte SMM2CourseMonthOffset = 0xA;
        public const byte SMM2CourseDayOffset = 0xB;
        public const byte SMM2CourseHourOffset = 0xC;
        public const byte SMM2CourseMinutetOffset = 0xD;

        public const byte SMM2CustomScrollSpeedOffset = 0xE; //00 to 02

        public const byte ClearConditionCategoryOffset = 0xF; //00 to 03

        public const byte CRC32ClearConditionOffset = 0x10;
        public const byte CRC32ClearConditionSize = 4;

        public const byte SMM2GameVersionOffset = 0x14;
        public const byte SMM2GameVersionSize = 4;

        public const byte SMM2ManagementFlagsOffset = 0x18; //Clear check, uploaded, removed, etc. Stored as BITS
        public const byte SMM2ManagementFlagsSize = 4;

        public const byte SMM2ClearCheckAttemptsOffset = 0x1C;
        public const byte SMM2ClearCheckAttemptsSize = 4;

        public const byte SMM2ClearCheckTimeOffset = 0x20;
        public const byte SMM2ClearCheckTimeSize = 4;

        public const byte SMM2CreationIDoffset = 0x24;
        public const byte SMM2CreationIDsize = 4;

        public const byte SMM2CourseIDoffset = 0x28;
        public const byte SMM2CourseIDsize = 8;

        public const byte SMM2GameVersionClearCheckOffset = 0x30;
        public const byte SMM2GameVersionClearChekSize = 4;

        public const byte SMM2GameStyleOffset = 0xF1;
        public const byte SMM2GameStyleSize = 2;

        public const byte SMM2CourseNameOffset = 0xF4;
        public const byte SMM2CourseNameSize = 64;

        public const short SMM2CourseDescriptionOffset = 0x136;
        public const byte SMM2CourseDescriptionSize = 150;

        static public void ReadSMM2Course(ref byte[] tmpfileBytes,
            ref NumericUpDown CourseTimer,
            ref NumericUpDown CourseDateYear, ref NumericUpDown CourseDateMonth, ref NumericUpDown CourseDateDay,
            ref NumericUpDown CourseDateHour, ref NumericUpDown CourseDateMinute,
            ref NumericUpDown ClearCheckAttempts,
            ref NumericUpDown ClearCheckTime,
            ref TextBox CourseIDsuffix1, ref TextBox CourseIDsuffix2, ref TextBox CourseIDsuffix3,
            ref ComboBox GameVersionClearCheck,
            ref ComboBox CourseStyleSettings,
            ref TextBox CourseName,
            ref TextBox CourseDescription
        )
        {
            byte[] CourseTimerBytes = new byte[SMM2CourseTimerSize];
            Array.Copy(tmpfileBytes, SMM2CourseTimerOffset, CourseTimerBytes, 0, SMM2CourseYearSize);
            CourseTimer.Value = (CourseTimerBytes[1] << 8) | CourseTimerBytes[0];

            byte[] CourseDateYearBytes = new byte[SMM2CourseYearSize];
            Array.Copy(tmpfileBytes, SMM2CourseYearOffset, CourseDateYearBytes, 0, SMM2CourseYearSize);
            CourseDateYear.Value = (CourseDateYearBytes[1] << 8) | CourseDateYearBytes[0];

            byte[] CourseDateMonthByte = new byte[1];
            Array.Copy(tmpfileBytes, SMM2CourseMonthOffset, CourseDateMonthByte, 0, 1);
            CourseDateMonth.Value = CourseDateMonthByte[0];

            byte[] CourseDateDayByte = new byte[1];
            Array.Copy(tmpfileBytes, SMM2CourseDayOffset, CourseDateDayByte, 0, 1);
            CourseDateDay.Value = CourseDateDayByte[0];

            byte[] CourseDateHourByte = new byte[1];
            Array.Copy(tmpfileBytes, SMM2CourseHourOffset, CourseDateHourByte, 0, 1);
            CourseDateHour.Value = CourseDateHourByte[0];

            byte[] CourseDateMinuteByte = new byte[1];
            Array.Copy(tmpfileBytes, SMM2CourseMinutetOffset, CourseDateMinuteByte, 0, 1);
            CourseDateMinute.Value = CourseDateMinuteByte[0];

            byte[] ClearCheckAttemptsBytes = new byte[SMM2ClearCheckAttemptsSize];
            Array.Copy(tmpfileBytes, SMM2ClearCheckAttemptsOffset, ClearCheckAttemptsBytes, 0, SMM2ClearCheckAttemptsSize);
            ClearCheckAttempts.Value = (ClearCheckAttemptsBytes[3] << 24) | (ClearCheckAttemptsBytes[2] << 16) | (ClearCheckAttemptsBytes[1] << 8) | ClearCheckAttemptsBytes[0];

            byte[] ClearCheckTimeBytes = new byte[SMM2ClearCheckTimeSize];
            Array.Copy(tmpfileBytes, SMM2ClearCheckTimeOffset, ClearCheckTimeBytes, 0, SMM2ClearCheckTimeSize);
            ClearCheckTime.Value = (ClearCheckTimeBytes[3] << 24) | (ClearCheckTimeBytes[2] << 16) | (ClearCheckTimeBytes[1] << 8) | ClearCheckTimeBytes[0];

            byte[] EntryByte = new byte[SMM2GameVersionClearChekSize];
            Array.Copy(tmpfileBytes, SMM2GameVersionClearCheckOffset, EntryByte, 0, SMM2GameVersionClearChekSize);
            ushort ResultByte = EntryByte[0]; //???

            GameVersionClearCheck.SelectedIndex = ResultByte;

            byte[] CourseStyleBytes = new byte[SMM2GameStyleSize];
            Array.Copy(tmpfileBytes, SMM2GameStyleOffset, CourseStyleBytes, 0, SMM2GameStyleSize);
            string CourseStyle = Encoding.ASCII.GetString(CourseStyleBytes);

            if (CourseStyle == "M1") CourseStyleSettings.SelectedIndex = 0;
            else if (CourseStyle == "M3") CourseStyleSettings.SelectedIndex = 1;
            else if (CourseStyle == "MW") CourseStyleSettings.SelectedIndex = 2;
            else if (CourseStyle == "WU") CourseStyleSettings.SelectedIndex = 3;
            else if (CourseStyle == "3W") CourseStyleSettings.SelectedIndex = 4;
            else CourseStyle = "M1";

            char[] charArray;

            byte[] CourseNameBytes = new byte[SMM2CourseNameSize];
            Array.Copy(tmpfileBytes, SMM2CourseNameOffset, CourseNameBytes, 0, SMM2CourseNameSize);
            charArray = Encoding.Unicode.GetString(CourseNameBytes).TrimEnd('\0').ToArray();
            CourseName.Text = new string(charArray);

            byte[] CourseDescriptionBytes = new byte[SMM2CourseDescriptionSize];
            Array.Copy(tmpfileBytes, SMM2CourseDescriptionOffset, CourseDescriptionBytes, 0, SMM2CourseDescriptionSize);
            charArray = Encoding.Unicode.GetString(CourseDescriptionBytes).TrimEnd('\0').ToArray();
            CourseDescription.Text = new string(charArray);
        }
    }
}
