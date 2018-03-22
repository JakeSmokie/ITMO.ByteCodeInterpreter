using LAPPI.Instructions;

namespace LAPPI
{
    internal class PopInstruction : IInstruction
    {
        public byte Opcode => 3;

        public void Run(InterpreterStorage storage)
        {
            InterpreterUtils.SaveValue(storage, storage.stack.Pop());
        }
    }
}