using System.Configuration;
using System.Data;
using System.Windows;
using cafe_session_timer.Services;
using cafe_session_timer.Model;
using cafe_session_timer.ViewModels;

namespace cafe_session_timer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserService UserService { get; private set; }
        internal static AuthService AuthService { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var connectionString = ConfigurationManager.AppSettings["MongoConn"];
            var databasebName = ConfigurationManager.AppSettings["MongoDbName"];


            UserService = new UserService(connectionString, databasebName);
            AuthService = new AuthService(UserService);

            var loginWindow = new LoginWindow(AuthService);
            if (loginWindow.ShowDialog() == true)
            {
              if (loginWindow.IsAdminLogIn)
                {
                    var adminVM = new AdminDashboardViewModel(UserService);
                    var adminWindow = new AdminDashboardWindow(adminVM);

                    adminWindow.Show();
                }
              else
                {
                    var mainWindow = new MainWindow(loginWindow.LoggedInUser, UserService);
                    mainWindow.Show();
                }
            }
            else
            {
                Shutdown();
            }

        }
    }

}
