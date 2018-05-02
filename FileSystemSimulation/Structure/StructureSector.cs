namespace FileSystemSimulation.Structure
{
    public class StructureSector
    {
        public StructureSector(string _name, int _sizeOctet)
        {
            Name = _name;
            SizeOctet = _sizeOctet;
        }

        public string Name { get; set; }

        public int SizeOctet { get; set; }
    }
}
