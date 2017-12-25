using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YIPP
{
    static class OpCodes
    {
        public static IInstruction[] instructions = new IInstruction[256];
        public static Random random = new Random();
        public static Stack<int> callStack = new Stack<int>();
        public static List<List<int>> args = new List<List<int>> { };
        public static List<List<int>> locals = new List<List<int>> { };

        public static void AssociateOpcodes()
        {
            instructions[0] = new NOP();
            instructions[1] = new PRNT();

            instructions[16] = new PUSH();
            instructions[17] = new POP();
            instructions[18] = new SAVE();
            instructions[19] = new CPY();
            instructions[20] = new RNDM();
            instructions[21] = new EMPTY();

            instructions[128] = new ADD();
            instructions[129] = new SUB();
            instructions[130] = new MUL();
            instructions[131] = new DIV();

            instructions[132] = new SUB2();
            instructions[133] = new DIV2();

            instructions[134] = new NEG();
            instructions[135] = new ABS();

            instructions[144] = new INC();
            instructions[145] = new DEC();

            instructions[64] = new JMP();
            instructions[65] = new JGZ();
            instructions[66] = new JLZ();
            instructions[67] = new JEZ();
            instructions[68] = new JNZ();
            instructions[69] = new CALL();
            instructions[70] = new RET();
            instructions[71] = new LDLOC();
            instructions[72] = new STLOC();
            instructions[73] = new LDARG();
            instructions[74] = new STARG();

            instructions[0b10100100] = new CMP();
            instructions[0b10100101] = new CMP2();
            instructions[0b10100110] = new READ();
        }
    }

    interface IInstruction
    {
        void Run();
    }

    class NOP : IInstruction
    {
        public void Run() { }
    }

    class PUSH : IInstruction
    {
        public void Run()
        {
            int varType = Interpreter.codeBlob[Interpreter.cursor++];
            int offset = Interpreter.Tools.ReadInt32();
            int num = 0;

            /* 0 for value, 1 for offset, 2 for array */
            if (varType == 0)
            {
                num = offset;
            }
            else if (varType == 1)
            {
                num = Interpreter.Tools.GetNumAtOffset(offset);
            }
            else
            {
                int varOffset = Interpreter.Tools.ReadInt32();
                int index = Interpreter.Tools.GetNumAtOffset(varOffset);

                num = Interpreter.Tools.GetNumAtOffset(offset + index * 4);
            }

            Interpreter.stack.Push(num);
        }
    }

    class POP : IInstruction
    {
        public void Run()
        {
            Interpreter.stack.Pop();
        }
    }
    class PRNT : IInstruction
    {
        public void Run()
        {
            int value = Interpreter.stack.Pop();
            //Console.WriteLine("0x{0:X}", value);
            Console.WriteLine(value);
        }
    }
    class SAVE : IInstruction
    {
        public void Run()
        {
            int type = Interpreter.codeBlob[Interpreter.cursor++];
            int offset = Interpreter.Tools.ReadInt32();
            int num = Interpreter.stack.Pop();

            int indexOffset = 0;

            if (type == 1)
            {
                indexOffset = Interpreter.Tools.ReadInt32();
                indexOffset = Interpreter.Tools.GetNumAtOffset(indexOffset) * 4; // real index offset
            }

            for (int i = 0; i < 4; i++)
            {
                Byte temp = Convert.ToByte(num & 0xFF);
                num >>= 8;

                Interpreter.dataBlob[offset + i + indexOffset] = temp;
            }
        }
    }
    class CPY : IInstruction
    {
        public void Run()
        {
            Interpreter.stack.Push(Interpreter.stack.Peek());
        }
    }
    class ADD : IInstruction
    {
        public void Run()
        {
            int a = Interpreter.stack.Pop();
            int b = Interpreter.stack.Pop();

            //Interpreter.stack.Push(a);
            Interpreter.stack.Push(a + b);
        }
    }
    class SUB : IInstruction
    {
        public void Run()
        {
            int a = Interpreter.stack.Pop();
            int b = Interpreter.stack.Pop();

            //Interpreter.stack.Push(a);
            Interpreter.stack.Push(a - b);
        }
    }
    class SUB2 : IInstruction
    {
        public void Run()
        {
            int a = Interpreter.stack.Pop();
            int b = Interpreter.stack.Pop();

            //Interpreter.stack.Push(a);
            Interpreter.stack.Push(b - a);
        }
    }
    class MUL : IInstruction
    {
        public void Run()
        {
            int a = Interpreter.stack.Pop();
            int b = Interpreter.stack.Pop();

            //Interpreter.stack.Push(a);
            Interpreter.stack.Push(a * b);
        }
    }
    class DIV : IInstruction
    {
        public void Run()
        {
            int a = Interpreter.stack.Pop();
            int b = Interpreter.stack.Pop();

            //Interpreter.stack.Push(a);
            Interpreter.stack.Push(a / b);
        }
    }
    class DIV2 : IInstruction
    {
        public void Run()
        {
            int a = Interpreter.stack.Pop();
            int b = Interpreter.stack.Pop();

            //Interpreter.stack.Push(a);
            Interpreter.stack.Push(b / a);
        }
    }
    class NEG : IInstruction
    {
        public void Run()
        {
            Interpreter.stack.Push(-Interpreter.stack.Pop());
        }
    }
    class ABS : IInstruction
    {
        public void Run()
        {
            Interpreter.stack.Push(Math.Abs(Interpreter.stack.Pop()));
        }
    }

    class INC : IInstruction
    {
        public void Run()
        {
            Interpreter.stack.Push(Interpreter.stack.Pop() + 1);
        }
    }
    class DEC : IInstruction
    {
        public void Run()
        {
            Interpreter.stack.Push(Interpreter.stack.Pop() - 1);
        }
    }
    class JMP : IInstruction
    {
        public void Run()
        {
            Interpreter.cursor = Interpreter.Tools.ReadInt32();
        }
    }
    class JLZ : IInstruction
    {
        public void Run()
        {
            if (Interpreter.stack.Pop() < 0)
                Interpreter.cursor = Interpreter.Tools.ReadInt32();
            else
                Interpreter.cursor += 4;
        }
    }
    class JGZ : IInstruction
    {
        public void Run()
        {
            if (Interpreter.stack.Pop() > 0)
                Interpreter.cursor = Interpreter.Tools.ReadInt32();
            else
                Interpreter.cursor += 4;
        }
    }
    class JEZ : IInstruction
    {
        public void Run()
        {
            if (Interpreter.stack.Pop() == 0)
                Interpreter.cursor = Interpreter.Tools.ReadInt32();
            else
                Interpreter.cursor += 4;
        }
    }
    class JNZ : IInstruction
    {
        public void Run()
        {
            if (Interpreter.stack.Pop() != 0)
                Interpreter.cursor = Interpreter.Tools.ReadInt32();
            else
                Interpreter.cursor += 4;
        }
    }
    class CMP : IInstruction
    {
        public void Run()
        {
            int a = Interpreter.stack.Pop();
            int b = Interpreter.stack.Peek();

            Interpreter.stack.Push(a);

            int value = 0;

            if (a > b)
                value = 1;
            else if (a < b)
                value = -1;

            Interpreter.stack.Push(value);
            //Console.WriteLine(a - b);
        }
    }
    class CMP2 : IInstruction
    {
        public void Run()
        {
            int a = Interpreter.stack.Pop();
            int b = Interpreter.stack.Pop();

            int value = 0;

            if (a > b)
                value = 1;
            else if (a < b)
                value = -1;

            Interpreter.stack.Push(value);
            //Console.WriteLine("--> " + a + " --> " + b);
        }
    }
    class READ : IInstruction
    {
        public void Run()
        {
            Console.Write("Enter int value: ");
            Interpreter.stack.Push(Convert.ToInt32(Console.ReadLine()));
        }
    }
    class RNDM : IInstruction
    {
        public void Run()
        {
            int min = Interpreter.Tools.ReadInt32();
            int max = Interpreter.Tools.ReadInt32();

            int value = OpCodes.random.Next(min, max);
            Interpreter.stack.Push(value);
        }
    }
    class CALL : IInstruction
    {
        public void Run()
        {
            int cursor = Interpreter.Tools.ReadInt32();
            Byte argNum = Interpreter.codeBlob[Interpreter.cursor++];

            OpCodes.callStack.Push(Interpreter.cursor + 4 * argNum);
            List<int> listArg = new List<int> { };
            List<int> listLoc = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (Byte arg = 0; arg < argNum; arg++)
            {
                int offset = Interpreter.Tools.ReadInt32();
                int num = Interpreter.Tools.GetNumAtOffset(offset);

                listArg.Add(num);
            }

            OpCodes.args.Add(listArg);
            OpCodes.locals.Add(listLoc);

            Interpreter.cursor = cursor;
        }
    }
    class RET : IInstruction
    {
        public void Run()
        {
            Byte type = Interpreter.codeBlob[Interpreter.cursor++];
            int offset = Interpreter.Tools.ReadInt32();

            if (type == 1)
            {
                int num = Interpreter.Tools.GetNumAtOffset(offset);
                Interpreter.stack.Push(num);
            }

            int listSize = OpCodes.args.Count;

            List<int> list = OpCodes.args[listSize - 1];
            OpCodes.args.RemoveAt(listSize - 1);

            int cursor = OpCodes.callStack.Pop();
            Interpreter.cursor = cursor;
        }
    }

    class LDLOC : IInstruction
    {
        public void Run()
        {
            int varNum = Interpreter.Tools.ReadInt32();
            int index = OpCodes.locals.Count - 1;

            if (varNum > OpCodes.locals[index].Count - 1 || varNum < 0)
            {
                throw new Exception("Invalid local var num. " + (OpCodes.locals[index].Count - 1));
            }

            Interpreter.stack.Push(OpCodes.locals[index][varNum]);
        }
    }

    class STLOC : IInstruction
    {
        public void Run()
        {
            int varNum = Interpreter.Tools.ReadInt32();
            int index = OpCodes.locals.Count - 1;

            if (varNum > OpCodes.locals[index].Count - 1 || varNum < 0)
            {
                throw new Exception("Invalid local var num. " + (OpCodes.locals[index].Count - 1));
            }

            OpCodes.locals[index][varNum] = Interpreter.stack.Pop();
        }
    }

    class LDARG : IInstruction
    {
        public void Run()
        {
            int varNum = Interpreter.Tools.ReadInt32();
            int index = OpCodes.args.Count - 1;

            if (varNum > OpCodes.args[index].Count - 1 || varNum < 0)
            {
                throw new Exception("Invalid local argument num. " + (OpCodes.args[index].Count - 1));
            }

            Interpreter.stack.Push(OpCodes.args[index][varNum]);
        }
    }

    class STARG : IInstruction
    {
        public void Run()
        {
            int varNum = Interpreter.Tools.ReadInt32();
            int index = OpCodes.args.Count - 1;

            if (varNum > OpCodes.args[index].Count - 1 || varNum < 0)
            {
                throw new Exception("Invalid local argument num. " + (OpCodes.args[index].Count - 1));
            }

            OpCodes.args[index][varNum] = Interpreter.stack.Pop();
        }
    }

    class EMPTY : IInstruction
    {
        public void Run()
        {
            int empty = Convert.ToInt32(Interpreter.stack.Count == 0);
            Interpreter.stack.Push(empty);
        }
    }
    class BLANK : IInstruction
    {
        public void Run()
        {

        }
    }
}
