using System;
using LAPPI.Instructions;

namespace LAPPI
{
    internal class JumpInstruction : IInstruction
    {
        public byte Opcode => 10;

        public void Run(InterpreterStorage storage)
        {
            int offset = BitConverter.ToInt32(storage.module.codeSection.Blob, storage.cursor);
            storage.cursor = offset;
        }
    }
}