using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FileSystemSimulation2.Clusters;
using FileSystemSimulation2.Files;

namespace FileSystemSimulation2.FAT32
{
    public class RootDirectory : INotifyPropertyChanged
    {
        private ObservableCollection<Cluster> clusters;
        private ObservableCollection<File> files;

        public RootDirectory(ObservableCollection<Cluster> _clusters, ObservableCollection<File> _files)
        {
            CurrentCluster = null;
            clusters = _clusters;
            files = _files;
        }

        public bool NeedUpdateRootDirectory()
        {
            return files.Count > 0 && files.Count % Settings.MaxRootDirectoryEntries == 0;
        }

        public Cluster CurrentCluster { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
