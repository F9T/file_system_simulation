using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace FileSystemSimulation2.Clusters.Abstract
{
    public abstract class AbstractContentCluster : INotifyPropertyChanged
    {
        private string content;
        private SolidColorBrush color;

        public SolidColorBrush Color
        {
            get => color;
            set
            {
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
