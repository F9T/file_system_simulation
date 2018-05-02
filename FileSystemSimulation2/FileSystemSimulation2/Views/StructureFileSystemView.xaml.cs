using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using FileSystemSimulation2.Filesystem.Structure;

namespace FileSystemSimulation2.Views
{
    /// <summary>
    /// Logique d'interaction pour StructureFileSystemView.xaml
    /// </summary>
    public partial class StructureFileSystemView : UserControl, INotifyPropertyChanged
    {
        private string selectedText;

        public StructureFileSystemView()
        {
            InitializeComponent();
        }

        public string SelectedText
        {
            get => selectedText;
            set
            {
                selectedText = value;
                OnPropertyChanged(nameof(SelectedText));
            }
        }

        private void SectorStructure_OnMouseEnter(object _sender, MouseEventArgs _e)
        {
            if (!(_sender is Border border)) return;
            if (border.DataContext is SectorStructureFileSystem sector)
            {
                SelectedText = sector.HelpText;
            }
        }

        private void SectorStructure_OnMouseLeave(object _sender, MouseEventArgs _e)
        {
            SelectedText = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
