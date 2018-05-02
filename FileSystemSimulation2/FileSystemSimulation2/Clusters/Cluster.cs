using System.ComponentModel;
using System.Runtime.CompilerServices;
using FileSystemSimulation2.Clusters.Abstract;

namespace FileSystemSimulation2.Clusters
{
    public class Cluster : INotifyPropertyChanged
    {
        private AbstractContentCluster content;
        private bool isSelected, isMouseEnter;

        public Cluster(string _address)
        {
            Address = _address;
        }

        public string Address { get; set; }

        public AbstractContentCluster Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
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