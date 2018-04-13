using System.Windows.Media;
using FileSystemSimulation.Clusters;

namespace FileSystemSimulation.Files
{
    public class FragmentClusterFile : AbstractClusterFile
    {
        private SolidColorBrush color;
        private int size;
        private Cluster nextCluster;
        private Cluster previousCluster;


        public Cluster PreviousCluster
        {
            get => previousCluster;
            set
            {
                previousCluster = value;
                OnPropertyChanged(nameof(PreviousCluster));
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

        public int Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        public SolidColorBrush Color
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }
    }
}
