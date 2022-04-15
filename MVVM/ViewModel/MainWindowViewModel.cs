using System;
using System.Collections.Generic;
using System.IO.Ports;
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

        private List<ArduinoComuication> devices;


        public MainWindowViewModel()
        {
            deviceList = ArduinoComuication.connectableDeviceList();
            devices = new List<ArduinoComuication>();
        }

        public void searchConnectableDeviceList()
        {
            deviceList = ArduinoComuication.connectableDeviceList();
        }

        public void connectDevice(string portName, SerialDataReceivedEventHandler h)
        {
            ArduinoComuication device = new ArduinoComuication();

            device.deviceConnect(portName, h);

            devices.Add(device);
        }


    }
}
