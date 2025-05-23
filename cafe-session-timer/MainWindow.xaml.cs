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
using cafe_session_timer.Model;
using cafe_session_timer.Services;

namespace cafe_session_timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer _sessionTimer;
        private TimeSpan _remainingTime;
        private readonly UserService _userService;
        private User _currentUser;
        public MainWindow(User user, UserService userService)
        {
            InitializeComponent();
            _userService = userService;
            _currentUser = user;

            InitializeTimer();
            LoadUserTime();

            StartSession();
        }

        private void InitializeTimer()
        {
            _sessionTimer = new DispatcherTimer();
            _sessionTimer.Interval = TimeSpan.FromSeconds(1);
            _sessionTimer.Tick += SessionTimer_Tick;
        }

        private void LoadUserTime()
        {
            _remainingTime = TimeSpan.FromMinutes(_currentUser.TimeRemaining);
            UpdateTimeDisplay();

            if (_currentUser.TimeRemaining > 0)
            {
                StartBtn.IsEnabled = true;
                PauseBtn.IsEnabled = false;
            }
            else
            {
                StartBtn.IsEnabled = false;
                PauseBtn.IsEnabled = false;
                MessageBox.Show("Your account has no remaining time. Please contact the admin to add time", "Out of Time");
            }
        }

        private void SessionTimer_Tick(object sender,EventArgs e)
        {
            if (_remainingTime <= TimeSpan.Zero)
            {
                EndSession();
                return;
            }

            _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
            UpdateTimeDisplay();

            if (_remainingTime.Seconds == 0)
            {
                _currentUser.TimeRemaining = (int)_remainingTime.TotalMinutes;
                _userService.UpdateUserAsync(_currentUser);
            }
        }

        private void UpdateTimeDisplay()
        {
            TimerText.Text = _remainingTime.ToString(@"hh\:mm\:ss");
        }

        private void StartSession()
        {
            if (_currentUser.TimeRemaining <= 0)
                return;
            _sessionTimer.Start();
            StartBtn.IsEnabled = false;
            PauseBtn.IsEnabled = false;

            _currentUser.isLoggedIn = true;
            _userService.UpdateUserAsync(_currentUser);
        }

        private void PauseSession()
        {
            _sessionTimer.Stop();
            StartBtn.IsEnabled = true;
            PauseBtn.IsEnabled = false;
        }

        private void EndSession()
        {
            _sessionTimer.Stop();
            _currentUser.TimeRemaining = 0;
            _currentUser.isLoggedIn = false;
            _userService.UpdateUserAsync(_currentUser);

            MessageBox.Show("Your session has ened. The system will now lock", "Session Ended");

            Application.Current.Shutdown();
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e) => StartSession();
        private void PauseBtn_Click(object sender, RoutedEventArgs e) => PauseSession();
        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            _sessionTimer.Stop();
            _currentUser.isLoggedIn = false;
            _userService.UpdateUserAsync(_currentUser);

            var app = Application.Current;
            app.Shutdown();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_currentUser != null)
            {
                _currentUser.isLoggedIn = false;
                _userService.UpdateUserAsync(_currentUser);
            }


            base.OnClosed(e);
        }



    }
}