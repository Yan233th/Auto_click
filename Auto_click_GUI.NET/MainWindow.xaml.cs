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
using static System.Net.Mime.MediaTypeNames;

namespace Auto_click_GUI.NET
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

        private delegate void DllCallBack (int status);

        [DllImport ("Auto_click.dll")]
        static extern bool CheckNum (string text, int mode);
        [DllImport ("Auto_click.dll")]
        static extern bool Click_Time (double time, double gap, double stay, int mode, DllCallBack CallBack);
        [DllImport ("Auto_click.dll")]
        static extern bool Click_Times (int times, double gap, double stay, int mode, DllCallBack CallBack);
        [DllImport ("Auto_click.dll")]
        static extern bool Hold_Time (double time, double stay, int mode, DllCallBack CallBack);

        private void ExecutionCallBack (int status)
        {
            if (status != 0) MessageBox.Show ("出错了...");
            Run.Dispatcher.Invoke (new Action (delegate {Run.IsEnabled = true;}));
        }

        private void Run_Click (object sender, RoutedEventArgs e)
        {
            Run.IsEnabled = false;
            int mode = 1;
            if (right_button.IsChecked == true) mode = 2;
            string times_data_Text = times_data.Text;
            string time_data_Text = time_data.Text;
            string gap_data_Text = gap_data.Text;
            string stay_data_Text = stay_data.Text;
            if (hold_mode.IsChecked == true)
            {
                if (time_data_Text == "" || stay_data_Text == "")
                {
                    MessageBox.Show ("输入不能为空!");
                    return;
                }
                if (CheckNum (time_data_Text, 2) == false || CheckNum (stay_data_Text, 2) == false)
                {
                    MessageBox.Show ("数字不合法!");
                    return;
                }
                double time = double.Parse (time_data_Text);
                double stay = double.Parse (stay_data_Text);
                Thread execute = new Thread (() => Hold_Time (time, stay, mode, ExecutionCallBack));
                execute.Start ();
            }
            else
            {
                if (gap_data_Text == "" || stay_data_Text == "" || times_mode.IsChecked == true && times_data_Text == "" || time_mode.IsChecked == true && time_data_Text == "")
                {
                    MessageBox.Show ("输入不能为空!");
                    return;
                }
                if (CheckNum (gap_data_Text, 2) == false || CheckNum (stay_data_Text, 2) == false || times_mode.IsChecked == true && CheckNum (times_data_Text, 1) == false || time_mode.IsChecked == true && CheckNum (time_data_Text, 2) == false)
                {
                    MessageBox.Show ("数字不合法!");
                    return;
                }
                if (times_mode.IsChecked == true)
                {
                    int times = int.Parse (times_data_Text);
                    double gap = double.Parse (gap_data_Text);
                    double stay = double.Parse (stay_data_Text);
                    Thread execute = new Thread (() => Click_Times (times, gap, stay, mode, ExecutionCallBack));
                    execute.Start ();
                }
                if (time_mode.IsChecked == true)
                {
                    double time = double.Parse (time_data_Text);
                    double gap = double.Parse (gap_data_Text);
                    double stay = double.Parse (stay_data_Text);
                    Thread execute = new Thread (() => Click_Time (time, gap, stay, mode, ExecutionCallBack));
                    execute.Start ();
                }
            }
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

        private void hold_mode_Checked (object sender, RoutedEventArgs e)
        {
            time_mode.IsChecked = true;
            times_mode.IsEnabled = false;
            gap_data.IsEnabled = false;
        }

        private void hold_mode_Unchecked (object sender, RoutedEventArgs e)
        {
            times_mode.IsEnabled = true;
            gap_data.IsEnabled = true;
        }
    }
}