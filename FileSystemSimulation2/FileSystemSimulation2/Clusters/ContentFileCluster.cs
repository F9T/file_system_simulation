using FileSystemSimulation2.Clusters.Abstract;

namespace FileSystemSimulation2.Clusters
{
    public class ContentFileCluster : AbstractContentCluster
    {
        private int size;
        private Cluster nextCluster;
        private Cluster previousCluster;

        public int Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

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
