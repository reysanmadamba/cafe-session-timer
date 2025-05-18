using System.Text;
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

namespace cafe_session_timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer _timer;
        private TimeSpan _time;
        public MainWindow()
        {
            InitializeComponent();

            StartBtn.Visibility = Visibility.Collapsed;
            PauseBtn.Visibility = Visibility.Collapsed;     
            ResetBtn.Visibility = Visibility.Collapsed;


            _time = TimeSpan.FromMinutes(25);
            TimerText.Text = _time.ToString(@"mm\:ss");

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_time == TimeSpan.Zero)
            {
                _timer.Stop();
                MessageBox.Show("Time's up!", "Focus Timer");
                ResetControls();
            }
            else
            {
                _time = _time.Add(TimeSpan.FromSeconds(-1));
                TimerText.Text = _time.ToString(@"mm\:ss");
            }
        }

        private void ChangeDuration_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SetDurationWindow((int)_time.TotalMinutes)
            {
                Owner = this
            };
            if (dlg.ShowDialog() == true)
            {
                _time = TimeSpan.FromMinutes(dlg.Minutes);
                    TimerText.Text = _time.ToString(@"mm\:ss");

                StartBtn.Visibility = Visibility.Visible;
                PauseBtn.Visibility = Visibility.Visible;
                ResetBtn.Visibility = Visibility.Visible;
            }
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
            StartBtn.IsEnabled = false;
            PauseBtn.IsEnabled = true;
            ResetBtn.IsEnabled = true;
        }

        private void PauseBtn_Click(Object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            StartBtn.IsEnabled = true;
            PauseBtn.IsEnabled = false;
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _time = TimeSpan.FromMinutes(25);
            TimerText.Text = _time.ToString(@"mm\:ss");
            ResetControls();
        }

        private void ResetControls()
        {
            StartBtn.IsEnabled = true;
            PauseBtn.IsEnabled = false;
            ResetBtn.IsEnabled = false;
        }



    }
}