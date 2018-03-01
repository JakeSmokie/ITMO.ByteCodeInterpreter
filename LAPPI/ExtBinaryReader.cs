using System;
using System.IO;
using LAPPI.Module;

namespace LAPPI.IO
{
    public class ExtBinaryReader : BinaryReader
    {
        public ExtBinaryReader(Stream input) : base(input)
        {
        }

        public TPrimitive[] ReadArrayOfPrimitives<TPrimitive>(int count, Func<TPrimitive> readFunction)
        {
            TPrimitive[] primitives = new TPrimitive[count];

            for (int i = 0; i < count; i++)
            {
                primitives[i] = readFunction();
            }

            return primitives;
        }
        public TReadable[] ReadArrayOfReadables<TReadable>(int count) where TReadable : IReadable
        {
            TReadable[] readables = new TReadable[count];

            for (int i = 0; i < count; i++)
            {
                readables[i].Read(this);
            }

            return readables;
        }
    }
}
