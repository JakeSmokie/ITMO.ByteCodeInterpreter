using System;
using System.Collections.Generic;
using LAPPI.Instructions;
using LAPPI.Module;

namespace LAPPI
{
    class InterpreterStorage
    {
        public BinaryModule module;
        public Stack<int> stack;
        public List<IInstruction> instructions;

        public int[] registers;
        public int cursor;
    }
}
