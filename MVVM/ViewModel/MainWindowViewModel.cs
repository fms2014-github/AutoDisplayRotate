using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;
using AutoDisplayRotate.Core;
using AutoDisplayRotate.MVVM.Model;

namespace AutoDisplayRotate.MVVM.ViewModel
{
    internal class MainWindowViewModel : Notifier
    {
        private List<DeviceList> deviceList = new List<DeviceList>();
        private string[] gyroList;
        private string[] displayList;
        
        public string[] DeviceList
        {
            get { return DeviceList; }
            set { 
                gyroList = value;
                OnPropertyChanged("DeviceList");
            }
        }

        private List<ArduinoComuication> gyro;


        public MainWindowViewModel()
        {
            gyroList = ArduinoComuication.connectableGyroList();
            displayList = DisplayControl.displayList();
            gyro = new List<ArduinoComuication>();

            deviceList = new List<DeviceList>();

            for(int i = 0; i < displayList.Length; i++)
            {
                deviceList.Add(new DeviceList(displayList[i], gyroList, false));
            }
        }

        public void searchConnectableDeviceList()
        {
            gyroList = ArduinoComuication.connectableGyroList();
        }

        public void connectDevice(string portName, SerialDataReceivedEventHandler h)
        {
            ArduinoComuication arduino = new ArduinoComuication();

            arduino.deviceConnect(portName, h);

            gyro.Add(arduino);
        }

        public List<string> getDisplayList()
        {
            List<string> list = new List<string>();

            foreach (Screen screen in Screen.AllScreens)
            {
                list.Add(ConnectedDisplayList.GetFriendlyDeviceName(screen));
            }

            return list;
        }


    }
}
