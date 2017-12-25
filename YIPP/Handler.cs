using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YIPP
{
    static class PTPTBHandler
    {
        /* Methods */
        public static void ReadFile(string[] args)
        {
            ReadFilePath(args);
            CreateBinaryReader();

            CheckSignature();

            ReadModuleInfo();
            ReadEntriesCount();

            ReadSections();
            ReadSymbols();

            // THIS
            ReadSourceFiles();
            ReadSourceTextRanges();
            ReadSourcePoints();

            ReadBlobs();
            ReadStrings();

            CloseBinaryReader();

            //Console.ReadKey(true);
        }

        private static void ReadFilePath(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments has been inputted!");
                Console.ReadKey(true);
                Environment.Exit(-1);

                return;
            }

            filePath = args[0];

            if (debug)
            {
                Console.WriteLine("Loading file '{0}'...", args[0]);
            }
        }

        private static void CreateBinaryReader()
        {
            fileStream = File.Open(filePath, FileMode.Open);
            reader = new BinaryReader(fileStream);
        }

        private static void CloseBinaryReader()
        {
            fileStream.Close();
            reader.Close();
        }

        private static void CheckSignature()
        {
            Int32 len = reader.ReadInt32();
            string str = new string(reader.ReadChars(len));

            if (str != "P.TP.TB.")
            {
                Console.WriteLine("Wrong file! (not .ptptb)");
                Console.ReadKey(true);
                Environment.Exit(-1);

                return;
            }

            if (debug)
            {
                Console.WriteLine("File signature is OK!");
            }
        }

        private static void ReadModuleInfo()
        {
            ModuleHeader.formatVersion = reader.ReadInt32();
            ModuleHeader.platformNameIndex = reader.ReadInt32();
            ModuleHeader.platformVersion = reader.ReadInt32();
            ModuleHeader.entryPoint = reader.ReadInt64();

            if (debug)
            {
                Console.WriteLine(
                    "Module Info:\n" +
                    " formatVersion = {0}\n" +
                    " platformNameIndex = {1}\n" +
                    " platformVersion = {2}\n" +
                    " entryPoint = {3}",
                    ModuleHeader.formatVersion,
                    ModuleHeader.platformNameIndex,
                    ModuleHeader.platformVersion,
                    ModuleHeader.entryPoint);
            }
        }

        private static void ReadEntriesCount()
        {
            if (debug)
            {
                Console.WriteLine("Entries counts: ");
            }

            for (int i = 0; i < Count.tables; i++)
            {
                Count.counts[i] = reader.ReadInt32();

                if (debug)
                {
                    Console.WriteLine(" {0} = {1}",
                        Count.names[i],
                        Count.counts[i]);
                }
            }
        }

        private static void ReadSections()
        {
            int count = Count.counts[Count.sections];
            sections = new SectionLayout[count];

            if (debug)
            {
                Console.WriteLine("Loading sections ({0}):", count);
            }

            for (int i = 0; i < count; i++)
            {
                sections[i].blobIndex = reader.ReadInt32();
                sections[i].bankNameIndex = reader.ReadInt32();
                sections[i].startAddress = reader.ReadInt64();
                sections[i].kind = (SectionLayout.SectionKind)reader.ReadInt16();
                sections[i].customSectionNameIndex = reader.ReadInt32();
                sections[i].accessMode = (SectionLayout.SectionAccessMode)reader.ReadInt16();

                if (debug)
                {
                    Console.WriteLine(
                    " {0}:\n" +
                    "  blobIndex = {1}\n" +
                    "  bankNameIndex = {2}\n" +
                    "  startAddress = {3}\n" +
                    "  kind = {4}\n" +
                    "  customSectionNameIndex = {5}\n" +
                    "  accessMode = {6}\n",
                    i,
                    (sections[i].blobIndex),
                    (sections[i].bankNameIndex),
                    (sections[i].startAddress),
                    (sections[i].kind),
                    (sections[i].customSectionNameIndex),
                    (sections[i].accessMode)
                    );
                }
            }
        }

        private static void ReadSymbols()
        {
            int count = Count.counts[Count.symbols];
            symbols = new SymbolLayout[count];

            if (debug)
            {
                Console.WriteLine("Loading symbols ({0}):", count);
            }

            for (int i = 0; i < count; i++)
            {
                symbols[i].sectionIndex = reader.ReadInt32();
                symbols[i].blobEntryIndex = reader.ReadInt64();
                symbols[i].nameIndex = reader.ReadInt32();

                if (debug)
                {
                    Console.WriteLine(
                        " {0}:\n" +
                        "  sectionIndex = {1}\n" +
                        "  blobEntryIndex = {2}\n" +
                        "  nameIndex = {3}\n",
                        i,
                        (symbols[i].sectionIndex),
                        (symbols[i].blobEntryIndex),
                        (symbols[i].nameIndex)
                        );
                }
            }
        }

        private static void ReadSourceFiles()
        {
            int count = Count.counts[Count.sourceFiles];
            sourceFiles = new SourceFileLayout[count];

            if (debug)
            {
                Console.WriteLine("Loading source files ({0}):", count);
            }

            for (int i = 0; i < count; i++)
            {
                sourceFiles[i].fileNameIndex = reader.ReadInt32();
                sourceFiles[i].sha256hashBytesIndex = reader.ReadInt32();

                if (debug)
                {
                    Console.WriteLine(
                        " {0}:\n" +
                        "  fileNameIndex = {1}\n" +
                        "  sha256hashBytesIndex = {2}\n",
                        i,
                        (sourceFiles[i].fileNameIndex),
                        (sourceFiles[i].sha256hashBytesIndex)
                        );
                }
            }
        }

        // DO
        private static void ReadSourceTextRanges()
        {

        }

        // DO
        private static void ReadSourcePoints()
        {

        }

        private static void ReadBlobs()
        {
            int count = Count.counts[Count.blobs];
            blobs = new Blob[count];

            if (debug)
            {
                Console.Write("Loading blobs ({0}):", count);
            }

            for (int i = 0; i < count; i++)
            {
                blobs[i].length = reader.ReadInt32();
            }
            for (int i = 0; i < count; i++)
            {
                if (debug)
                {
                    Console.Write("\n {0}[{1}]:\n  ", i, blobs[i].length);
                }
                
                blobs[i].data = new Byte[blobs[i].length];

                for (int j = 0; j < blobs[i].length; j++)
                {
                    blobs[i].data[j] = reader.ReadByte();

                    if (debug)
                    {
                        Console.Write("{0} ", blobs[i].data[j]);
                    }
                }
            }

            Console.WriteLine();
        }

        private static void ReadStrings()
        {
            int count = Count.counts[Count.strings];
            strings = new string[count];

            if (debug)
            {
                Console.WriteLine("Loading strings ({0}):", count);
            }

            int len;
            char[] str;

            for (int i = 0; i < count; i++)
            {
                len = reader.ReadInt32();
                str = reader.ReadChars(len);

                strings[i] = new string(str);

                if (debug)
                {
                    Console.WriteLine(" [{0}] {1}", i, strings[i]);
                }
            }
        }

        /* Variables */
        private static string filePath = "";
        private static FileStream fileStream;
        private static BinaryReader reader;

        public static SectionLayout[] sections;
        public static SymbolLayout[] symbols;

        // DONT FORGET ABOUT THIS
        public static SourceFileLayout[] sourceFiles;
        public static SourceTextRangeLayout[] sourceTextRanges;
        public static SourceCodePointLayout[] sourceCodePoints;

        public static Blob[] blobs;
        public static string[] strings;

        public const bool debug = true;
    }
}