using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileSystemSimulation2.Filesystem.Structure
{
    public class SectorStructureFileSystem : INotifyPropertyChanged
    {
        private bool isMouseOver;

        public SectorStructureFileSystem()
        {
            // default value
            IsMouseOver = false;
        }

        public string Name { get; set; }

        public string HelpText { get; set; }

        public bool IsMouseOver
        {
            get => isMouseOver;
            set
            {
                isMouseOver = value;
                OnPropertyChanged(nameof(IsMouseOver));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
