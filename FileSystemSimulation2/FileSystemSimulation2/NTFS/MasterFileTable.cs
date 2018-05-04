using FileSystemSimulation2.Filesystem;

namespace FileSystemSimulation2.NTFS
{
    public class MasterFileTable
    {
        private NtfsFileSystem fileSystem;

        public MasterFileTable(NtfsFileSystem _fileSystem)
        {
            fileSystem = _fileSystem;
        }


    }
}
