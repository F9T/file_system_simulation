using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace FileSystemSimulation.FileSystem
{
    public class FileSystemSerializer
    {
        public static bool Serialize(FileSystem _fileSystem, string _path)
        {
            var fileInfo = new FileInfo(_path);
            if (fileInfo.Directory != null && fileInfo.Directory.Exists)
            {
                var serializer = new XmlSerializer(typeof(FileSystem));
                using (var sw = new StreamWriter(fileInfo.FullName))
                {
                    try
                    {
                        serializer.Serialize(sw, _fileSystem);
                    }
                    catch (InvalidOperationException e)
                    {
                        MessageBox.Show(e.Message);
                        return false;
                    }
                }
                return true;
            }
            MessageBox.Show("Path not found!");
            return false;
        }

        public static FileSystem Deserialize(string _path)
        {
            var fileInfo = new FileInfo(_path);
            if (fileInfo.Exists)
            {
                FileSystem fileSystem = null;
                var serializer = new XmlSerializer(typeof(FileSystem));
                using (var sr = new StreamReader(fileInfo.FullName))
                {
                    try
                    {
                        fileSystem = serializer.Deserialize(sr) as FileSystem;
                    }
                    catch (InvalidOperationException e)
                    {
                        MessageBox.Show(e.Message);
                        return null;
                    }
                }
                return fileSystem;
            }
            MessageBox.Show($"Path {fileInfo.FullName} not found!");
            return null;
        }
    }
}
