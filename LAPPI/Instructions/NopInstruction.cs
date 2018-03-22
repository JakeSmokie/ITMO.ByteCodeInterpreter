using System;

namespace LAPPI.Instructions
{
    class NopInstruction : IInstruction
    {
        public byte Opcode => 0;

        public void Run(InterpreterStorage storage) { }
    }
}
