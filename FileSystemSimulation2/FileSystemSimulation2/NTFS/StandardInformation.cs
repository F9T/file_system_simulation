using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileSystemSimulation2.NTFS
{
    public class StandardInformation : INotifyPropertyChanged
    {
        private DateTime createdDate, accessDate, createdTime;
        private DateTime dateModified;
        private DateTime dateMftRecordModified;

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

        public DateTime DateModified
        {
            get => dateModified;
            set
            {
                dateModified = value;
                OnPropertyChanged(nameof(DateModified));
            }
        }

        public DateTime DateMftRecordModified
        {
            get => dateMftRecordModified;
            set
            {
                dateMftRecordModified = value;
                OnPropertyChanged(nameof(DateMftRecordModified));
            }   
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}
