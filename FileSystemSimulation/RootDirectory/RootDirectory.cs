using System.Collections.ObjectModel;
using FileSystemSimulation.Files;

namespace FileSystemSimulation.RootDirectory
{
    public class RootDirectory
    {
        public RootDirectory()
        {
            Files = new ObservableCollection<File>();    
        }

        public ObservableCollection<File> Files { get; set; }
    }
}
