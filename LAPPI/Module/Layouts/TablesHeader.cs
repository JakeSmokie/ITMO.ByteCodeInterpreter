using System;
using LAPPI.IO;

namespace LAPPI.Module
{
    public struct TablesHeader : IReadable
    {
        public Int32 sectionsCount;
        public Int32 symbolsCount;
        public Int32 sourceFilesCount;
        public Int32 sourceTextRangesCount;
        public Int32 sourceCodePointsCount;
        public Int32 blobsCount;
        public Int32 stringsCount;

        public void Read(ExtBinaryReader reader)
        {
            sectionsCount = reader.ReadInt32();
            symbolsCount = reader.ReadInt32();
            sourceFilesCount = reader.ReadInt32();
            sourceTextRangesCount = reader.ReadInt32();
            sourceCodePointsCount = reader.ReadInt32();
            blobsCount = reader.ReadInt32();
            stringsCount = reader.ReadInt32();
        }
    }
}
