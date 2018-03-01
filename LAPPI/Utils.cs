using LAPPI.Module;

namespace LAPPI
{
    public static class InterpreterUtils
    {
        public static int ReadInt32(int cursor, byte[] blob)
        {
            return blob[cursor] + (blob[cursor + 1] << 8) + (blob[cursor + 2] << 16) + (blob[cursor + 3] << 24);
        }
        public static void SaveInt32(BinaryModule module, int offset, int val)
        {
            module.DataSection.Blob[offset] = (byte) ((val) & 0xFF);
            module.DataSection.Blob[offset + 1] = (byte) ((val >> 8) & 0xFF);
            module.DataSection.Blob[offset + 2] = (byte) ((val >> 16) & 0xFF);
            module.DataSection.Blob[offset + 3] = (byte) ((val >> 24) & 0xFF);
        }
    }
}
