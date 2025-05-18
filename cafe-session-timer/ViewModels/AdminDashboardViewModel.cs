using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cafe_session_timer.Model;
using cafe_session_timer.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;


namespace cafe_session_timer.ViewModels
{
 
    internal class AdminDashboardViewModel
    {
        public const int InitialTime = 25;
       
        public ObservableCollection<User> Users {get; } = new ObservableCollection<User>();

        public IAsyncRelayCommand RegisterCommand { get; }
        //public ICommand RegisterCommand { get; }
        //public ICommand AddTimeCommand { get; }
        //public ICommand ToggleLockCommand { get; }
        //public ICommand ResetPasswordCommand { get; }

    public AdminDashboardViewModel()
    {
        RegisterCommand = new AsyncRelayCommand(async _ => await RegisterUserAsync());

        _ = LoadUsersAsync();
    }

        private static string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    private async Task RegisterUserAsync()
    {
            var dlg = new RegisterUserWindow();
            if (dlg.ShowDialog() != true)
                return;

            var newUser = new User
            {
                Username = dlg.Username,
                PasswordHash = HashPassword(dlg.Password),
                TimeRemaining = InitialTime,
                isLocked = false
            };

            await App.UserService.CreateUserAsync(newUser);
            await LoadUsersAsync();
    }

        private async Task LoadUsersAsync()
        {
            Users.Clear();
                var allUsers = await App.UserService.getAllUserAsync();
            foreach (var u in allUsers)
            {
                Users.Add(u);
            }
        }
    }
}
