using System;
using System.Collections.Generic;
using LAPPI.Instructions;
using LAPPI.Module;

namespace LAPPI
{
    internal class Interpreter
    {
        private InterpreterStorage storage;

        public Interpreter(InterpreterStorage storage)
        {
            this.storage = storage;
        }

        public void Run()
        {
            storage.cursor = (int) storage.module.entryPoint;

            while (storage.cursor < storage.module.codeSection.Blob.Length)
            {
                Byte opcode = storage.module.codeSection.Blob[storage.cursor];
                storage.cursor += 1;

                var instruction = storage.instructions.Find(op => op.Opcode == opcode);
                instruction.Run(storage);
            }
        }
    }
}
