using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FileSystemSimulation2.Clusters;
using FileSystemSimulation2.Command;
using FileSystemSimulation2.FAT32;
using FileSystemSimulation2.Files;
using FileSystemSimulation2.Files.Metadata;
using FileSystemSimulation2.Filesystem.Structure;
using FileSystemSimulation2.Windows;

namespace FileSystemSimulation2.Filesystem
{
    public class FatFileSystem : AbstractFileSystem
    {
        private FileAllocationTable fileAllocationTable;
        private const int MaxSizeFile = 5000;

        public FatFileSystem()
        {
            Name = "FAT";
            Structure = new FatStructureFileSystem();
            FileAllocationTable = new FileAllocationTable(this);
            RootDirectory = new RootDirectory(Clusters, Files);

            Initialize();
        }

        public override void RemoveFile(File _file)
        {
            if (_file == null) return;

            //Clear selection
            ClearSelection();

            //Get index of cluster to remove
            var listIndex = GetIndexFragmentClusterFileOfFile(_file);

            //Clear cluster
            foreach (var index in listIndex)
            {
                Clusters.ElementAt(index).Content = new EmptyContentCluster();
            }

            Files.Remove(_file);
            SelectedFile = null;

            var needUpdate = RootDirectory.NeedUpdateRootDirectory();
            if (needUpdate)
            {
                DeleteRootDirectory();
            }
        }

        protected override void RemoveAllFiles()
        {
            ClearSelection();

            for (int i = 0; i < Files.Count;)
            {
                RemoveFile(Files.ElementAt(i));
            }
        }

        private void Initialize()
        {
            // init reserved cluster (1er and 2nd)
            Clusters.ElementAt(0).Content = new ReservedContentCluster();
            Clusters.ElementAt(1).Content = new ReservedContentCluster();

            // init root directory
            var cluster = Clusters.ElementAt(2);
            var rootDirectorsCluster = cluster;
            rootDirectorsCluster.Content = new RootDirectoryContentCluster();
            RootDirectory.CurrentCluster = cluster;

            // init commands
            DeleteAllFileCommand = new RelayCommand(_param => RemoveAllFiles(), _param => true);
            DeleteFileCommand = new RelayCommand(_param => RemoveFile(SelectedFile), _param => SelectedFile != null);
            ModifyFileCommand = new RelayCommand(_param => ModifySelectedFile(), _param => SelectedFile != null);
            NewFileCommand = new RelayCommand(_param => NewFile(), _param => true);
        }

        public override void AutoGenerate()
        {
            var generateWindow = new GenerateFileWindow
            {
                Owner = Application.Current.MainWindow
            };
            generateWindow.ShowDialog();

            if (generateWindow.IsConfirmed)
            {
                var numberFile = generateWindow.NumberFile;

                for (var i = 0; i < numberFile; i++)
                {
                    var file = RandomFile();
                    var isOk = NewFile(file);
                    if (!isOk) break;
                }
            }
        }

        private File RandomFile()
        {
            var size = random.Next(0, MaxSizeFile);

            var metadata = new FatFileMetada
            {
                Attribut = EnumFileAttribut.Archive,
                CreatedTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                Extension = "txt",
                FileName = "auto",
                FileSize = size,
                AccessDate = new DateTime().Date,
                WriteDate = new DateTime().Date,
                WriteTime = DateTime.Now,
            };

            var file = new File
            {
                Metadata = metadata,
            };
            return file;
        }

        public override void Defragmentation()
        {
        }

        public override Cluster GetFirstEmptyCluster()
        {
            return Clusters.FirstOrDefault(_cluster => _cluster.Content is EmptyContentCluster);
        }

        public override void MouseEnterCluster(Cluster _cluster)
        {
            if (_cluster.Content is ContentFileCluster file)
            {
                ClearClusterMouseEnter();
                _cluster.IsMouseEnter = true;
                while (file.PreviousCluster != null)
                {
                    file.PreviousCluster.IsMouseEnter = true;
                    file = file.PreviousCluster.Content as ContentFileCluster;
                }
                //Next cluster
                file = (ContentFileCluster)_cluster.Content;
                while (file.NextCluster != null)
                {
                    file.NextCluster.IsMouseEnter = true;
                    file = file.NextCluster.Content as ContentFileCluster;
                }
            }
        }

