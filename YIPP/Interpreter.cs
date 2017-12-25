using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YIPP
{
    static class Interpreter
    {
        public static void Run()
        {
            stack = new Stack<Int32>();

            OpCodes.AssociateOpcodes();
            ResetCursor();

            SetBlobAndSectionIndexBySectionKind(SectionLayout.SectionKind.Code, ref codeBlobIndex, ref codeSectionIndex);
            SetBlobAndSectionIndexBySectionKind(SectionLayout.SectionKind.Data, ref dataBlobIndex, ref dataSectionIndex);

            codeBlob = PTPTBHandler.blobs[codeBlobIndex].data;
            dataBlob = PTPTBHandler.blobs[dataBlobIndex].data;

            while (cursor < PTPTBHandler.blobs[codeBlobIndex].length)
            {
                GetNextInstruction();
            }
        }

        private static void ResetCursor()
        {
            cursor = 0;
        }

        private static void GetNextInstruction()
        {
            Byte opcode = codeBlob[cursor];

            if (OpCodes.instructions[opcode] == null)
            {
                throw new Exception("Unknown opcode: " + opcode);
            }

            cursor++;
            OpCodes.instructions[opcode].Run();
        }

        private static void SetBlobAndSectionIndexBySectionKind(SectionLayout.SectionKind kind, ref int blobIndex, ref int secIndex)
        {
            int count = Count.counts[Count.sections];

            for (int i = 0; i < count; i++)
            {
                if (kind == PTPTBHandler.sections[i].kind)
                {
                    blobIndex = PTPTBHandler.sections[i].blobIndex;
                    secIndex = i;
                    return;
                }
            }

            throw new Exception("Can't find section with kind " + kind);
        }

        public static int cursor;

        public static int codeBlobIndex;
        public static int dataBlobIndex;

        public static int codeSectionIndex;
        public static int dataSectionIndex;

        public static Byte[] codeBlob;
        public static Byte[] dataBlob;

        public static Stack<Int32> stack;

        public static class Tools
        {
            public static int ReadInt32()
            {
                int value = 0;

                for (int i = 0; i < 4; i++)
                {
                    value += (codeBlob[cursor++] << (i * 8));
                }

                return value;
            }

            public static int GetNumAtOffset(int offset)
            {
                int num = 0;

                for (int i = 0; i < 4; i++)
                {
                    num += dataBlob[offset + i] << (i * 8);
                }

                return num;
            }
        }
    }
}
