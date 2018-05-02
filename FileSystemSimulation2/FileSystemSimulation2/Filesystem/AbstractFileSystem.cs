using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FileSystemSimulation2.Clusters;
using FileSystemSimulation2.Command;
using FileSystemSimulation2.Files;
using FileSystemSimulation2.Filesystem.Structure;

namespace FileSystemSimulation2.Filesystem
{
    public abstract class AbstractFileSystem : INotifyPropertyChanged
    {
        private File selectedFile;
        private ObservableCollection<File> files;
        private ObservableCollection<Cluster> clusters;
        private int diskCapacity, diskSpaceUsed;

        protected AbstractFileSystem()
        {
            Files = new ObservableCollection<File>();
            Clusters = new ObservableCollection<Cluster>();
        }

        public abstract void NewFile();

        protected abstract void Initialize();

        public abstract void RemoveSelectedFile();

        public abstract void AutoGenerate();
        public abstract void Defragmentation();

        public abstract Cluster GetFirstEmptyCluster();

        public abstract IEnumerable<int> GetIndexFragmentClusterFileOfFile(File _file);

        public abstract void MouseEnterCluster(Cluster _cluster);

        public abstract void ClearClusterMouseEnter();

        public abstract void ClearSelection();

        public abstract void SelectClustersByFile(File _file, bool _select);

        public abstract void SelectFileByCluster(Cluster _cluster, bool _select);

        public abstract void SelectClustersByAddress(string _address, bool _select);

        public Cluster GetClusterByFragmentFile(ContentFileCluster _fragmentFile)
        {
            return Clusters.FirstOrDefault(_cluster => _cluster.Content == _fragmentFile);
        }

        public string Name { get; set; }

        public AbstractStructureFileSystem Structure { get; set; }

        public ObservableCollection<File> Files
        {
            get => files;
            set
            {
                files = value;
                OnPropertyChanged(nameof(Files));
            }
        }

        public ObservableCollection<Cluster> Clusters
        {
            get => clusters;
            set
            {
                clusters = value;
                OnPropertyChanged(nameof(Clusters));
            }
        }

        public int DiskCapacity
        {
            get => diskCapacity;
            set
            {
                diskCapacity = value;
                OnPropertyChanged(nameof(DiskCapacity));
            }
        }

        public int DiskSpaceUsed
        {
            get => diskSpaceUsed;
            set
            {
                diskSpaceUsed = value;
                OnPropertyChanged(nameof(DiskSpaceUsed));
            }
        }

        public File SelectedFile
        {
            get => selectedFile;
            set
            {
                selectedFile = value;
                SelectClustersByFile(value, true);
                OnPropertyChanged(nameof(SelectedFile));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
