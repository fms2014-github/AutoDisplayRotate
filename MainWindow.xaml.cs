using AutoDisplayRotate.Core;
using AutoDisplayRotate.MVVM.Model;
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

            //MessageBox.Show(DisplayControl.Rotate(2, DisplayControl.Orientations.DEGREES_CW_90).ToString());\
            if(listView.SelectedItem != null)
            {
                ListViewItem lvi = (ListViewItem)listView.ItemContainerGenerator.ContainerFromItem(listView.Items.CurrentItem);
                ContentPresenter contentPresenter = FindVisualChild<ContentPresenter>(lvi);

                DataTemplate dataTemplate = contentPresenter.ContentTemplate;
                ComboBox cbo = (ComboBox)dataTemplate.FindName("cbo_serialSelected", contentPresenter);

                string? s = (string)cbo.SelectedValue;

                if (s != null)
                {
                    string serialPortName = s;
                    //Console.WriteLine("123123" + serialPortName);
                    arduinoComunication.deviceConnect(serialPortName, serialPort_DataReceived);
                    arduinoComunication.connectCheck();
                }
                else
                {
                    MessageBox.Show("시리얼 포트를 선택해 주세요.");
                }

            }

        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
                {
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                    {
                        DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                        if (child != null && child is childItem)
                        {
                            return (childItem)child;
                        }
                        else
                        {
                            childItem childOfChild = FindVisualChild<childItem>(child);
                            if (childOfChild != null)
                                return childOfChild;
                        }
                    }
                    return null;
        }

        /// <summary>
        /// Recursively finds the specified named parent in a control hierarchy
        /// </summary>
        /// <typeparam name="T">The type of the targeted Find</typeparam>
        /// <param name="child">The child control to start with</param>
        /// <param name="parentName">The name of the parent to find</param>
        /// <returns></returns>
        private static T FindParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            if (child == null) return null;

            T foundParent = null;
            var currentParent = VisualTreeHelper.GetParent(child);

            do
            {
                var frameworkElement = currentParent as FrameworkElement;
                if (frameworkElement is T)
                {
                    foundParent = (T)currentParent;
                    break;
                }

                currentParent = VisualTreeHelper.GetParent(currentParent);

            } while (currentParent != null);

            return foundParent;
        }

        private delegate void DeviceConnectState(string state);

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string receivce = arduinoComunication.SerialPort.ReadLine();
            Dispatcher.Invoke(DispatcherPriority.Normal, new DeviceConnectState(changeStatus), receivce);
        }

        private void changeStatus(string state)
        {
            char[] stoc = state.ToCharArray();
            Console.WriteLine(stoc);

            if ((byte)(stoc[0]) == 0b00100001)
            {
                Console.WriteLine("OK");

            }
            else
            {
                Console.WriteLine("Fail");

            }
        }

        static class NativeMethods
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AllocConsole();
        }

        private void cbo_serialSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindParent<ListViewItem>(sender as ComboBox).IsSelected = true;
        }

    }
}