        public override void ClearClusterMouseEnter()
        {
            foreach (var cluster in Clusters)
            {
                cluster.IsMouseEnter = false;
            }
        }

        public override void ClearSelection()
        {
            foreach (var cluster in Clusters)
            {
                cluster.IsSelected = false;
            }
        }

        public override void SelectClustersByFile(File _file, bool _select)
        {
            if (_file != null)
            {
                var address = _file.Metadata.FirstCluster.Address;
                SelectClustersByAddress(address, _select);
            }
            else
            {
                ClearSelection();
            }
        }

        public override void SelectFileByCluster(Cluster _cluster, bool _select)
        {
            if (!_select)
            {
                SelectedFile = null;
                return;
            }
            if (_cluster.Content is ContentFileCluster content)
            {
                foreach (var file in Files)
                {
                    foreach (var contentFile in file.FragmentFiles)
                    {
                        if (contentFile == content)
                        {
                            SelectedFile = file;
                            break;
                        }
                    }
                }
            }
        }

        public override void SelectClustersByAddress(string _address, bool _select)
        {
            ClearSelection();
            foreach (var cluster in Clusters)
            {
                if (!cluster.Address.Equals(_address)) continue;
                if (cluster.Content is ContentFileCluster file)
                {
                    cluster.IsSelected = true;
                    //Previous cluster
                    while (file.PreviousCluster != null)
                    {
                        file.PreviousCluster.IsSelected = _select;
                        file = file.PreviousCluster.Content as ContentFileCluster;
                    }
                    //Next cluster
                    file = (ContentFileCluster)cluster.Content;
                    while (file.NextCluster != null)
                    {
                        file.NextCluster.IsSelected = _select;
                        file = file.NextCluster.Content as ContentFileCluster;
                    }
                    break;
                }
            }
        }

        public override IEnumerable<int> GetIndexFragmentClusterFileOfFile(File _file)
        {
            var listIndex = new List<int>();

            //Add index of all cluster to empty
            var file = _file.FragmentFiles.First();
            if (file != null)
            {
                for (var i = 0; i < Clusters.Count; i++)
                {
                    var cluster = Clusters[i];
                    if (cluster.Content == file)
                    {
                        listIndex.Add(i);
                        break;
                    }
                }
            }
            while (file.NextCluster != null)
            {
                listIndex.Add(Clusters.IndexOf(file.NextCluster));
                file = file.NextCluster.Content as ContentFileCluster;
            }
            return listIndex;
        }

        public void ModifySelectedFile()
        {
            if (SelectedFile == null) return;
            
            var oldSize = ((FatFileMetada)SelectedFile.Metadata).FileSize;

            var fileWindow = new Windows.FileWindow(SelectedFile)
            {
                ConfirmContent = "Modify",
                Owner = Application.Current.MainWindow
            };
            fileWindow.ShowDialog();

            if (!fileWindow.IsConfirmed) return;

            var newFile = fileWindow.File;
            ((FatFileMetada)newFile.Metadata).WriteDate = new DateTime().Date;
            ((FatFileMetada)newFile.Metadata).WriteTime = DateTime.Now;
            ((FatFileMetada)newFile.Metadata).AccessDate = DateTime.Now;

            //Clear selection

            var newSize = ((FatFileMetada) newFile.Metadata).FileSize;

            if (newSize > oldSize)
            {
                var numberNewCluster = (int) Math.Ceiling((decimal)newSize / Settings.ClusterSize) - newFile.NumberClusterUse;
                newFile.IncreaseFileSize(oldSize);
                FileAllocationTable.IncreaseClusterFile(numberNewCluster, newFile);
            }
            else if (newSize < oldSize) //Smaller size
            {
                FileAllocationTable.DecreaseClusterFile(oldSize, newFile);
                newFile.DecreaseSizeFile(oldSize);
            }

            //Reselect
            SelectClustersByFile(newFile, true);
        }

