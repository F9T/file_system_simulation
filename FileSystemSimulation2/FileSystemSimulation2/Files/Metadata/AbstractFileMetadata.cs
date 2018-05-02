using System.ComponentModel;
using System.Runtime.CompilerServices;
using FileSystemSimulation2.Clusters;

namespace FileSystemSimulation2.Files.Metadata
{
    public abstract class AbstractFileMetadata : INotifyPropertyChanged
    {
        private Cluster firstCluster;
        private string fileName;
        private int fileSize;

        public Cluster FirstCluster
        {
            get => firstCluster;
            set
            {
                firstCluster = value;
                OnPropertyChanged(nameof(FirstCluster));
            }
        }

        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public int FileSize
        {
            get => fileSize;
            set
            {
                fileSize = value;
                OnPropertyChanged(nameof(FileSize));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
