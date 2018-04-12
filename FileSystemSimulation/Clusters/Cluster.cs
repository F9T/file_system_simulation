using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FileSystemSimulation.Files;

namespace FileSystemSimulation.Clusters
{
    public class Cluster : INotifyPropertyChanged
    {
        private AbstractFile file;
        private bool isSelected;

        public Cluster()
        {
            //Default value
            IsSelected = false;
        }

        public int Size { get; set; }

        public string Address { get; set; }

        public AbstractFile File
        {
            get => file;
            set
            {
                file = value;
                OnPropertyChanged(nameof(File));
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

            public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}