using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FileSystemSimulation2.Files;
using FileSystemSimulation2.Files.Metadata;

namespace FileSystemSimulation2
{
    /// <summary>
    /// Logique d'interaction pour FileWindow.xaml
    /// </summary>
    public partial class FileWindow : INotifyPropertyChanged
    {
        private File file;

        public FileWindow()
        {
            InitializeComponent();
            IsConfirmed = true;
            File = new File {Metadata = new FatFileMetada {FileName = "test", FileSize = 1000}};
            DataContext = File;
        }

        public FileWindow(File _file)
        {
            InitializeComponent();
            IsConfirmed = true;
            File = _file;
            DataContext = File;
        }

        public File File
        {
            get => file;
            set
            {
                file = value;
                OnPropertyChanged(nameof(File));
            }
        }

        public string ConfirmContent { get; set; }

        public bool IsConfirmed { get; set; }

        private void Cancel_OnClick(object _sender, RoutedEventArgs _e)
        {
            Close();
            IsConfirmed = false;
        }

        private void Confirm_OnClick(object _sender, RoutedEventArgs _e)
        {
            Close();
            IsConfirmed = true;
        }

        private void FileWindow_OnClosed(object _sender, EventArgs _e)
        {
            IsConfirmed = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
