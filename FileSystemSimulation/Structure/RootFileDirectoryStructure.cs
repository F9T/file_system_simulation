using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemSimulation.Structure
{
    public class RootFileDirectoryStructure : AbstractStructure
    {
        public RootFileDirectoryStructure()
        {
            StructureSectors.Add(new StructureSector("short file name", 8));
            StructureSectors.Add(new StructureSector("file extension", 3));
            StructureSectors.Add(new StructureSector("attribut", 1));
            StructureSectors.Add(new StructureSector("reserved", 1));
            StructureSectors.Add(new StructureSector("file created time (ms)", 1));
            StructureSectors.Add(new StructureSector("created time", 2));
            StructureSectors.Add(new StructureSector("created date", 2));
            StructureSectors.Add(new StructureSector("access date", 2));
            StructureSectors.Add(new StructureSector("first cluster high", 2));
            StructureSectors.Add(new StructureSector("write time", 2));
            StructureSectors.Add(new StructureSector("write date", 2));
            StructureSectors.Add(new StructureSector("first cluster low", 2));
            StructureSectors.Add(new StructureSector("file size", 4));
        }
    }
}
