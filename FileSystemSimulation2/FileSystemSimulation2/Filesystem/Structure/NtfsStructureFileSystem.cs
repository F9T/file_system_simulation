namespace FileSystemSimulation2.Filesystem.Structure
{
    public class NtfsStructureFileSystem : AbstractStructureFileSystem
    {
        public NtfsStructureFileSystem()
        {
            StructureSectors.Add(new SectorStructureFileSystem { Name = "NTFS Boot Sector", HelpText = "Contains various information about the logical organization of the support." });
            StructureSectors.Add(new SectorStructureFileSystem { Name = "Master File Table (MFT)", HelpText = "All information about a file, including its size, time and date stamps, permissions, and data content, is stored either in MFT entries." });
            StructureSectors.Add(new SectorStructureFileSystem { Name = "File System Data", HelpText = "Specifies how clusters are organized based on files and directories." });
            StructureSectors.Add(new SectorStructureFileSystem { Name = "Master File Table Copy", HelpText = "Area where directory and file data are stored. The data savec here, remain present even if files or directories are deleted." });
        }
    }
}
