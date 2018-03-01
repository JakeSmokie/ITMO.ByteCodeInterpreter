using LAPPI.IO;

namespace LAPPI.Module
{
    public class BinaryModuleLayout : IReadable
    {
        public ModuleHeader moduleHeader;
        public TablesHeader tablesHeader;
        public SectionLayout[] sections;
        public SymbolLayout[] symbols;
        public SourceFileLayout[] sourceFiles;
        public SourceTextRangeLayout[] sourceTextRanges;
        public SourceCodePointLayout[] sourceCodePoints;
        public BlobLayout[] blobs;
        public StringLayout[] strings;

        public void Read(ExtBinaryReader reader)
        {
            moduleHeader.Read(reader);
            tablesHeader.Read(reader);

            sections = reader.ReadArrayOfReadables<SectionLayout>(tablesHeader.sectionsCount);
            symbols = reader.ReadArrayOfReadables<SymbolLayout>(tablesHeader.symbolsCount);
            sourceFiles = reader.ReadArrayOfReadables<SourceFileLayout>(tablesHeader.sourceFilesCount);
            sourceTextRanges = reader.ReadArrayOfReadables<SourceTextRangeLayout>(tablesHeader.sourceTextRangesCount);
            sourceCodePoints = reader.ReadArrayOfReadables<SourceCodePointLayout>(tablesHeader.sourceCodePointsCount);

            int[] blobLengths = reader.ReadArrayOfPrimitives(tablesHeader.blobsCount, reader.ReadInt32);
            blobs = new BlobLayout[tablesHeader.blobsCount];

            for (int i = 0; i < tablesHeader.blobsCount; i++)
            {
                blobs[i].data = reader.ReadBytes(blobLengths[i]);
            }

            strings = reader.ReadArrayOfReadables<StringLayout>(tablesHeader.stringsCount);
        }
    }
}
