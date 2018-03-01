using System;
using LAPPI.Module;

namespace LAPPI
{
    public class Section
    {
        public string Name { get; set; }
        public Byte[] Blob { get; set; }

        public Section(SectionLayout sectionLayout, BinaryModuleLayout moduleLayout)
        {
            Name = moduleLayout.strings[sectionLayout.bankNameIndex].str;
            Blob = moduleLayout.blobs[sectionLayout.blobIndex].data;
        }
    }
}
