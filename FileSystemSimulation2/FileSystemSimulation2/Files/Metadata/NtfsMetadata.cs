using FileSystemSimulation2.NTFS;

namespace FileSystemSimulation2.Files.Metadata
{
    public class NtfsMetadata : AbstractFileMetadata
    {
        private StandardInformation standardInformation;
        private EntryFileName entryFileName;

        public StandardInformation StandardInformation
        {
            get => standardInformation;
            set
            {
                standardInformation = value;
                OnPropertyChanged(nameof(StandardInformation));
            }
        }

        public EntryFileName EntryFileName
        {
            get => entryFileName;
            set
            {
                entryFileName = value;
                OnPropertyChanged(nameof(EntryFileName));
            }
        }
    }
}