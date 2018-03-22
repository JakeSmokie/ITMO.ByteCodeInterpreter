using System;

namespace LAPPI.Module
{
    public class BinaryModule
    {
        public Section codeSection;
        public Int64 entryPoint;
        public BinaryModule(BinaryModuleLayout moduleLayout)
        {
            codeSection = new Section(Array.Find(moduleLayout.sections, sec => sec.kind == SectionKind.Custom), moduleLayout);
            int bankIndex = Array.IndexOf(moduleLayout.strings, new StringLayout() { str = "__entry" });
            var symbolLayout = Array.Find(moduleLayout.symbols, sym => sym.nameIndex == bankIndex);
            entryPoint = symbolLayout.blobEntryIndex;
        }
    }
}
