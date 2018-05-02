using System.ComponentModel;
using System.Windows.Media;

namespace FileSystemSimulation2
{
    public class Settings
    {
        public Settings()
        {
            GlobalPropertyChanged += HandleGlobalPropertyChanged;
        }

        // 512
        public static int SectorSize = 32;
        // ClusterSize must be a multiple of SectorSize
        public static int ClusterSize = 4 * SectorSize;
        // size of one entry in root directory (32 octets)
        public static int SizeRootDirectoryEntry = 32;
        // max entries file in one cluster
        public static int MaxRootDirectoryEntries = ClusterSize / SizeRootDirectoryEntry;
        // color root directory in cluster
        public static SolidColorBrush RootDirectoryColor = new SolidColorBrush(Colors.LightCoral);
        // color reserved cluster
        public static SolidColorBrush ReservedClusterColor = new SolidColorBrush(Colors.DarkRed);        
        // color empty cluster
        public static SolidColorBrush EmptyClusterColor = new SolidColorBrush(Colors.LightGray);

        private static int _numberCluster = 500;

        // number cluster disk
        public static int NumberCluster
        {
            get => _numberCluster;
            set
            {
                _numberCluster = value;
                OnGlobalPropertyChanged(nameof(NumberCluster));
            }
        }

        private static event PropertyChangedEventHandler GlobalPropertyChanged = delegate { };

        private static void OnGlobalPropertyChanged(string _propertyName)
        {
            GlobalPropertyChanged(typeof(Settings), new PropertyChangedEventArgs(_propertyName));
        }

        private static void HandleGlobalPropertyChanged(object _sender, PropertyChangedEventArgs _e)
        {
            switch (_e.PropertyName)
            {
                case "NumberCluster":
                    _numberCluster = NumberCluster;
                    break;
            }
        }
    }
}   