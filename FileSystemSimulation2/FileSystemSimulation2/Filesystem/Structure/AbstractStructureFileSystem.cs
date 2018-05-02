using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileSystemSimulation2.Filesystem.Structure
{
    public abstract class AbstractStructureFileSystem : INotifyPropertyChanged
    {
        private SectorStructureFileSystem selectedSectorStructure;

        protected AbstractStructureFileSystem()
        {
            StructureSectors = new ObservableCollection<SectorStructureFileSystem>();
        }

        public ObservableCollection<SectorStructureFileSystem> StructureSectors { get; set; }

        public SectorStructureFileSystem SelectedSectorStructure
        {
            get => selectedSectorStructure;
            set
            {
                selectedSectorStructure = value;
                OnPropertyChanged(nameof(SelectedSectorStructure));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
