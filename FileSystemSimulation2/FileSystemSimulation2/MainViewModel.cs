using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FileSystemSimulation2.Command;
using FileSystemSimulation2.Filesystem;

namespace FileSystemSimulation2
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private AbstractFileSystem selectedFileSystem;
        private ObservableCollection<AbstractFileSystem> fileSystems;

        public MainViewModel()
        {
            AutoGenerateCommand = new RelayCommand(_param => AutoGenerate(), _param => SelectedFileSystem != null);
            DefragmentationCommand = new RelayCommand(_param => Defragmentation(), _param => SelectedFileSystem != null);

            FileSystems = new ObservableCollection<AbstractFileSystem>
            {
                new FatFileSystem(),
                new NtfsFileSystem()
            };

            SelectedFileSystem = FileSystems[0];
        }

        private void AutoGenerate()
        {
            SelectedFileSystem.AutoGenerate();
        }

        private void Defragmentation()
        {
            SelectedFileSystem.Defragmentation();
        }

        public ObservableCollection<AbstractFileSystem> FileSystems
        {
            get => fileSystems;
            set
            {
                fileSystems = value;
                OnPropertyChanged(nameof(FileSystems));
            }
        }


        public AbstractFileSystem SelectedFileSystem
        {
            get => selectedFileSystem;
            set
            {
                selectedFileSystem = value;
                OnPropertyChanged(nameof(SelectedFileSystem));
            }
        }

        public ICommand AutoGenerateCommand { get; set; }
        public ICommand DefragmentationCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
