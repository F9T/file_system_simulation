using MahApps.Metro.Controls;

namespace FileSystemSimulation2.Windows
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
        }

        public MainViewModel MainViewModel { get; set; }
    }
}
