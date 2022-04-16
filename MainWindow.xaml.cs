using AutoDisplayRotate.Core;
using AutoDisplayRotate.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


/*
 * 
 * 코드 참조 : https://github.com/JohnnyPP/WPF-Serial-Communication-Advanced/blob/master/Serial%20Communication%20WPF/MainWindow.xaml.cs
 * 
 */

namespace AutoDisplayRotate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ArduinoComuication arduinoComunication;
        
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
            NativeMethods.AllocConsole();
            arduinoComunication = new ArduinoComuication();
        }

        private void btn_scan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("장치 검색");
        }

        private void btn_correction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("...");
        }

        private void btn_connectCheck_Click(object sender, RoutedEventArgs e)
        {
            arduinoComunication.deviceConnect("COM4", serialPort_DataReceived);
            arduinoComunication.connectCheck();
            //MessageBox.Show(DisplayControl.Rotate(2, DisplayControl.Orientations.DEGREES_CW_90).ToString());
            //MessageBox.Show("연결 확인");
        }

        private delegate void DeviceConnectState(string state);

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string receivce = arduinoComunication.SerialPort.ReadLine();
            Dispatcher.Invoke(DispatcherPriority.Normal, new DeviceConnectState(changeStatus), receivce);
        }

        private void changeStatus(string state)
        {
            Console.WriteLine(state);
        }

        static class NativeMethods
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AllocConsole();
        }

    }
}
