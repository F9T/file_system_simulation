using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FileSystemSimulation.FileSystem;
using FileSystemSimulation.FileSystem.Structure;

namespace FileSystemGenerator
{
    public class Program
    {
        public static void Main(string[] _args)
        {
            Console.Write(@"Enter path save : ");
            string path = Console.ReadLine();

            if (path != null)
            {
                var fileInfo = new FileInfo(path);
                if (fileInfo.Directory == null || !fileInfo.Directory.Exists)
                {
                    Console.WriteLine($@"Error path {path}");
                    return;
                }
            }


            Console.Write(@"Enter name file system : ");
            string name = Console.ReadLine();

            if (name == null)
            {
                Console.WriteLine(@"Error name");
                return;
            }

            Console.Write(@"Enter sectors name (separate with ; ) : ");
            string sectorsStr = Console.ReadLine();

            if (sectorsStr == null)
            {
                Console.WriteLine(@"Error sectors name");
                return;
            }

            string[] sectors = null;
            if (sectorsStr.Contains(";"))
            {
                sectors = sectorsStr.Split(';');
            }
            else if (!sectorsStr.Contains(";"))
            {
                sectors = new[] {sectorsStr};
            }
            if(sectors == null)
            {
                Console.WriteLine(@"Error sectors name (empty)!");
                return;
            }
            var listSectors = sectors.Select(_sector => new StructureBloc {Name = _sector}).ToList();
            var structure = new Structure
            {
                Sectors = new ObservableCollection<StructureBloc>(listSectors)
            };
            var fileSystem = new FileSystem
            {
                Name = name,
                Structure = structure
            };
            var result = FileSystemSerializer.Serialize(fileSystem, path);
            Console.WriteLine(result ? @"Serialize success" : @"Error serialize");
        }
    }
}
