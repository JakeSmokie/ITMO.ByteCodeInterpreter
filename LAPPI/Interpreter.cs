using System;
using System.Collections.Generic;
using LAPPI.Instructions;
using LAPPI.Module;

namespace LAPPI
{
    internal class Interpreter
    {
        private BinaryModule module;
        private Stack<int> valueStack;
        private Stack<Int64> callStack;
        private List<IInstruction> instructions;
        private int cursor;

        public Interpreter(BinaryModule _module)
        {
            instructions = new List<IInstruction>
            {
                new NopInstruction(),
                new PushImmInstruction(),
                new PushVarInstruction(),
                new PushArrInstruction(),
                new PopInstruction()
            };

            valueStack = new Stack<int>();
            callStack = new Stack<Int64>();
            module = _module;
            cursor = 0;
        }

        public void Run()
        {
            while (cursor < module.CodeSection.Blob.Length)
            {
                Byte opcode = module.CodeSection.Blob[cursor];
                cursor += 1;

                var instruction = instructions.Find(ins => ins.Opcode == opcode);
                instruction.Run(module, valueStack, callStack, ref cursor);
            }
        }
    }
}
