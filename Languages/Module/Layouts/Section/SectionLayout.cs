using System;
using LAPPI.IO;

namespace LAPPI.Module
{
    public struct SectionLayout : IReadable
    {
        public Int32 blobIndex;
        public Int32 bankNameIndex;
        public Int64 startAddress;
        public SectionKind kind;
        public Int32 customSectionNameIndex;
        public SectionAccessMode accessMode;

        public void Read(ExtBinaryReader reader)
        {
            blobIndex = reader.ReadInt32();
            bankNameIndex = reader.ReadInt32();
            startAddress = reader.ReadInt64();
            kind = (SectionKind)reader.ReadInt16();
            customSectionNameIndex = reader.ReadInt32();
            accessMode = (SectionAccessMode)reader.ReadInt16();
        }
    }
}
