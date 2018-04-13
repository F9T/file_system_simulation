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
        private ObservableCollection<Cluster> clusters;

        private readonly List<SolidColorBrush> useColors;
        private readonly Random random;

        public ClusterTable()
        {
            //Initialize
            random = new Random(DateTime.Now.Millisecond);
            useColors = new List<SolidColorBrush>
            {
                //add the color of empty cluster and selected cluster
                new SolidColorBrush(Colors.LightGray),
                new SolidColorBrush(Colors.CornflowerBlue),
                new SolidColorBrush(Colors.LightBlue)
            }; 
            Clusters = new ObservableCollection<Cluster>();

            Initialize();
        }

        private void Initialize()
        {
            //Create all cluster with empty
            for (int i = 0, dec = 2; i < Settings.Settings.NumberCluster; i++, dec++)
            {
                string hexValue = $"0x{dec:X}";
                Clusters.Add(new Cluster
                {
                    ClusterFile = new EmptyClusterFile(),
                    Address = hexValue
                });
            }
        }

        private bool IsColorUsed(SolidColorBrush _color)
        {
            return Enumerable.Contains(useColors, _color);
        }

        public void AddFileInCluster(File _file)
        {
            //Random choose color if not exists
            SolidColorBrush color;
            do
            {
                color = new SolidColorBrush(Color.FromRgb((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255)));
            } while (IsColorUsed(color));
            
            var index = 0;
            var first = true;

            //Save last cluster for assign next cluster to file
            FragmentClusterFile saveLastClusterFile = null;
            Cluster saveLastCluster = null;
            foreach (var cluster in Clusters)
            {
                if (cluster.ClusterFile is EmptyClusterFile)
                {
                    if (first)
                    {
                        _file.StartCluster = cluster;
                        first = false;
                    }
                    if (saveLastClusterFile != null)
                    {
                        saveLastClusterFile.NextCluster = cluster;
                    }
                    var currentFragmentFile = _file.FragmentFiles[index];
                    currentFragmentFile.Color = color;
                    currentFragmentFile.PreviousCluster = saveLastCluster;
                    cluster.ClusterFile = currentFragmentFile;

                    saveLastClusterFile = (FragmentClusterFile) cluster.ClusterFile;
                    saveLastCluster = cluster;
                    index++;
                }
                //if all files are initialized  
                if (index == _file.FragmentFiles.Count)
                {
                    break;
                }
            }
        }

        public void IncreaseClusterFile(int _numberNewCluster, File _file)
        {
            var color = ((FragmentClusterFile) _file.StartCluster.ClusterFile).Color;
            var start = (_file.FragmentFiles.Count - _numberNewCluster) - 1;

            //Get old last fragmentfile
            var oldFragmentFile = _file.FragmentFiles.ElementAt(start);
            
            var saveLastClusterFile = oldFragmentFile;
            var saveLastCluster = GetClusterByFragmentFile(oldFragmentFile);
            //Add new file to cluster
            for (var i = start + 1; i < _file.FragmentFiles.Count; i++)
            {
                var fragmentFile = _file.FragmentFiles.ElementAt(i);
                var cluster = GetFirstEmptyCluster();
                if (cluster != null)
                {
                    if (saveLastClusterFile != null)
                    {
                        saveLastClusterFile.NextCluster = cluster;
                    }
                    fragmentFile.Color = color;
                    fragmentFile.PreviousCluster = saveLastCluster;
                    cluster.ClusterFile = fragmentFile;

                    saveLastClusterFile = (FragmentClusterFile) cluster.ClusterFile;
                    saveLastCluster = cluster;
                }
            }
        }

        private Cluster GetClusterByFragmentFile(FragmentClusterFile _fragmentFile)
        {
            return Clusters.FirstOrDefault(_cluster => _cluster.ClusterFile == _fragmentFile);
        }

        public void DecreaseClusterFile(int _oldSize, File _file)
        {
            //New number cluster
            var nCluster = (int)Math.Ceiling((decimal)_file.Size / Settings.Settings.ClusterSize);

            //Number clear cluster
            var numberClusterToDelete = _file.NumberClusterUse - nCluster;

            //Get index of cluster to remove
            var listIndex = (List<int>)GetIndexFragmentClusterFileOfFile(_file);

            //Clear clusters
            for (var i = 0; i < numberClusterToDelete; i++)
            {
                var cluster = Clusters.ElementAt(listIndex.Last());
                cluster.ClusterFile = new EmptyClusterFile();
                listIndex.RemoveAt(listIndex.Count - 1);
            }

            if (listIndex.Count > 0)
            {
                //Update cluster
                var cluster = Clusters.ElementAt(listIndex.Last());
                if (cluster.ClusterFile is FragmentClusterFile fragmentFile)
                {
                    fragmentFile.NextCluster = null;
                }
            }
        }

        private Cluster GetFirstEmptyCluster()
        {
            return Clusters.FirstOrDefault(_cluster => _cluster.ClusterFile is EmptyClusterFile);
        }

        private IEnumerable<int> GetIndexFragmentClusterFileOfFile(File _file)
        {
            var listIndex = new List<int> { Clusters.IndexOf(_file.StartCluster) };

            //Add index of all cluster to empty
            var file = _file.FragmentFiles.First();
            while (file.NextCluster != null)
            {
                listIndex.Add(Clusters.IndexOf(file.NextCluster));
                file = file.NextCluster.ClusterFile as FragmentClusterFile;
            }
            return listIndex;
        }

        public void DeleteFile(File _file)
        {
            //Clear selection
            SelectionClear();

            //Get index of cluster to remove
            var listIndex = GetIndexFragmentClusterFileOfFile(_file);

            //Clear cluster
            foreach (var index in listIndex)
            {
                Clusters.ElementAt(index).ClusterFile = new EmptyClusterFile();
            }
        }

        public void SelectionClear()
        {
            foreach (var cluster in Clusters)
            {
                cluster.IsSelected = false;
            }
        }

        public void ClearClusterMouseEnter()
        {
            foreach (var cluster in Clusters)
            {
                cluster.IsMouseEnter = false;
            }
        }

        public void SelectClusters(string _address)
        {
            SelectionClear();
            foreach (var cluster in Clusters)
            {
                if (!cluster.Address.Equals(_address)) continue;
                var file = cluster.ClusterFile as FragmentClusterFile;
                if (file != null)
                {
                    cluster.IsSelected = true;
                    //Previous cluster
                    while (file.PreviousCluster != null)
                    {
                        file.PreviousCluster.IsSelected = true;
                        file = file.PreviousCluster.ClusterFile as FragmentClusterFile;
                    }
                    //Next cluster
                    file = (FragmentClusterFile)cluster.ClusterFile;
                    while (file.NextCluster != null)
                    {
                        file.NextCluster.IsSelected = true;
                        file = file.NextCluster.ClusterFile as FragmentClusterFile;
                    }
                    break;
                }
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
