using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Serialization;
using FileSystemSimulation.Clusters;
using FileSystemSimulation.Files;

namespace FileSystemSimulation.FileSystem
{
    [Serializable]
    public class FileSystem : INotifyPropertyChanged
    {
        private ClusterTable clusterTable;
        private ObservableCollection<File> files;
        private File selectedFile;

        public FileSystem()
        {
            ClusterTable = new ClusterTable();
            Files = new ObservableCollection<File>();
        }

        private bool IsFileExists(string _name)
        {
            return Files.Any(_file => _file.Name.ToLower().Equals(_name.ToLower()));
        }

        public void CreateFile()
        {
            var createFileWindow = new CreateUpdateFileWindow
            {
                Owner = Application.Current.MainWindow,
                File = new File()
            };
            createFileWindow.ShowDialog();
            if (createFileWindow.IsConfirmed)
            {
                var file = createFileWindow.File;
                file.InitializeCluster();

                if (IsFileExists(file.Name))
                {
                    MessageBox.Show("File name already exist");
                    return;
                }

                //Add file in cluster
                ClusterTable.AddFileInCluster(file);
                Files.Add(file);
            }
        }

        public void UpdateFile()
        {
            if (SelectedFile != null)
            {
                var oldName = SelectedFile.Name;
                var oldSize = SelectedFile.Size;

                var updateFileWindow = new CreateUpdateFileWindow
                {
                    Owner = Application.Current.MainWindow,
                    File = SelectedFile
                };
                updateFileWindow.ShowDialog();

                if (updateFileWindow.IsConfirmed)
                {
                    var newFile = updateFileWindow.File;

                    if (!newFile.Name.Equals(oldName) && IsFileExists(newFile.Name))
                    {
                        MessageBox.Show("File name already exist");
                        return;
                    }

                    //Clear selection
                    ClusterTable.SelectionClear();

                    if (newFile.Size > oldSize)
                    {
                        //New number cluster
                        var numberNewCluster = (int) Math.Ceiling((decimal)newFile.Size / Settings.Settings.ClusterSize) - newFile.NumberClusterUse;
                        newFile.IncreaseSizeFile(oldSize);
                        ClusterTable.IncreaseClusterFile(numberNewCluster, newFile);
                    }
                    else if (newFile.Size < oldSize) //Smaller size
                    {
                        ClusterTable.DecreaseClusterFile(oldSize, newFile);
                        newFile.DecreseSizeFile(oldSize);
                    }

                    //Reselect
                    ClusterTable.SelectClusters(newFile.StartCluster.Address);
                }
            }
        }

        public void DeleteFile()
        {
            if (SelectedFile != null)
            {
                ClusterTable.DeleteFile(SelectedFile);
                Files.Remove(SelectedFile);
            }
        }

        public void ClearClusterMouseEnter()
        {
            ClusterTable.ClearClusterMouseEnter();
        }

        public void SelectFileByClusterAddress(string _address)
        {
            foreach (var file in Files)
            {
                if (file.StartCluster.Address.Equals(_address))
                {
                    SelectedFile = SelectedFile == file ? null : file;
                    break;
                }
                foreach (var fragmentFile in file.FragmentFiles)
                {
                    if (fragmentFile.NextCluster != null && fragmentFile.NextCluster.Address.Equals(_address))
                    {
                        SelectedFile = SelectedFile == file ? null : file;
                        break;
                    }
                }
            }
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Structure")]
        public Structure.Structure Structure { get; set; }

        [XmlIgnore]
        public ClusterTable ClusterTable
        {
            get => clusterTable;
            set
            {
                clusterTable = value;
                OnPropertyChanged(nameof(ClusterTable));
            }
        }

        [XmlIgnore]
        public File SelectedFile
        {
            get => selectedFile;
            set
            {
                selectedFile = value;
                if(value == null)
                    ClusterTable.SelectionClear();
                else if(value.StartCluster != null)
                    ClusterTable.SelectClusters(value.StartCluster.Address);
                OnPropertyChanged(nameof(SelectedFile));
            }
        }


        [XmlIgnore]
        public ObservableCollection<File> Files
        {
            get => files;
            set
            {
                files = value;
                OnPropertyChanged(nameof(Files));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}