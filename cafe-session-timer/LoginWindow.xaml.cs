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
using System.Windows.Shapes;
using cafe_session_timer.Services;
using cafe_session_timer.Model;
using System.Configuration;

namespace cafe_session_timer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private readonly AuthService _authService;

        public User LoggedInUser { get; private set; }
        public bool IsAdminLogIn { get; private set; }

        public LoginWindow( AuthService authService)
        {
            if (authService == null)
            throw new ArgumentNullException(nameof(authService));

            _authService = authService;
            InitializeComponent();
        }

        public LoginWindow()
        {
            var connectionString = ConfigurationManager.AppSettings["MongoConn"];
            var databaseName = ConfigurationManager.AppSettings["MongoDbName"];
            var userService = new UserService(connectionString, databaseName);
            _authService = new AuthService(userService);

            InitializeComponent();
        }
        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;

            ErrorText.Visibility = Visibility.Collapsed;

            if (AuthService.ValidateAdmin(username, password))
            {
                IsAdminLogIn = true;
                DialogResult = true;
                Close();
                return;
            }

            var user = await _authService.ValidateUser(username, password);

            if (user != null)
            {
                LoggedInUser = user;
                IsAdminLogIn = false;
                DialogResult = true;
                Close();
            }
            else
            {
                ErrorText.Text = "Invalid login or account is locked/out of time";
                ErrorText.Visibility = Visibility.Visible;
            }
        }
    }
}
