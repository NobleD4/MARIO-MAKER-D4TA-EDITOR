using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMM_D4TA_EDITOR
{
    public static class SMM2FileFormats
    {
        private static readonly uint[] bcdTable = {
            0x7AB1C9D2, 0xCA750936, 0x3003E59C, 0xF261014B,
            0x2E25160A, 0xED614811, 0xF1AC6240, 0xD59272CD,
            0xF38549BF, 0x6CF5B327, 0xDA4DB82A, 0x820C435A,
            0xC95609BA, 0x19BE08B0, 0x738E2B81, 0xED3C349A,
            0x045275D1, 0xE0A73635, 0x1DEBF4DA, 0x9924B0DE,
            0x6A1FC367, 0x71970467, 0xFC55ABEB, 0x368D7489,
            0x0CC97D1D, 0x17CC441E, 0x3528D152, 0xD0129B53,
            0xE12A69E9, 0x13D1BDB7, 0x32EAA9ED, 0x42F41D1B,
            0xAEA5F51F, 0x42C5D23C, 0x7CC742ED, 0x723BA5F9,
            0xDE5B99E3, 0x2C0055A4, 0xC38807B4, 0x4C099B61,
            0xC4E4568E, 0x8C29C901, 0xE13B34AC, 0xE7C3F212,
            0xB67EF941, 0x08038965, 0x8AFD1E6A, 0x8E5341A3,
            0xA4C61107, 0xFBAF1418, 0x9B05EF64, 0x3C91734E,
            0x82EC6646, 0xFB19F33E, 0x3BDE6FE2, 0x17A84CCA,
            0xCCDF0CE9, 0x50E4135C, 0xFF2658B2, 0x3780F156,
            0x7D8F5D68, 0x517CBED1, 0x1FCDDF0D, 0x77A58C94
        };

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

        static public void DecryptSMM2Course(ref byte[] fileBytes)
        {
            const int FileSize = 0x5C000;
            const int BodyOffset = 0x10;
            const int BodySize = 0x5BFC0;
            const int FooterOffset = 0x5BFD0;

            if (fileBytes == null)
                throw new ArgumentNullException(nameof(fileBytes));

            if (fileBytes.Length != FileSize)
                throw new InvalidDataException($"Invalid BCD size: 0x{fileBytes.Length:X}.");

            //1. Read RNG state
            RandomSMM2 random = new RandomSMM2(
                ReadUInt32LE(fileBytes, FooterOffset + 0x10),
                ReadUInt32LE(fileBytes, FooterOffset + 0x14),
                ReadUInt32LE(fileBytes, FooterOffset + 0x18),
                ReadUInt32LE(fileBytes, FooterOffset + 0x1C)
            );

            //2. Read IV
            byte[] iv = new byte[16];

            Buffer.BlockCopy(fileBytes, FooterOffset, iv, 0, 16);

            //3. Read stored CMAC
            byte[] expectedCMAC = new byte[16];

            Buffer.BlockCopy( fileBytes, FooterOffset + 0x20, expectedCMAC, 0, 16);

            //4. Create AES key
            byte[] aesKey =
                CreateKey(random, bcdTable, 16);

            //5. AES-CBC decrypt
            byte[] decrypted =
                new byte[BodySize];

            using (Aes aes = Aes.Create())
            {
                aes.Key = aesKey;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.None;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    decryptor.TransformBlock(fileBytes, BodyOffset, BodySize, decrypted, 0);
                }
            }

            //6. CRC32
            uint expectedCRC = BitConverter.ToUInt32(fileBytes, 0x08);

            Crc32 crc32 = new Crc32();
            uint actualCRC = crc32.ComputeChecksum(decrypted, 0, decrypted.Length);

            if (actualCRC != expectedCRC)
            {
                throw new InvalidDataException($"Invalid CRC32. Expected 0x{expectedCRC:X8}, " + $"got 0x{actualCRC:X8}.");
            }

            //7. Create CMAC key
            //IMPORTANT:
            //Random was already used to generate aesKey
            //So, here continues after their new state
            byte[] cmacKey = CreateKey(random, bcdTable, 16);

            // 8. Calculate CMAC
            byte[] actualCMAC = AESCMAC(decrypted, cmacKey);

            if (!ByteArraysEqual(actualCMAC, expectedCMAC))
            {
                throw new InvalidDataException("Invalid CMAC.");
            }

            //9. Result
            fileBytes = decrypted;
        }

        static public void EncryptSMM2Course(ref byte[] fileBytes)
        {
            const int HeaderSize = 0x10;
            const int BodySize = 0x5BFC0;
            const int HeaderAndBodySize = 0x5BFD0;

            bool withoutHeader;

            if (fileBytes.Length == BodySize)
            {
                withoutHeader = true;
            }
            else if (fileBytes.Length == HeaderAndBodySize)
            {
                withoutHeader = false;
            }
            else
            {
                throw new InvalidDataException(
                    $"Invalid decrypted BCD size: 0x{fileBytes.Length:X}."
                );
            }

            byte[] decrypted = new byte[BodySize];

            if (withoutHeader)
            {
                Buffer.BlockCopy( fileBytes, 0, decrypted, 0, BodySize);
            }
            else
            {
                Buffer.BlockCopy( fileBytes, HeaderSize, decrypted, 0, BodySize);
            }

            // 1. Create header
            byte[] header = new byte[HeaderSize];

            Crc32 crc32 = new Crc32();

            uint crc = crc32.ComputeChecksum(
                decrypted,
                0,
                decrypted.Length
            );

            byte[] checksum = BitConverter.GetBytes(crc);

            if (withoutHeader)
            {
                WriteUInt32LE(header, 0x00, 0x00000001);
                WriteUInt16LE(header, 0x04, 0x0010);
                WriteUInt16LE(header, 0x06, 0x0000);

                Array.Copy(checksum, 0, header, 0x08, 4);

                header[0x0C] = 0x53; // S
                header[0x0D] = 0x43; // C
                header[0x0E] = 0x44; // D
                header[0x0F] = 0x4C; // L
            }
            else
            {
                Buffer.BlockCopy(fileBytes, 0, header, 0, HeaderSize);

                //Update CRC because the body could have been modified.
                Array.Copy(checksum, 0, header, 0x08, 4);
            }

            //2. RNG state determinístico
            byte[] randomSeed = {
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            };

            RandomSMM2 random = new RandomSMM2(
                ReadUInt32LE(randomSeed, 0),
                ReadUInt32LE(randomSeed, 4),
                ReadUInt32LE(randomSeed, 8),
                ReadUInt32LE(randomSeed, 12)
            );

            //3. IV
            byte[] iv = {
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            };

            //4. AES key
            byte[] aesKey =
                CreateKey(random, bcdTable, 16);

            //5. AES-CBC encrypt
            byte[] encrypted = new byte[BodySize];

            using (Aes aes = Aes.Create())
            {
                aes.Key = aesKey;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.None;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    encryptor.TransformBlock(decrypted, 0, BodySize, encrypted, 0);
                }
            }

            //6. CMAC key
            //AGAIN:
            //Random continues after generate aesKey
            byte[] cmacKey = CreateKey(random, bcdTable, 16);

            //7. CMAC
            byte[] cmac = AESCMAC(decrypted, cmacKey);

            //8. Build final file
            byte[] result = new byte[0x5C000];

            Buffer.BlockCopy( header, 0, result, 0, HeaderSize);

            Buffer.BlockCopy(encrypted, 0, result, 0x10, BodySize);

            //IV
            Buffer.BlockCopy(iv, 0, result, 0x5BFD0, 0x10);

            //RNG state
            Buffer.BlockCopy(randomSeed, 0, result, 0x5BFE0, 0x10);

            //CMAC
            Buffer.BlockCopy(cmac, 0, result, 0x5BFF0, 0x10);

            fileBytes = result;
        }

        private class RandomSMM2
        {
            private uint s0;
            private uint s1;
            private uint s2;
            private uint s3;

            public RandomSMM2(uint s0, uint s1, uint s2, uint s3)
            {
                this.s0 = s0;
                this.s1 = s1;
                this.s2 = s2;
                this.s3 = s3;
            }

            public uint U32()
            {
                uint temp = s0;

                temp ^= temp << 11;
                temp ^= temp >> 8;
                temp ^= s3;
                temp ^= s3 >> 19;

                s0 = s1;
                s1 = s2;
                s2 = s3;
                s3 = temp;

                return temp;
            }

            public uint Next(uint max)
            {
                return (uint)(((ulong)U32() * max) >> 32);
            }
        }

        private static byte[] CreateKey(
            RandomSMM2 random,
            uint[] table,
            int size
        )
        {
            byte[] key = new byte[size];

            int outputOffset = 0;

            for (int i = 0; i < size / 4; i++)
            {
                uint value = 0;

                for (int e = 0; e < 4; e++)
                {
                    uint index = random.Next((uint)table.Length);

                    uint shift = random.Next(4) * 8;

                    byte b = (byte)((table[index] >> (int)shift) & 0xFF);

                    value = (value << 8) | b;
                }

                // binary.Write(... LittleEndian)
                key[outputOffset++] = (byte)(value & 0xFF);
                key[outputOffset++] = (byte)((value >> 8) & 0xFF);
                key[outputOffset++] = (byte)((value >> 16) & 0xFF);
                key[outputOffset++] = (byte)((value >> 24) & 0xFF);
            }

            return key;
        }

        private static bool ByteArraysEqual(
        byte[] a,
        byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }

            return true;
        }

        private static uint ReadUInt32LE(byte[] data, int offset)
        {
            return (uint)(
                data[offset] |
                (data[offset + 1] << 8) |
                (data[offset + 2] << 16) |
                (data[offset + 3] << 24)
            );
        }

        private static void WriteUInt32LE(
        byte[] data,
        int offset,
        uint value)
        {
            data[offset] = (byte)(value & 0xFF);
            data[offset + 1] = (byte)((value >> 8) & 0xFF);
            data[offset + 2] = (byte)((value >> 16) & 0xFF);
            data[offset + 3] = (byte)((value >> 24) & 0xFF);
        }

        private static void WriteUInt16LE(
        byte[] data,
        int offset,
        ushort value)
        {
            data[offset] = (byte)(value & 0xFF);
            data[offset + 1] = (byte)(value >> 8);
        }

        private static byte[] AESCMAC(byte[] data, byte[] key)
        {
            const int blockSize = 16;

            byte[] L = AES_ECB_Encrypt(
                new byte[blockSize],
                key
            );

            byte[] K1 = GenerateSubkey(L);
            byte[] K2 = GenerateSubkey(K1);

            int blockCount = Math.Max(1, (data.Length + blockSize - 1) / blockSize);

            bool complete =
                data.Length != 0 &&
                data.Length % blockSize == 0;

            byte[] lastBlock = new byte[blockSize];

            if (complete)
            {
                Buffer.BlockCopy(
                    data,
                    (blockCount - 1) * blockSize,
                    lastBlock,
                    0,
                    blockSize
                );

                XOR(lastBlock, K1);
            }
            else
            {
                int remaining = data.Length % blockSize;

                if (remaining > 0)
                {
                    Buffer.BlockCopy(
                        data,
                        (blockCount - 1) * blockSize,
                        lastBlock,
                        0,
                        remaining
                    );
                }

                lastBlock[remaining] = 0x80;

                XOR(lastBlock, K2);
            }

            byte[] X = new byte[blockSize];

            for (int i = 0; i < blockCount - 1; i++)
            {
                byte[] block = new byte[blockSize];

                Buffer.BlockCopy(
                    data,
                    i * blockSize,
                    block,
                    0,
                    blockSize
                );

                XOR(block, X);

                X = AES_ECB_Encrypt(block, key);
            }

            XOR(lastBlock, X);

            return AES_ECB_Encrypt(lastBlock, key);
        }

        private static byte[] AES_ECB_Encrypt(
        byte[] data,
        byte[] key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;

                using (ICryptoTransform encryptor =
                    aes.CreateEncryptor())
                {
                    return encryptor.TransformFinalBlock(
                        data,
                        0,
                        data.Length
                    );
                }
            }
        }

        private static void XOR(
        byte[] destination,
        byte[] source)
        {
            for (int i = 0; i < destination.Length; i++)
                destination[i] ^= source[i];
        }

        private static byte[] GenerateSubkey(byte[] input)
        {
            byte[] output = new byte[16];

            bool msbSet = (input[0] & 0x80) != 0;

            for (int i = 0; i < 15; i++)
            {
                output[i] =
                    (byte)((input[i] << 1) |
                    (input[i + 1] >> 7));
            }

            output[15] = (byte)(input[15] << 1);

            if (msbSet)
                output[15] ^= 0x87;

            return output;
        }

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

        static public void WriteSMM2Course(ref byte[] tmpfileBytes)
        {

        }
    }
}
