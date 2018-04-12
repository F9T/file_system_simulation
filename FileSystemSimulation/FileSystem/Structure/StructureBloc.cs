using System;
using System.Xml.Serialization;

namespace FileSystemSimulation.FileSystem.Structure
{
    [Serializable]
    public class StructureBloc
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}