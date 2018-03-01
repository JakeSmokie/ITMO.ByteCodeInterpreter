using System;
using System.IO;
using LAPPI.IO;
using LAPPI.Module;

namespace LAPPI
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            var reader = new ExtBinaryReader(File.OpenRead(path: args[0]));

            var moduleLayout = new BinaryModuleLayout();
            moduleLayout.Read(reader);

            var interpreter = new Interpreter(new BinaryModule(moduleLayout));
            interpreter.Run();

            Console.ReadKey();
        }
    }
}
