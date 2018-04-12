using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using FileSystemSimulation.Clusters;
using FileSystemSimulation.Files;

namespace FileSystemSimulation.Views
{
    /// <summary>
    /// Logique d'interaction pour ClusterTableView.xaml
    /// </summary>
    public partial class ClusterTableView : UserControl
    {
        private bool isClickEvent;

        public ClusterTableView()
        {
            InitializeComponent();
            isClickEvent = false;
        }

        private void ClusterSelector_OnSelectionChanged(object _sender, SelectionChangedEventArgs _e)
        {
            //Select only if listviewitem is pressed with mouse click
            if (isClickEvent)
            {
                if (_sender is ListView listView) listView.SelectedIndex = -1;
                isClickEvent = false;
                if (!(DataContext is MainViewModel mainViewModel)) return;

                var isSelected = true;
                Cluster cluster = null;
                if (_e.AddedItems.Count > 0)
                {
                    cluster = _e.AddedItems[0] as Cluster;
                }
                else if (_e.RemovedItems.Count > 0)
                {
                    cluster = _e.RemovedItems[0] as Cluster;
                    isSelected = false;
                }

                if (cluster != null)
                {
                     mainViewModel.GetClusterByFile(cluster);
                    foreach (var c in mainViewModel.SelectedClusters)
                    {
                        c.IsSelected = isSelected;
                    }
                }
            }
        }

        private void MouseLeftDown_ListViewOnHandler(object _sender, MouseButtonEventArgs _e)
        {
            isClickEvent = true;
        }

        private void MouseLeftUp_ListViewOnHandler(object _sender, MouseButtonEventArgs _e)
        {
            isClickEvent = false;
        }
    }
}