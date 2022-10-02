using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

namespace Auto_click_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent ();
        }
        [DllImport ("Auto_click.dll")]
        static extern bool CheckNum (string text, int mode);
        [DllImport ("Auto_click.dll")]
        static extern bool Click_Time (double time, double gap, double stay, int mode);
        [DllImport ("Auto_click.dll")]
        static extern bool Click_Times (int times, double gap, double stay, int mode);
        private void Run_Click (object sender, RoutedEventArgs e)
        {
            if (gap_data.Text == "" || stay_data.Text == "" || times_mode.IsChecked == true && times_data.Text == "" || time_mode.IsChecked == true && time_data.Text == "")
            {
                MessageBox.Show ("输入不能为空!");
                return;
            }
            if (CheckNum (gap_data.Text, 2) == false || CheckNum (stay_data.Text, 2) == false || times_mode.IsChecked == true && CheckNum (times_data.Text, 1) == false || time_mode.IsChecked == true && CheckNum (time_data.Text, 2) == false)
            {
                MessageBox.Show ("数字不合法!");
                return;
            }
            int mode = 1;
            if (right_button.IsChecked == true) mode = 2;
            if (times_mode.IsChecked == true) Click_Times (int.Parse (times_data.Text), double.Parse (gap_data.Text), double.Parse (stay_data.Text), mode);
            if (time_mode.IsChecked == true) Click_Time (double.Parse (time_data.Text), double.Parse (gap_data.Text), double.Parse (stay_data.Text), mode);
        }

        private void times_mode_Checked (object sender, RoutedEventArgs e)
        {
            times_data.IsEnabled = true;
            time_data.IsEnabled = false;
        }

        private void time_mode_Checked (object sender, RoutedEventArgs e)
        {
            time_data.IsEnabled = true;
            times_data.IsEnabled = false;
        }
    }
}
