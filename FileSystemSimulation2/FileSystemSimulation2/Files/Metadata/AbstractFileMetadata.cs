using System.ComponentModel;
using System.Runtime.CompilerServices;
using FileSystemSimulation2.Clusters;

namespace FileSystemSimulation2.Files.Metadata
{
    public abstract class AbstractFileMetadata : INotifyPropertyChanged
    {
        private Cluster firstCluster;

        public Cluster FirstCluster
        {
            get => firstCluster;
            set
            {
                firstCluster = value;
                OnPropertyChanged(nameof(FirstCluster));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
