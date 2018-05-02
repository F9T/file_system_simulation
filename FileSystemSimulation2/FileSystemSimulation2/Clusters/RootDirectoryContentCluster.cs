using System.Collections.ObjectModel;
using FileSystemSimulation2.Clusters.Abstract;
using FileSystemSimulation2.Files;

namespace FileSystemSimulation2.Clusters
{
    public class RootDirectoryContentCluster : AbstractContentCluster
    {
        private Cluster nextCluster;
        private Cluster previousCluster;

        public RootDirectoryContentCluster()
        {
            Content = "RD";
            Color = Settings.RootDirectoryColor;
        }

        public ObservableCollection<File> Files { get; set; }

        public Cluster NextCluster
        {
            get => nextCluster;
            set
            {
                nextCluster = value;
                OnPropertyChanged(nameof(NextCluster));
            }
        }

        public Cluster PreviousCluster
        {
            get => previousCluster;
            set
            {
                previousCluster = value;
                OnPropertyChanged(nameof(PreviousCluster));
            }
        }
    }
}
