using System;
using LAPPI.Instructions;

namespace LAPPI
{
    internal class PrintInstruction : IInstruction
    {
        public byte Opcode => 1;

        public void Run(InterpreterStorage storage)
        {
            Console.WriteLine(InterpreterUtils.ReadVar(storage));
        }
    }
}