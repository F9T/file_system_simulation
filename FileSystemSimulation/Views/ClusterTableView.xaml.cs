using System;
using System.Windows.Controls;
using System.Windows.Input;
using FileSystemSimulation.Clusters;

namespace FileSystemSimulation.Views
{
    /// <summary>
    /// Logique d'interaction pour ClusterTableView.xaml
    /// </summary>
    public partial class ClusterTableView : UserControl
    {
        public ClusterTableView()
        {
            InitializeComponent();
        }

        private void ClusterPanel_OnMouseEnter(object _sender, MouseEventArgs _e)
        {
            if (!(DataContext is MainViewModel mainViewModel)) return;
            if (!(_sender is StackPanel panel)) return;

            var tag = panel.Tag;
            if (tag == null) return;

            if (panel.Tag is Cluster cluster)
            {
                mainViewModel.MouseEnterCluster(cluster);
            }
        }

        private void ClusterPanel_OnMouseLeftButtonDown(object _sender, MouseButtonEventArgs _e)
        {
            if (_e.ClickCount == 1)
            {
                if (!(DataContext is MainViewModel mainViewModel)) return;
                if (!(_sender is StackPanel panel)) return;

                var tag = panel.Tag;
                if (tag == null) return;

                if (panel.Tag is Cluster cluster)
                {
                    mainViewModel.SelectFileByClusterAddress(cluster.Address);
                }
            }
        }

        private void ItemsControl_OnMouseLeave(object _sender, MouseEventArgs _e)
        {
            if (!(DataContext is MainViewModel mainViewModel)) return;
            mainViewModel.MouseLeaveCluster();
        }
    }
}