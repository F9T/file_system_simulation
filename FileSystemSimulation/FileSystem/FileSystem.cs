using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using FileSystemSimulation.Clusters;

namespace FileSystemSimulation.FileSystem
{
    [Serializable]
    public class FileSystem : INotifyPropertyChanged
    {
        private ClusterTable clusterTable;

        public FileSystem()
        {
            ClusterTable = new ClusterTable();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Structure")]
        public Structure.Structure Structure { get; set; }

        [XmlIgnore]
        public ClusterTable ClusterTable
        {
            get => clusterTable;
            set
            {
                clusterTable = value;
                OnPropertyChanged(nameof(ClusterTable));
            }
        }

        public List<Cluster> GetClustersFile(string _address)
        {
            return ClusterTable.GetClustersFile(_address);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
