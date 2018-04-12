using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FileSystemSimulation.Files;

namespace FileSystemSimulation
{
    /// <summary>
    /// Logique d'interaction pour CreateFileWindow.xaml
    /// </summary>
    public partial class CreateFileWindow : INotifyPropertyChanged
    {
        private FragmentFile file;

        public CreateFileWindow()
        {
            InitializeComponent();
            File = new FragmentFile();
            DataContext = File;
            IsConfirmed = false;
        }

        public bool IsConfirmed { get; set; }

        public FragmentFile File
        {
            get => file;
            set
            {
                file = value;
                OnPropertyChanged(nameof(File));
            }
        }

        private void Cancel_OnClick(object _sender, RoutedEventArgs _e)
        {
            IsConfirmed = false;
            Close();
        }

        private void Confirm_OnClick(object _sender, RoutedEventArgs _e)
        {
            IsConfirmed = true;
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
