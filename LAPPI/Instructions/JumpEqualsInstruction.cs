using LAPPI.Instructions;

namespace LAPPI
{
    internal class JumpEqualsInstruction : IInstruction
    {
        public byte Opcode => 11;

        public void Run(InterpreterStorage storage)
        {
            InterpreterUtils.RunJumpCondition(storage, (left, right) => left == right);
        }
    }
}