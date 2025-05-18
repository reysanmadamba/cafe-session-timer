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

namespace cafe_session_timer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

           
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = UsernameBox.Text;
            var pass = PasswordBox.Password;

            if (AuthService.ValidateAdmin(user, pass))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                ErrorText.Visibility = Visibility.Visible;
            }
        }
    }
}
