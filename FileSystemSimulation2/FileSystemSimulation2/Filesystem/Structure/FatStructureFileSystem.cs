namespace FileSystemSimulation2.Filesystem.Structure
{
    public class FatStructureFileSystem : AbstractStructureFileSystem
    {
        public FatStructureFileSystem()
        {
            StructureSectors.Add(new SectorStructureFileSystem { Name = "Boot Sector", HelpText = "Contains various information about the logical organization of the support."});
            StructureSectors.Add(new SectorStructureFileSystem { Name = "Reserved" });
            StructureSectors.Add(new SectorStructureFileSystem { Name = "File Allocation Table (FAT)", HelpText = "Specifies how clusters are organized based on files and directories."});
            StructureSectors.Add(new SectorStructureFileSystem { Name = "Mirror FAT" , HelpText = "Copy of File Allocation Table for redundancy." });
            StructureSectors.Add(new SectorStructureFileSystem { Name = "Root Directory" , HelpText = "The Root directory is the area where all directory and file information is stored." });
            StructureSectors.Add(new SectorStructureFileSystem { Name = "Data Area", HelpText = "Area where directory and file data are stored. The data save here, remain present even if files or directories are deleted."});
        }
    }
}
