using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Windows;
using System.Windows.Input;
using FileSystemSimulation2.Clusters;
using FileSystemSimulation2.Command;
using FileSystemSimulation2.FAT32;
using FileSystemSimulation2.Files;
using FileSystemSimulation2.Files.Metadata;
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
            NewFileCommand = new RelayCommand(_param => NewFile(), _param => true);

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

        private bool NewFile(File _file)
        {
            var file = _file;
            var name = ((FatFileMetada)file.Metadata).FileName;
            var size = ((FatFileMetada)file.Metadata).FileSize;
            var ext = "txt";


            //Allocate cluster and create file
            {
                var metadata = new NtfsMetadata
                {
                    StandardInformation = new StandardInformation
                    {
                        CreatedTime = DateTime.Now,
                        AccessDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        DateMftRecordModified = DateTime.Now,
                        DateModified = DateTime.Now
                    },
                    EntryFileName = new EntryFileName
                    {
                        CreatedTime = DateTime.Now,
                        AccessDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        DateMftRecordModified = DateTime.Now,
                        DateModified = DateTime.Now,
                        Name = name,
                        ParentDirectory = "parent"
                    }
                };
                file.Metadata = metadata;
                Files.Add(file);
            }
            return true;
        }

        public override bool NewFile()
        {
            var fileWindow = new Windows.FileWindow
            {
                ConfirmContent = "Create",
                Owner = Application.Current.MainWindow
            };
            fileWindow.ShowDialog();

            if (!fileWindow.IsConfirmed) return false;

            return NewFile(fileWindow.File);
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
        public ICommand NewFileCommand { get; set; }
    }
}
