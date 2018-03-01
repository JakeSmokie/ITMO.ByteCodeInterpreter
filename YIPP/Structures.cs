using System;

namespace YIPP
{
    struct ModuleHeader
    {
        public static Int32 formatVersion;
        public static Int32 platformNameIndex;
        public static Int32 platformVersion;
        public static Int64 entryPoint;
    }

    static class Count
    {
        #region enum
        public const int sections = 0;
        public const int symbols = 1;
        public const int sourceFiles = 2;
        public const int sourceTextRanges = 3;
        public const int sourceCodePoints = 4;
        public const int blobs = 5;
        public const int strings = 6;
        public const int tables = 7;
        #endregion
        public static Int32[] counts = new Int32[tables];
        public static readonly string[] names =
        {
            "Sections",
            "Symbols",
            "SourceFiles",
            "SourceTextRanges",
            "SourceCodePoints",
            "Blobs",
            "Strings",
            "Tables"
        };
    }

    struct SectionLayout
    {
        public enum SectionKind : Int16
        {
            Code = 0x01,
            Data = 0x02,
            Const = 0x03,
            Custom = 0xff,
        };
        public enum SectionAccessMode : Int16
        {
            None = 0,
            Read = 1 << 0,
            Write = 1 << 1,
            Execute = 1 << 2,
        };

        public Int32 blobIndex;
        public Int32 bankNameIndex;
        public Int64 startAddress;
        public SectionKind kind;
        public Int32 customSectionNameIndex;
        public SectionAccessMode accessMode;
    }

    struct SymbolLayout
    {
        public Int32 sectionIndex;
        public Int64 blobEntryIndex;
        public Int32 nameIndex;
    }

    struct SourceFileLayout
    {
        public Int32 fileNameIndex;
        public Int32 sha256hashBytesIndex;
    };

    struct SourceTextRangeLayout
    {
        public Int32 sourceFileIndex;
        public Int32 position;
        public Int32 length;
        public Int32 line;
        public Int32 column;
    };

    struct SourceCodePointLayout
    {
        public Int64 address;
        public Int32 sourceOperationRangeIndex;
    };

    struct Blob
    {
        public Int32 length;
        public Byte[] data;
    }
}
