using System;
using LAPPI.IO;

namespace LAPPI.Module
{
    public struct SourceTextRangeLayout : IReadable
    {
        public Int32 sourceFileIndex;
        public Int32 position;
        public Int32 length;
        public Int32 line;
        public Int32 column;

        public void Read(ExtBinaryReader reader)
        {
            sourceFileIndex = reader.ReadInt32();
            position = reader.ReadInt32();
            length = reader.ReadInt32();
            line = reader.ReadInt32();
            column = reader.ReadInt32();
        }
    }
}
