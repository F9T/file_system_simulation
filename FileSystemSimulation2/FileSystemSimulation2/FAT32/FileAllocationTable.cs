using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using FileSystemSimulation2.Clusters;
using FileSystemSimulation2.Files;
using FileSystemSimulation2.Files.Metadata;
using FileSystemSimulation2.Filesystem;

namespace FileSystemSimulation2.FAT32
{
    public class FileAllocationTable : INotifyPropertyChanged
    {
        private readonly FatFileSystem fileSystem;

        private readonly List<SolidColorBrush> useColors;
        private readonly Random random;

        public FileAllocationTable(FatFileSystem _fileSystem)
        {
            fileSystem = _fileSystem;
            //Initialize
            random = new Random(DateTime.Now.Millisecond);
            useColors = new List<SolidColorBrush>
            {
                //add the color of empty cluster and selected cluster
                new SolidColorBrush(Colors.CornflowerBlue),
                new SolidColorBrush(Colors.LightBlue),
                Settings.ReservedClusterColor,
                Settings.RootDirectoryColor,
                Settings.EmptyClusterColor
            };

            Initialize();
        }

        private void Initialize()
        {
            //Create all cluster with empty
            for (int i = 0, dec = 2; i < Settings.NumberCluster; i++, dec++)
            {
                string hexValue = $"0x{dec:X}";
             /*   Clusters.Add(new Cluster(hexValue)
                {
                    Content = new EmptyContentCluster()
                });*/
            }
        }

        private bool IsColorUsed(SolidColorBrush _color)
        {
            return Enumerable.Contains(useColors, _color);
        }

        public void UpdateRootDirectory()
        {

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
            ContentFileCluster saveLastClusterFile = null;
            Cluster saveLastCluster = null;
            foreach (var cluster in fileSystem.Clusters)
            {
                if (cluster.Content is EmptyContentCluster)
                {
                    if (first)
                    {
                        ((FatFileMetada)_file.Metadata).FirstCluster = cluster;
                        first = false;
                    }
                    if (saveLastClusterFile != null)
                    {
                        saveLastClusterFile.NextCluster = cluster;
                    }
                    var currentFragmentFile = _file.FragmentFiles[index];
                    currentFragmentFile.Color = color;
                    currentFragmentFile.PreviousCluster = saveLastCluster;
                    cluster.Content = currentFragmentFile;

                    saveLastClusterFile = (ContentFileCluster)cluster.Content;
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
            var color = _file.Metadata.FirstCluster.Content.Color;
            var start = (_file.FragmentFiles.Count - _numberNewCluster) - 1;

            //Get old last fragmentfile
            var oldFragmentFile = _file.FragmentFiles.ElementAt(start);

            var saveLastClusterFile = oldFragmentFile;
            var saveLastCluster = fileSystem.GetClusterByFragmentFile(oldFragmentFile);
            //Add new file to cluster
            for (var i = start + 1; i < _file.FragmentFiles.Count; i++)
            {
                var fragmentFile = _file.FragmentFiles.ElementAt(i);
                var cluster = fileSystem.GetFirstEmptyCluster();
                if (cluster != null)
                {
                    if (saveLastClusterFile != null)
                    {
                        saveLastClusterFile.NextCluster = cluster;
                    }
                    fragmentFile.Color = color;
                    fragmentFile.PreviousCluster = saveLastCluster;
                    cluster.Content = fragmentFile;

                    saveLastClusterFile = (ContentFileCluster)cluster.Content;
                    saveLastCluster = cluster;
                }
            }
        }


        public void DecreaseClusterFile(int _oldSize, File _file)
        {
            //New number cluster
            var nCluster = (int)Math.Ceiling((decimal)((FatFileMetada)_file.Metadata).FileSize / Settings.ClusterSize);

            //Number clear cluster
            var numberClusterToDelete = _file.NumberClusterUse - nCluster;

            //Get index of cluster to remove
            var listIndex = (List<int>)fileSystem.GetIndexFragmentClusterFileOfFile(_file);

            //Clear clusters
            for (var i = 0; i < numberClusterToDelete; i++)
            {
                var cluster = fileSystem.Clusters.ElementAt(listIndex.Last());
                cluster.Content = new EmptyContentCluster();
                listIndex.RemoveAt(listIndex.Count - 1);
            }

            if (listIndex.Count > 0)
            {
                //Update cluster
                var cluster = fileSystem.Clusters.ElementAt(listIndex.Last());
                if (cluster.Content is ContentFileCluster fragmentFile)
                {
                    fragmentFile.NextCluster = null;
                }
            }
        }

        public void DeleteFile(File _file)
        {/*
            //Clear selection
            SelectionClear();

            //Get index of cluster to remove
            var listIndex = GetIndexFragmentClusterFileOfFile(_file);

            //Clear cluster
            foreach (var index in listIndex)
            {
                Clusters.ElementAt(index).ClusterFile = new EmptyClusterFile();
            }*/
        }

        public void SelectionClear()
        {/*
            foreach (var cluster in Clusters)
            {
                cluster.IsSelected = false;
            }*/
        }

        public void ClearClusterMouseEnter()
        {/*
            foreach (var cluster in Clusters)
            {
                cluster.IsMouseEnter = false;
            }*/
        }

        public void SelectClusters(string _address)
        {/*
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
            }*/
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
