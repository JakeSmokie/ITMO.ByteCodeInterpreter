using LAPPI.Instructions;

namespace LAPPI
{
    internal class PushInstruction : IInstruction
    {
        public byte Opcode => 2;

        public void Run(InterpreterStorage storage)
        {
            storage.stack.Push(InterpreterUtils.ReadVar(storage));
        }
    }
}