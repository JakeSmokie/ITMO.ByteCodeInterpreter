using System;
using LAPPI.Instructions;

namespace LAPPI
{
    internal class InputInstruction : IInstruction
    {
        public byte Opcode => 17;

        public void Run(InterpreterStorage storage)
        {
            Console.Write("Enter int value: ");
            int input = 0;

            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.Write("Wrong value. Try again: ");
            }

            InterpreterUtils.SaveValue(storage, input);
        }
    }
}