using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FileSystemSimulation2.Clusters;
using FileSystemSimulation2.Files.Metadata;

namespace FileSystemSimulation2.Files
{
    public class File : INotifyPropertyChanged
    {
        private AbstractFileMetadata metadata;
        private ObservableCollection<ContentFileCluster> fragmentFiles;

        public File()
        {
            FragmentFiles = new ObservableCollection<ContentFileCluster>();
        }

        public AbstractFileMetadata Metadata
        {
            get => metadata;
            set
            {
                metadata = value;
                OnPropertyChanged(nameof(Metadata));
            }
        }

        public int NumberClusterUse { get; set; }

        public void Create(int _numberCluster, string _name, int _size)
        {
            NumberClusterUse = _numberCluster;
            for (var i = 0; i < _numberCluster; i++)
            {
                var fragmentFile = new ContentFileCluster
                {
                    Content = _name
                };
                var calcsize = Settings.ClusterSize;
                if (_size < Settings.ClusterSize)
                {
                    calcsize = _size;
                }
                fragmentFile.Size = calcsize;
                FragmentFiles.Add(fragmentFile);
                _size -= Settings.ClusterSize;
            }
        }

        public void IncreaseFileSize(int _oldSize)
        {
            //size
            var size = ((FatFileMetada)Metadata).FileSize;

            //New number cluster
            var nCluster = (int)Math.Ceiling((decimal)size / Settings.ClusterSize);
            var remainingSize = size - _oldSize;

            var color = FragmentFiles.First().Color;

            //Change size last cluster
            var lastFragmentFile = FragmentFiles.Last();
            remainingSize -= (Settings.ClusterSize - lastFragmentFile.Size);
            lastFragmentFile.Size = Settings.ClusterSize;

            //Calculate size of fragmentfile
            var calcSize = remainingSize;
            if (calcSize > Settings.ClusterSize)
            {
                calcSize = Settings.ClusterSize;
            }

            for (var i = NumberClusterUse + 1; i <= nCluster; i++)
            {
                FragmentFiles.Add(new ContentFileCluster
                {
                    Size = calcSize,
                    Color = color,
                    Content = ((FatFileMetada)Metadata).FileName
                });
                remainingSize -= Settings.ClusterSize;
                if (remainingSize < Settings.ClusterSize)
                {
                    calcSize = remainingSize;
                }
            }
            //Update NumberClusterUse
            NumberClusterUse = nCluster;
        }

        public void DecreaseSizeFile(int _oldSize)
        {
            //New number cluster
            var nCluster = (int)Math.Ceiling((decimal)((FatFileMetada)Metadata).FileSize / Settings.ClusterSize);

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
                lastFragmentFile.Size = ((FatFileMetada)Metadata).FileSize - ((nCluster - 1) * Settings.ClusterSize);
                lastFragmentFile.NextCluster = null;
            }
            //Update NumberClusterUse
            NumberClusterUse = nCluster;
        }

        public ObservableCollection<ContentFileCluster> FragmentFiles
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
