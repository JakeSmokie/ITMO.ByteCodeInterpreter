using System.Collections.Generic;
using System.IO;
using LAPPI.Instructions;
using LAPPI.IO;
using LAPPI.Module;

namespace LAPPI
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            var input = File.OpenRead(path: args[0]);
            var reader = new ExtBinaryReader(input);

            var moduleLayout = new BinaryModuleLayout();
            moduleLayout.Read(reader);

            input.Close();

            var storage = new InterpreterStorage()
            {
                instructions = new List<IInstruction>
                {
                    new NopInstruction(),
                    new PrintInstruction(),
                    new MovInstruction(),
                    new AddInstruction(),
                    new JumpLessInstruction(),
                    new HaltInstruction(),
                    new InputInstruction(),
                    new JumpGreaterInstruction(),
                    new JumpEqualsInstruction(),
                    new JumpInstruction(),
                    new PushInstruction(),
                    new PopInstruction (),
                },
                stack = new Stack<int>(),
                module = new BinaryModule(moduleLayout),
                cursor = 0,
                registers = new int[4]
            };

            var interpreter = new Interpreter(storage);
            interpreter.Run();
        }
    }
}
