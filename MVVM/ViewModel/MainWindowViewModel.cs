using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoDisplayRotate.Core;

namespace AutoDisplayRotate.MVVM.ViewModel
{
    internal class MainWindowViewModel : Notifier
    {
        private string[] deviceList;

        public string[] DeviceList
        {
            get { return deviceList; }
            set { 
                deviceList = value;
                OnPropertyChanged("DeviceList");
            }
        }
        public MainWindowViewModel()
        {
            deviceList = ArduinoComuication.connectableDeviceList();
        }

        public void searchConnectableDeviceList()
        {
            deviceList = ArduinoComuication.connectableDeviceList();
        }
    }
}
