using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileSystemSimulation.Files
{
    public abstract class AbstractClusterFile : INotifyPropertyChanged
    {
        private string name;

        protected AbstractClusterFile()
        {
            
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
