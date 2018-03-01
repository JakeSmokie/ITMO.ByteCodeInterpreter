using LAPPI.IO;

namespace LAPPI.Module
{
    public interface IReadable
    {
        void Read(ExtBinaryReader reader);
    }
}