using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace ProcessViewer
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            UpdateProcessList();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateProcessList();
        }
        private void UpdateProcessList()
        {
            var processes = Process.GetProcesses().Select(p => p.ProcessName).OrderBy(name => name).ToList();
            ProcessListBox.ItemsSource = processes;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IntervalTextBox.Text, out int interval) && interval > 0)
            {
                _timer.Interval = TimeSpan.FromMilliseconds(interval);
            }
            else
            {
                MessageBox.Show("Пожалуйста укажите позитивное число для интервала");
            }
        }
    }
}
