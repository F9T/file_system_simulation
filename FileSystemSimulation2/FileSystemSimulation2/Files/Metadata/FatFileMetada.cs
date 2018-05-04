using System;

namespace FileSystemSimulation2.Files.Metadata
{
    public class FatFileMetada : AbstractFileMetadata
    {
        private string extension;
        private EnumFileAttribut attribut;
        private byte fileCreationTime;
        private DateTime createdDate, accessDate, writeDate, createdTime, writeTime;
        private string fileName;
        private int fileSize;

        public string Extension
        {
            get => extension;
            set
            {
                extension = value;
                OnPropertyChanged(nameof(Extension));
            }
        }

        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public int FileSize
        {
            get => fileSize;
            set
            {
                fileSize = value;
                OnPropertyChanged(nameof(FileSize));
            }
        }

        public EnumFileAttribut Attribut
        {
            get => attribut;
            set
            {
                attribut = value;
                OnPropertyChanged(nameof(Attribut));
            }
        }

        public byte FileCreationTime
        {
            get => fileCreationTime;
            set
            {
                fileCreationTime = value;
                OnPropertyChanged(nameof(FileCreationTime));
            }
        }

        public DateTime CreatedTime
        {
            get => createdTime;
            set
            {
                createdTime = value;
                OnPropertyChanged(nameof(CreatedTime));
            }
        }

        public DateTime CreatedDate
        {
            get => createdDate;
            set
            {
                createdDate = value;
                OnPropertyChanged(nameof(CreatedDate));
            }
        }

        public DateTime AccessDate
        {
            get => accessDate;
            set
            {
                accessDate = value;
                OnPropertyChanged(nameof(AccessDate));
            }
        }

        public DateTime WriteTime
        {
            get => writeTime;
            set
            {
                writeTime = value;
                OnPropertyChanged(nameof(WriteTime));
            }
        }

        public DateTime WriteDate
        {
            get => writeDate;
            set
            {
                writeDate = value;
                OnPropertyChanged(nameof(WriteDate));
            }
        }

        public byte Reserved { get; set; }
    }
}
