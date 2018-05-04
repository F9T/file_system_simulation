using System.Collections.Generic;
using FileSystemSimulation2.Clusters;
using FileSystemSimulation2.Files;
using FileSystemSimulation2.Filesystem.Structure;
using FileSystemSimulation2.NTFS;

namespace FileSystemSimulation2.Filesystem
{
    public class NtfsFileSystem : AbstractFileSystem
    {
        public NtfsFileSystem()
        { 
            Name = "NTFS";
            Structure = new NtfsStructureFileSystem();
            MasterFileTable = new MasterFileTable(this);

            Initialize();
        }

        private void Initialize()
        {

        }

        public override void RemoveFile(File _file)
        {

        }

        protected override void RemoveAllFiles()
        {
            throw new System.NotImplementedException();
        }

        public override void AutoGenerate()
        {

        }

        public override void Defragmentation()
        {

        }

        public override bool NewFile()
        {
            return false;
        }

        public override Cluster GetFirstEmptyCluster()
        {
            return null;
        }

        public override IEnumerable<int> GetIndexFragmentClusterFileOfFile(File _file)
        {
            return null;
        }

        public override void MouseEnterCluster(Cluster _cluster)
        {

        }

        public override void ClearClusterMouseEnter()
        {

        }

        public override void ClearSelection()
        {

        }

        public override void SelectClustersByFile(File _file, bool _select)
        {

        }

        public override void SelectFileByCluster(Cluster _cluster, bool _select)
        {

        }

        public override void SelectClustersByAddress(string _address, bool _select)
        {

        }

        public MasterFileTable MasterFileTable { get; set; }
    }
}
