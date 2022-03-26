using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections.Specialized;

namespace AutoDisplayRotate.Core
{
    internal class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
