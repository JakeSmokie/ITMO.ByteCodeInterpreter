using System;
using System.Collections.Generic;
using LAPPI.Module;

namespace LAPPI.Instructions
{
    class PopInstruction : IInstruction
    {
        public byte Opcode => 4;

        public void Run(BinaryModule module, Stack<int> valueStack, Stack<long> callStack, ref int cursor)
        {
            Console.WriteLine(valueStack.Pop());
        }
    }
}
