using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FileSystemSimulation
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private MainViewModel mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
        }

        public MainViewModel MainViewModel
        {
            get => mainViewModel;
            set
            {
                mainViewModel = value;
                OnPropertyChanged(nameof(MainViewModel));
            }
        }

        private void CreateFile_OnClick(object _sender, RoutedEventArgs _e)
        {
            MainViewModel.CreateFile();
        }

        private void UpdateFile_OnClick(object _sender, RoutedEventArgs _e)
        {
            MainViewModel.UpdateFile();
        }

        private void DeleteFile_OnClick(object _sender, RoutedEventArgs _e)
        {
            MainViewModel.DeleteFile();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
