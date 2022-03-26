using System;
using System.Collections.Generic;
using System.Linq;
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

namespace AutoDisplayRotate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            MessageBox.Show("연결 확인");
        }
    }
}
