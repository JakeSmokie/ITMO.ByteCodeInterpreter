using System;
using System.Collections.Generic;
using LAPPI.Module;

namespace LAPPI.Instructions
{
    public interface IInstruction
    {
        Byte Opcode { get; }
        void Run(BinaryModule module, Stack<int> valueStack, Stack<Int64> callStack, ref int cursor);
    }
}
