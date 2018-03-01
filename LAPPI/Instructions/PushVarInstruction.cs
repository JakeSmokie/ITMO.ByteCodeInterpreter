using System.Collections.Generic;
using LAPPI.Module;

namespace LAPPI.Instructions
{
    class PushVarInstruction : IInstruction
    {
        public byte Opcode => 2;

        public void Run(BinaryModule module, Stack<int> valueStack, Stack<long> callStack, ref int cursor)
        {
            int offset = InterpreterUtils.ReadInt32(cursor, module.CodeSection.Blob);
            cursor += 4;

            valueStack.Push(InterpreterUtils.ReadInt32(offset, module.DataSection.Blob));
        }
    }
}
