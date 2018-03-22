using System;

namespace LAPPI.Instructions
{
    internal interface IInstruction
    {
        Byte Opcode { get; }
        void Run(InterpreterStorage storage);
    }
}
