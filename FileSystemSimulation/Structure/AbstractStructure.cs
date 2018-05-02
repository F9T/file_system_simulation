using System.Collections.ObjectModel;

namespace FileSystemSimulation.Structure
{
    public abstract class AbstractStructure
    {
        protected AbstractStructure()
        {
            StructureSectors = new ObservableCollection<StructureSector>();
        }

        public ObservableCollection<StructureSector> StructureSectors { get; set; }
    }
}
