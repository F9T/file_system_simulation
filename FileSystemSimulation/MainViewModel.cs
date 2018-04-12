using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
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
            SelectedClusters = new List<Cluster>();
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

        public void CreateFile()
        {
            if (SelectedFileSystem != null)
            {
                var createFileWindow = new CreateFileWindow { Owner = Application.Current.MainWindow };
                createFileWindow.ShowDialog();
                if (createFileWindow.IsConfirmed)
                {
                    var name = createFileWindow.File.Name;
                    var size = createFileWindow.File.Size;
                    SelectedFileSystem.ClusterTable.CreateFile(name, size);
                }
            }
        }

        public void DeleteFile()
        {
            if (SelectedFileSystem != null && SelectedClusters.Count > 0)
            {
                foreach (var cluster in SelectedClusters)
                {
                    var index = SelectedFileSystem.ClusterTable.Clusters.IndexOf(cluster);
                    SelectedFileSystem.ClusterTable.Clusters.ElementAt(index).File = new EmptyFile();
                }
                SelectedClusters.Clear();
            }
        }

        public void GetClusterByFile(Cluster _cluster)
        {
            SelectedClusters.Clear();
            SelectedClusters = SelectedFileSystem.GetClustersFile(_cluster.Address);
        }

        public List<Cluster> SelectedClusters { get; set; }

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
