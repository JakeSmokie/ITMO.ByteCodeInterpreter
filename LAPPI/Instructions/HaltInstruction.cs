using System;
using LAPPI.Instructions;

namespace LAPPI
{
    internal class HaltInstruction : IInstruction
    {
        public byte Opcode => 16;

        public void Run(InterpreterStorage storage)
        {   
            Console.ReadLine();
            storage.cursor = int.MaxValue;
        }
    }
}