        private bool NewFile(File _file)
        {
            var file = _file;
            var name = ((FatFileMetada)file.Metadata).FileName;
            var size = ((FatFileMetada)file.Metadata).FileSize;
            var ext = "txt";

            var numberCluster = (int)Math.Ceiling((decimal)size / Settings.ClusterSize);
            var enoughCluster = CheckIfEnoughCluster(numberCluster);

            if (!enoughCluster)
            {
                MessageBox.Show("Disk space full");
                return false;
            }

            //Update root directory
            {
                var needUpdate = RootDirectory.NeedUpdateRootDirectory();
                if (needUpdate)
                {
                    bool spaceDiskOk = AddRootDirectory();
                    if (!spaceDiskOk)
                    {
                        return false;
                    }
                }
            }
            //Allocate cluster and create file
            {
                var index = -1;
                if ((index = name.LastIndexOf(".", StringComparison.Ordinal)) > 0)
                {
                    index++;
                    ext = name.Substring(index, name.Length - index);
                    if (ext.Equals(""))
                    {
                        ext = "txt";
                    }
                }

                ((FatFileMetada)file.Metadata).CreatedTime = DateTime.Now;
                ((FatFileMetada)file.Metadata).WriteTime = DateTime.Now;
                ((FatFileMetada)file.Metadata).Extension = ext;
                ((FatFileMetada)file.Metadata).AccessDate = DateTime.Now;
                ((FatFileMetada)file.Metadata).FileCreationTime = (byte)random.Next(10, 300);
                ((FatFileMetada)file.Metadata).WriteDate = DateTime.Now;
                ((FatFileMetada)file.Metadata).CreatedDate = DateTime.Now;
                ((FatFileMetada)file.Metadata).Attribut = EnumFileAttribut.Archive;
                ((FatFileMetada)file.Metadata).Reserved = 0;

                AllocateCluster(file, name, size);
                FileAllocationTable.AddFileInCluster(file);
                Files.Add(file);
            }
            return true;
        }

        public override bool NewFile()
        {
            var fileWindow = new Windows.FileWindow
            {
                ConfirmContent = "Create",
                Owner = Application.Current.MainWindow
            };
            fileWindow.ShowDialog();

            if (!fileWindow.IsConfirmed) return false;

            return NewFile(fileWindow.File);
        }

        private void AllocateCluster(File _file, string _name, int _size)
        {
            var numberCluster = (int) Math.Ceiling((decimal)_size / Settings.ClusterSize);
            if (numberCluster == 0)
            {
                numberCluster = 1;
            }
            _file.Create(numberCluster, _name, _size);
        }

        private bool CheckIfEnoughCluster(int _numberCluster)
        {
            foreach (var cluster in Clusters)
            {
                if (cluster.Content is EmptyContentCluster)
                {
                    _numberCluster--;
                }
                if (_numberCluster == 0)
                {
                    break;
                }
            }
            return _numberCluster <= 0;
        }

        private bool AddRootDirectory()
        {
            var emptyCluster = GetFirstEmptyCluster();
            if (emptyCluster != null)
            {
                if (RootDirectory.CurrentCluster.Content is RootDirectoryContentCluster content)
                {
                    content.NextCluster = emptyCluster;
                    var newContent = new RootDirectoryContentCluster {PreviousCluster = RootDirectory.CurrentCluster};
                    emptyCluster.Content = newContent;
                    RootDirectory.CurrentCluster = emptyCluster;
                }
                return true;
            }
            MessageBox.Show("Disk space full");
            return false;
        }

        private void DeleteRootDirectory()
        {
            if (RootDirectory.CurrentCluster.Content is RootDirectoryContentCluster content)
            {
                var previousCluster = content.PreviousCluster;
                if (previousCluster != null)
                {
                    RootDirectory.CurrentCluster.Content = new EmptyContentCluster();
                    if (previousCluster.Content is RootDirectoryContentCluster previousContent)
                    {
                        previousContent.NextCluster = null;
                        RootDirectory.CurrentCluster = previousCluster;
                    }
                }
            }
        }

        public FileAllocationTable FileAllocationTable
        {
            get => fileAllocationTable;
            set
            {
                fileAllocationTable = value;
                OnPropertyChanged(nameof(FileAllocationTable));
            }
        }

        public ICommand DeleteFileCommand { get; set; }
        public ICommand DeleteAllFileCommand { get; set; }
        public ICommand ModifyFileCommand { get; set; }
        public ICommand NewFileCommand { get; set; }

        public RootDirectory RootDirectory { get; set; }
    }
}
 
 