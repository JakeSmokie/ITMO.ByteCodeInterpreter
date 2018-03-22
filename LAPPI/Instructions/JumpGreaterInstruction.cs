using LAPPI.Instructions;

namespace LAPPI
{
    internal class JumpGreaterInstruction : IInstruction
    {
        public byte Opcode => 13;

        public void Run(InterpreterStorage storage)
        {
            InterpreterUtils.RunJumpCondition(storage, (left, right) => left > right);
        }
    }
}