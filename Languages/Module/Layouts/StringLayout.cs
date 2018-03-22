using LAPPI.IO;

namespace LAPPI.Module
{
    public struct StringLayout : IReadable
    {
        public string str;
        public void Read(ExtBinaryReader reader) => str = new string(reader.ReadChars(reader.ReadInt32()));
    }
}
