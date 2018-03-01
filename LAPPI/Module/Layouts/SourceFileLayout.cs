using System;
using LAPPI.IO;

namespace LAPPI.Module
{
    public struct SourceFileLayout : IReadable
    {
        public Int32 fileNameIndex;
        public Int32 sha256hashBytesIndex;

        public void Read(ExtBinaryReader reader)
        {
            fileNameIndex = reader.ReadInt32();
            sha256hashBytesIndex = reader.ReadInt32();
        }
    }
}
