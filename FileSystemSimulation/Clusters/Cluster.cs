using System.ComponentModel;
using System.Runtime.CompilerServices;
using FileSystemSimulation.Files;

namespace FileSystemSimulation.Clusters
{
    public class Cluster : INotifyPropertyChanged
    {
        private AbstractClusterFile clusterFile;
        private bool isSelected;
        private bool isMouseEnter;

        public Cluster()
        {
            //Default value
            IsSelected = false;
            IsMouseEnter = false;
        }

        public string Address { get; set; }

        public AbstractClusterFile ClusterFile
        {
            get => clusterFile;
            set
            {
                clusterFile = value;
                OnPropertyChanged(nameof(ClusterFile));
            }
        }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public bool IsMouseEnter
        {
            get => isMouseEnter;
            set
            {
                isMouseEnter = value;
                OnPropertyChanged(nameof(IsMouseEnter));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}