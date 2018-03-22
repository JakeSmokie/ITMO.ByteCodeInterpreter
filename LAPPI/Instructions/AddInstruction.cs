using System;
using LAPPI.Instructions;

namespace LAPPI
{
    internal class AddInstruction : IInstruction
    {
        public byte Opcode => 5;

        public void Run(InterpreterStorage storage)
        {
            InterpreterUtils.RunDestSrc(storage, (dest, src) => dest + src);
        }
    }
}