using System;
using LAPPI.IO;

namespace LAPPI.Module
{
    public struct ModuleHeader : IReadable
    {
        private Byte[] CorrectSignature => new Byte[] { 0x50, 0x2E, 0x54, 0x50, 0x2E, 0x54, 0x42, 0x2E };

        public Byte[] signature;
        public Int32 formatVersion;
        public Int32 platformNameIndex;
        public Int32 platformVersion;
        public Int64 entryPoint;

        public void Read(ExtBinaryReader reader)
        {
            signature = reader.ReadArrayOfPrimitives(reader.ReadInt32(), reader.ReadByte);
            
            if (Equals(signature, CorrectSignature))
            {
                throw new Exception("Incorrect file. Wrong signature");
            }

            formatVersion = reader.ReadInt32();
            platformNameIndex = reader.ReadInt32();
            platformVersion = reader.ReadInt32();
            entryPoint = reader.ReadInt64();
        }
    }
}
