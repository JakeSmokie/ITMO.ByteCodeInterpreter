using System;
using LAPPI.IO;

namespace LAPPI.Module
{
    public struct SourceCodePointLayout : IReadable
    {
        public Int64 address;
        public Int32 sourceOperationRangeIndex;

        public void Read(ExtBinaryReader reader)
        {
            address = reader.ReadInt64();
            sourceOperationRangeIndex = reader.ReadInt32();
        }
    }
}
