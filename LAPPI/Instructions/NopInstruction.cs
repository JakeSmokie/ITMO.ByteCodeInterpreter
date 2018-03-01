using System.Collections.Generic;
using LAPPI.Module;

namespace LAPPI.Instructions
{
    class NopInstruction : IInstruction
    {
        public byte Opcode => 0;

        public void Run(BinaryModule module, Stack<int> valueStack, Stack<long> callStack, ref int cursor) { }
    }
}
