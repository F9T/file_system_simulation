using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FileSystemSimulation.Clusters;
using FileSystemSimulation.RootDirectory;

namespace FileSystemSimulation.Files
{
    public class File : INotifyPropertyChanged
    {
        private ObservableCollection<FragmentClusterFile> fragmentFiles;
        private int size;
        private string name;
        private int numberClusterUse;
        private Cluster startCluster;
        private string shortFileName;
        private string extension;
        private EnumAttributFile attribut;
        private int fileCreatedTime;
        private TimeSpan createdTime;
        private DateTime createdDate;
        private DateTime accessDate;
        private TimeSpan writeTime;
        private DateTime writeDate;

        public File()
        {
            //Default value
            Size = 6300;
            Name = "foo";
            FragmentFiles = new ObservableCollection<FragmentClusterFile>();
        }

        public File(string _name, int _size)
        {
            Size = _size;
            Name = _name;
            FragmentFiles = new ObservableCollection<FragmentClusterFile>();
            InitializeCluster();
        }

        public void InitializeCluster()
        {
            //Create fragment clusterFile for cluster
            NumberClusterUse = (int) Math.Ceiling((decimal) Size / Settings.Settings.ClusterSize);
            if (NumberClusterUse <= 0)
            {
                return;
            }
            var fragmentFileSize = Size;
            if (NumberClusterUse > 1)
            {
                fragmentFileSize = Settings.Settings.ClusterSize;
            }
            var totalFileSize = Size;
            for (var i = 1; i <= NumberClusterUse; i++)
            {
                var fragmentFile = new FragmentClusterFile
                {
                    Name = Name + $"_{i}",
                    Size = fragmentFileSize
                };
                FragmentFiles.Add(fragmentFile);
                totalFileSize -= Settings.Settings.ClusterSize;
                if (totalFileSize < Settings.Settings.ClusterSize)
                {
                    fragmentFileSize = totalFileSize;
                }
            }
        }

        public void IncreaseSizeFile(int _oldSize)
        {
            //New number cluster
            var nCluster = (int) Math.Ceiling((decimal)Size / Settings.Settings.ClusterSize);
            var remainingSize = Size - _oldSize;

            var color = FragmentFiles.First().Color;

            //Change size last cluster
            var lastFragmentFile = FragmentFiles.Last();
            remainingSize -= (Settings.Settings.ClusterSize - lastFragmentFile.Size);
            lastFragmentFile.Size = Settings.Settings.ClusterSize;
            
            //Calculate size of fragmentfile
            var calcSize = remainingSize;
            if (calcSize > Settings.Settings.ClusterSize)
            {
                calcSize = Settings.Settings.ClusterSize;
            }
            
            for (var i = NumberClusterUse + 1; i <= nCluster; i++)
            {
                FragmentFiles.Add(new FragmentClusterFile
                {
                    Name = Name + $"_{i}",
                    Size = calcSize,
                    Color = color
                });
                remainingSize -= Settings.Settings.ClusterSize;
                if (remainingSize < Settings.Settings.ClusterSize)
                {
                    calcSize = remainingSize;
                }
            }
            //Update NumberClusterUse
            NumberClusterUse = nCluster;
        }

        public void DecreseSizeFile(int _oldSize)
        {
            //New number cluster
            var nCluster = (int) Math.Ceiling((decimal)Size / Settings.Settings.ClusterSize);

            //Remove fragmentfile
            for (var i = nCluster; i < NumberClusterUse; i++)
            {
                if (FragmentFiles.Count > 0)
                {
                    FragmentFiles.RemoveAt(FragmentFiles.Count - 1);
                }
            }
            if (FragmentFiles.Count > 0)
            {
                //Update last fragment size and nextcluster
                var lastFragmentFile = FragmentFiles.Last();
                lastFragmentFile.Size = Size - ((nCluster - 1) * Settings.Settings.ClusterSize);
                lastFragmentFile.NextCluster = null;
            }
            //Update NumberClusterUse
            NumberClusterUse = nCluster;
        }

        public string ShortFileName
        {
            get => shortFileName;
            set
            {
                shortFileName = value;
                OnPropertyChanged(nameof(ShortFileName));
            }
        }

        public string Extension
        {
            get => extension;
            set
            {
                extension = value;
                OnPropertyChanged(nameof(Extension));
            }
        }

        public EnumAttributFile Attribut
        {
            get => attribut;
            set
            {
                attribut = value;
                OnPropertyChanged(nameof(Attribut));
            }
        }

        public int FileCreatedTime
        {
            get => fileCreatedTime;
            set
            {
                fileCreatedTime = value;
                OnPropertyChanged(nameof(FileCreatedTime));
            }
        }

        public TimeSpan CreatedTime
        {
            get => createdTime;
            set
            {
                createdTime = value;
                OnPropertyChanged(nameof(CreatedTime));
            }
        }

        public DateTime CreatedDate
        {
            get => createdDate;
            set
            {
                createdDate = value;
                OnPropertyChanged(nameof(CreatedDate));
            }
        }

        public DateTime AccessDate
        {
            get => accessDate;
            set
            {
                accessDate = value;
                OnPropertyChanged(nameof(AccessDate));
            }
        }

        public TimeSpan WriteTime
        {
            get => writeTime;
            set
            {
                writeTime = value;
                OnPropertyChanged(nameof(WriteTime));
            }
        }

        public DateTime WriteDate
        {
            get => writeDate;
            set
            {
                writeDate = value;
                OnPropertyChanged(nameof(WriteDate));
            }
        }

        public int NumberClusterUse
        {
            get => numberClusterUse;
            set
            {
                numberClusterUse = value;
                OnPropertyChanged(nameof(NumberClusterUse));
            }
        }

        public Cluster StartCluster
        {
            get => startCluster;
            set
            {
                startCluster = value;
                OnPropertyChanged(nameof(StartCluster));
            }
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

        public int Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        public ObservableCollection<FragmentClusterFile> FragmentFiles
        {
            get => fragmentFiles;
            set
            {
                fragmentFiles = value;
                OnPropertyChanged(nameof(FragmentFiles));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
