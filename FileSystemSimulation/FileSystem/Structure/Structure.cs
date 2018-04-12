using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace FileSystemSimulation.FileSystem.Structure
{
    [Serializable]
    public class Structure
    {
        public Structure()
        {
            Sectors = new ObservableCollection<StructureBloc>();
        }

        [XmlArray(ElementName = "Blocs")]
        [XmlArrayItem("Structure", Type = typeof(StructureBloc))]
        public ObservableCollection<StructureBloc> Sectors { get; set; }
    }
}