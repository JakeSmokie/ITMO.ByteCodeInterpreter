using System;

namespace LAPPI.Module
{
    public class BinaryModule
    {
        public Section CodeSection { get; set; }
        public Section DataSection { get; set; }

        public BinaryModule(BinaryModuleLayout moduleLayout)
        {
            CodeSection = new Section(Array.Find(moduleLayout.sections, sec => sec.kind == SectionKind.Code), moduleLayout);
            DataSection = new Section(Array.Find(moduleLayout.sections, sec => sec.kind == SectionKind.Data), moduleLayout);
        }
    }
}
