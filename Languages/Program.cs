using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Languages
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new ExtBinaryReader(File.OpenRead(path: args[0]));
        }
    }
}
