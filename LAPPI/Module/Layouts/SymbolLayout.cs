using System;
using LAPPI.IO;

namespace LAPPI.Module
{
    public struct SymbolLayout : IReadable
    {
        public Int32 sectionIndex;
        public Int64 blobEntryIndex;
        public Int32 nameIndex;

        public void Read(ExtBinaryReader reader)
        {
            sectionIndex = reader.ReadInt32();
            blobEntryIndex = reader.ReadInt64();
            nameIndex = reader.ReadInt32();
        }
    }
}
