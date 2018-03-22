using System;
using LAPPI.Instructions;

namespace LAPPI
{
    internal class MovInstruction : IInstruction
    {
        public byte Opcode => 4;

        public void Run(InterpreterStorage storage)
        {
            InterpreterUtils.RunDestSrc(storage, (dest, src) => src);
        }
    }
}