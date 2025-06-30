using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakerUtilidades
{
    class CustomStringWriter : System.IO.StringWriter
    {
        private readonly Encoding encoding;

        public CustomStringWriter(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }

    public class Crc32
    {
        private readonly uint[] table;

        public Crc32()
        {
            const uint poly = 0xEDB88320u;
            table = new uint[256];

            for (uint i = 0; i < table.Length; ++i)
            {
                uint crc = i;
                for (int j = 0; j < 8; ++j)
                    crc = (crc & 1) != 0 ? (poly ^ (crc >> 1)) : (crc >> 1);
                table[i] = crc;
            }
        }

        public uint ComputeChecksum(byte[] bytes, int offset, int length)
        {
            uint crc = 0xFFFFFFFFu;
            for (int i = offset; i < offset + length; ++i)
            {
                byte index = (byte)((crc ^ bytes[i]) & 0xFF);
                crc = (crc >> 8) ^ table[index];
            }
            return ~crc;
        }

        public byte[] ComputeChecksumBytes(byte[] bytes, int offset, int length)
        {
            uint crc = ComputeChecksum(bytes, offset, length);
            return BitConverter.GetBytes(crc); //Returns bytes on little endian order
        }
    }
}