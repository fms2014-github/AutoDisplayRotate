using System;
using System.Collections.Generic;
using System.IO.Ports;
using AutoDisplayRotate.Core;
using AutoDisplayRotate.MVVM.Model;

namespace AutoDisplayRotate.MVVM.ViewModel
{
    internal class MainWindowViewModel : Notifier
    {
        private DisplayControl displayControl = new DisplayControl();
        private List<DeviceList> device;
        //private string[]? gyroList;
        private string[]? displayList;
        
        public List<DeviceList> Device
        {
            get { return device; }
            set {
                device = value;
                OnPropertyChanged("Device");
            }
        }

        private List<ArduinoComuication> gyro;


        public MainWindowViewModel()
        {
            displayList = displayControl.displayList();
            gyro = new List<ArduinoComuication>();
            device = new List<DeviceList>();
           for (int i = 0; i < displayList.Length; i++)
            {
                device.Add(new DeviceList(displayList[i], ArduinoComuication.connectableGyroList(), false));
            }
        }


        public void connectDevice(string portName, SerialDataReceivedEventHandler h)
        {
            ArduinoComuication arduino = new ArduinoComuication();

            arduino.deviceConnect(portName, h);

            gyro.Add(arduino);
        }

    }
}
