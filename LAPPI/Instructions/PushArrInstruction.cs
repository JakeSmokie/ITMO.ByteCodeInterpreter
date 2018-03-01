using System.Collections.Generic;
using LAPPI.Module;

namespace LAPPI.Instructions
{
    class PushArrInstruction : IInstruction
    {
        public byte Opcode => 3;

        public void Run(BinaryModule module, Stack<int> valueStack, Stack<long> callStack, ref int cursor)
        {
            int offset = InterpreterUtils.ReadInt32(cursor, module.CodeSection.Blob);
            cursor += 4;

            int index = InterpreterUtils.ReadInt32(cursor, module.CodeSection.Blob);
            cursor += 4;

            valueStack.Push(InterpreterUtils.ReadInt32(offset + index * 4, module.DataSection.Blob));
        }
    }
}
