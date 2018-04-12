using System.Windows.Media;
using FileSystemSimulation.Clusters;

namespace FileSystemSimulation.Files
{
    public class FragmentFile : AbstractFile
    {
        private SolidColorBrush color;
        private int size;

        public FragmentFile()
        {
            Size = 6300;
            Name = "file";
        }
        public Cluster PreviousCluster { get; set; }

        public Cluster NextCluster { get; set; }

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
