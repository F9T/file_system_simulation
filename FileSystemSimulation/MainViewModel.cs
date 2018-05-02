using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using FileSystemSimulation.Clusters;
using FileSystemSimulation.Files;
using FileSystemSimulation.FileSystem;

namespace FileSystemSimulation
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private FileSystem.FileSystem selectedFileSystem;

        public MainViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            FilesSystem = new ObservableCollection<FileSystem.FileSystem>();
            ReadFolderFileSystem();
        }

        private void ReadFolderFileSystem()
        {
            var dirInfo = new DirectoryInfo("./files_system");
            if (dirInfo.Exists)
            {
                foreach (var file in dirInfo.GetFiles("*.xml"))
                {
                    var fileSystem = FileSystemSerializer.Deserialize(file.FullName);
                    if (fileSystem != null)
                    {
                        FilesSystem.Add(fileSystem);
                    }
                }
            }
        }

        public void MouseEnterCluster(Cluster _cluster)
        {
            if (_cluster.ClusterFile is FragmentClusterFile file)
            {
                SelectedFileSystem?.ClearClusterMouseEnter();
                _cluster.IsMouseEnter = true;
                while (file.PreviousCluster != null)
                {
                    file.PreviousCluster.IsMouseEnter = true;
                    file = file.PreviousCluster.ClusterFile as FragmentClusterFile;
                }
                //Next cluster
                file = (FragmentClusterFile) _cluster.ClusterFile;
                while (file.NextCluster != null)
                {
                    file.NextCluster.IsMouseEnter = true;
                    file = file.NextCluster.ClusterFile as FragmentClusterFile;
                }
            }
        }

        public void MouseLeaveCluster()
        {
            SelectedFileSystem?.ClearClusterMouseEnter();
        }

        public void SelectFileByClusterAddress(string _address)
        {
            SelectedFileSystem?.SelectFileByClusterAddress(_address);
        }

        public void DeselectFile()
        {
            if (SelectedFileSystem != null)
            {
                SelectedFileSystem.SelectedFile = null;
            }
        }

        public void CreateFile()
        {
            SelectedFileSystem?.CreateFile();
        }

        public void UpdateFile()
        {
            SelectedFileSystem?.UpdateFile();
        }

        public void DeleteFile()
        {
            SelectedFileSystem?.DeleteFile();
        }

        public FileSystem.FileSystem SelectedFileSystem
        {
            get => selectedFileSystem;
            set
            {
                selectedFileSystem = value;
                OnPropertyChanged(nameof(SelectedFileSystem));
            }
        }

        public ObservableCollection<FileSystem.FileSystem> FilesSystem { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}