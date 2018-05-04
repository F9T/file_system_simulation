using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FileSystemSimulation2.Windows
{
    /// <summary>
    /// Logique d'interaction pour GenerateFileWindow.xaml
    /// </summary>
    public partial class GenerateFileWindow : INotifyPropertyChanged
    {
        private int numberFile;

        public GenerateFileWindow()
        {
            InitializeComponent();
            DataContext = this;
            IsConfirmed = false;
        }

        public int NumberFile
        {
            get => numberFile;
            set
            {
                numberFile = value;
                OnPropertyChanged(nameof(NumberFile));
            }
        }

        public bool IsConfirmed { get; set; }

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
