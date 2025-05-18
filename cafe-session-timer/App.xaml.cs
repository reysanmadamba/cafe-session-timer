using System.Configuration;
using System.Data;
using System.Windows;
using cafe_session_timer.Services;

namespace cafe_session_timer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserService UserService { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var connString = ConfigurationManager.AppSettings["MongoConn"];
            var dbName = ConfigurationManager.AppSettings["MongoDbName"];


            UserService = new UserService(connString, dbName);

            var login = new LoginWindow();
            if (login.ShowDialog() != true)
            {
                Shutdown();
                return;
            }

            var main = new MainWindow();
            main.Show();
        }
    }

}
