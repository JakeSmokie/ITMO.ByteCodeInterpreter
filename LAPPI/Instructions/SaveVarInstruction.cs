using System.Collections.Generic;
using LAPPI.Module;

namespace LAPPI.Instructions
{
    class SaveVarInstruction : IInstruction
    {
        public byte Opcode => 5;

        public void Run(BinaryModule module, Stack<int> valueStack, Stack<long> callStack, ref int cursor)
        {
            int val = valueStack.Pop();

            int offset = InterpreterUtils.ReadInt32(cursor, module.CodeSection.Blob);
            cursor += 4;

            InterpreterUtils.SaveInt32(module, cursor, val);
        }
    }
}
