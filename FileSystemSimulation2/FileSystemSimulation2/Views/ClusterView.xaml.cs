using System.Windows.Controls;
using System.Windows.Input;
using FileSystemSimulation2.Clusters;
using FileSystemSimulation2.Filesystem;

namespace FileSystemSimulation2.Views
{
    /// <summary>
    /// Logique d'interaction pour ClusterView.xaml
    /// </summary>
    public partial class ClusterView : UserControl
    {
        public ClusterView()
        {
            InitializeComponent();
        }


        private void ClusterPanel_OnMouseEnter(object _sender, MouseEventArgs _e)
        {
            if (!(DataContext is AbstractFileSystem fileSystem)) return;
            if (!(_sender is StackPanel panel)) return;

            var tag = panel.Tag;
            if (tag == null) return;

            if (panel.Tag is Cluster cluster)
            {
                fileSystem.MouseEnterCluster(cluster);
            }
        }

        private void ClusterPanel_OnMouseLeftButtonDown(object _sender, MouseButtonEventArgs _e)
        {
            // If Ctrl key pressed -> deselect file (true select, false deselect)
            var select = !Keyboard.IsKeyDown(Key.LeftCtrl);
            if (_e.ClickCount == 1)
            {
                if (!(DataContext is AbstractFileSystem fileSystem)) return;
                if (!(_sender is StackPanel panel)) return;

                var tag = panel.Tag;
                if (tag == null) return;

                if (panel.Tag is Cluster cluster)
                {
                    fileSystem.SelectClustersByAddress(cluster.Address, select);
                    fileSystem.SelectFileByCluster(cluster, select);
                }
            }
        }

        private void ItemsControl_OnMouseLeave(object _sender, MouseEventArgs _e)
        {
            if (!(DataContext is AbstractFileSystem fileSystem)) return;
            fileSystem.ClearClusterMouseEnter();
        }
    }
}