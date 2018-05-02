using System.Collections.Generic;
using FileSystemSimulation2.Clusters;
using FileSystemSimulation2.Files;

namespace FileSystemSimulation2.Filesystem
{
    public class NtfsFileSystem : AbstractFileSystem
    {
        public NtfsFileSystem()
        { 
            Name = "NTFS";
        }

        protected override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveSelectedFile()
        {
            throw new System.NotImplementedException();
        }

        public override void AutoGenerate()
        {
            throw new System.NotImplementedException();
        }

        public override void Defragmentation()
        {
            throw new System.NotImplementedException();
        }

        public override void NewFile()
        {
            
        }

        public override Cluster GetFirstEmptyCluster()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<int> GetIndexFragmentClusterFileOfFile(File _file)
        {
            throw new System.NotImplementedException();
        }

        public override void MouseEnterCluster(Cluster _cluster)
        {
            throw new System.NotImplementedException();
        }

        public override void ClearClusterMouseEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void ClearSelection()
        {
            throw new System.NotImplementedException();
        }

        public override void SelectClustersByFile(File _file, bool _select)
        {
            throw new System.NotImplementedException();
        }

        public override void SelectFileByCluster(Cluster _cluster, bool _select)
        {
            throw new System.NotImplementedException();
        }

        public override void SelectClustersByAddress(string _address, bool _select)
        {
            throw new System.NotImplementedException();
        }
    }
}
