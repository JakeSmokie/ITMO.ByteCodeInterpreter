using LAPPI.Instructions;

namespace LAPPI
{
    internal class JumpLessInstruction : IInstruction
    {
        public byte Opcode => 12;

        public void Run(InterpreterStorage storage)
        {
            InterpreterUtils.RunJumpCondition(storage, (left, right) => left < right);
        }
    }
}