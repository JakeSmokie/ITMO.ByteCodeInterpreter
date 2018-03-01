using System.Collections.Generic;
using LAPPI.Module;

namespace LAPPI.Instructions
{
    class PushImmInstruction : IInstruction
    {
        public byte Opcode => 1;

        public void Run(BinaryModule module, Stack<int> valueStack, Stack<long> callStack, ref int cursor)
        {
            int val = InterpreterUtils.ReadInt32(cursor, module.CodeSection.Blob);
            valueStack.Push(val);
            cursor += 4;
        }
    }
}
