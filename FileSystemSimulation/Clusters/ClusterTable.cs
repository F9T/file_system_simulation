using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using FileSystemSimulation.Files;

namespace FileSystemSimulation.Clusters
{
    public class ClusterTable : INotifyPropertyChanged
    {
        private int numberCluster, sectorSize, clusterSize;
        private ObservableCollection<Cluster> clusters;

        private List<SolidColorBrush> useColors;
        private readonly Random random;

        public ClusterTable()
        {
            //Initialize
            random = new Random(DateTime.Now.Millisecond);
            useColors = new List<SolidColorBrush> {new SolidColorBrush(Colors.LightGray)}; //add the color of empty cluster
            Clusters = new ObservableCollection<Cluster>();

            //Default size
            SectorSize = 512;
            NumberCluster = 50;

            //ClusterSize must be a multiple of SectorSize
            ClusterSize = 4 * SectorSize;

            Initialize();
        }

        private void Initialize()
        {
            //Create all cluster with empty
            for (int i = 0, dec = 2; i < NumberCluster; i++, dec++)
            {
                string hexValue = $"0x{dec:X}";
                Clusters.Add(new Cluster
                {
                    Size = ClusterSize,
                    File = new EmptyFile(),
                    Address = hexValue
                });
            }
        }

        private bool IsColorUsed(SolidColorBrush _color)
        {
            return Enumerable.Contains(useColors, _color);
        }

        public void CreateFile(string _name, int _totalFileSize)
        {
            //Random choose color if not exists
            SolidColorBrush color;
            do
            {
                color = new SolidColorBrush(Color.FromRgb((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255)));
            } while (IsColorUsed(color));
            
            //Number cluster us to store file
            var nCluster = (int) Math.Ceiling((decimal)_totalFileSize / ClusterSize);
            var fragmentFileSize = _totalFileSize;
            if (nCluster > 1)
            {
                fragmentFileSize = ClusterSize;
            }
            FragmentFile saveLastFile = null;
            Cluster saveLastCluster = null;
            for (var i = 0; i < Clusters.Count; i++)
            {
                var cluster = Clusters.ElementAt(i);
                if (cluster.File is EmptyFile)
                {
                    if (saveLastFile != null)
                    {
                        saveLastFile.NextCluster = cluster;
                    }
                    cluster.File = new FragmentFile { PreviousCluster = saveLastCluster, Name = _name, Size = fragmentFileSize, Color = color };
                    saveLastFile = (FragmentFile) cluster.File;
                    saveLastCluster = cluster;
                    nCluster--;
                    _totalFileSize -= ClusterSize;
                    if (_totalFileSize < ClusterSize)
                    {
                        fragmentFileSize = _totalFileSize;
                    }
                }
                if (nCluster == 0)
                {
                    break;
                }
            }
        }

        public List<Cluster> GetClustersFile(string _address)
        {
            var list = new List<Cluster>();

            foreach (var cluster in Clusters)
            {
                if (!cluster.Address.Equals(_address)) continue;
                var file = cluster.File as FragmentFile;
                if (file != null)
                {
                    list.Add(cluster);
                    //Previous cluster
                    while (file.PreviousCluster != null)
                    {
                        list.Add(file.PreviousCluster);
                        file = file.PreviousCluster.File as FragmentFile;
                    }
                    //Next cluster
                    file = (FragmentFile) cluster.File;
                    while (file.NextCluster != null)
                    {
                        list.Add(file.NextCluster);
                        file = file.NextCluster.File as FragmentFile;
                    }
                    break;
                }
            }

            return list;
        }

        public int NumberCluster
        {
            get => numberCluster;
            set
            {
                numberCluster = value;
                OnPropertyChanged(nameof(NumberCluster));
            }
        }

        public int ClusterSize
        {
            get => clusterSize;
            set
            {
                clusterSize = value;
                OnPropertyChanged(nameof(ClusterSize));
            }
        }

        public int SectorSize
        {
            get => sectorSize;
            set
            {
                sectorSize = value;
                OnPropertyChanged(nameof(SectorSize));
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
