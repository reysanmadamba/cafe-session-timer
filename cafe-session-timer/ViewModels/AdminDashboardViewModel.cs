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
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;


namespace cafe_session_timer.ViewModels
{
 
    public partial class AdminDashboardViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private User _selectedUser;
       
        public ObservableCollection<User> Users {get; } = new ObservableCollection<User>();


        public User SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        public int[] TimeOptions { get; } = { 5, 10, 15, 30, 60, 120, 180, 300, 800 };

        
        public IAsyncRelayCommand RegisterCommand { get; }
        public IAsyncRelayCommand AddTimeComand { get; }
        public IAsyncRelayCommand ToggleLockCommand { get; }
        public IAsyncRelayCommand ResetPasswordCommand { get; }
        public IAsyncRelayCommand LoadUsersCommand { get; }
      

        public AdminDashboardViewModel(UserService userService)
    {
            _userService = userService;

            RegisterCommand = new AsyncRelayCommand(RegisterUserAsync);
            AddTimeComand = new AsyncRelayCommand<int>(AddTimeToUserAsync);
            ToggleLockCommand = new AsyncRelayCommand(ToggleUserLockAsync);
            ResetPaswordCommand = new AsyncRelayCommand(ResetUserPasswordAsync);
            LoadUsersCommand = new AsyncRelayCommand(LoadUsersAsync);

            LoadUsersCommand.ExecuteAsync(null);
    }


        //private static string HashPassword(string password)
        //{
        //    using var sha = System.Security.Cryptography.SHA256.Create();
        //    var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        //    var hash = sha.ComputeHash(bytes);
        //    return Convert.ToBase64String(hash);
        //}

    private async Task RegisterUserAsync()
    {
            var dlg = new RegisterUserWindow();
            if (dlg.ShowDialog() != true)
                return;


            var existingUser = await _userService.GetUsernameAsync(dlg.Username);

            if (existingUser != null)
            {
                MessageBox.Show("Username already exists.", "Registration Error");
                return;
            }

            var newUser = new User
            {
                Username = dlg.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dlg.Password),
                TimeRemaining = 0,
                isLocked = false,
                isLoggedIn = false
            };

            await _userService.CreateUserAsync(newUser);
            await LoadUsersAsync();
    }

        private async Task AddTimeToUserAsync(int minutes)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user first", "No user selected");
                return;
            }

            await _userService.AddTimeToUserAsync(SelectedUser.Id, minutes);
            await LoadUsersAsync();
        }

        private async Task ToggleUserLockAsync()
        {
            if (SelectedUser == null) return;

            var newLockStatus = !SelectedUser.isLocked;
            await _userService.ToggleUserLockAsync(SelectedUser.Id, newLockStatus);
            await LoadUsersAsync();
        }

        private async Task ResetUserPasswordAsync()
        {
            if(SelectedUser == null) return;

            var dlg = new ResetPasswordWindow();
            if (dlg.ShowDialog() == true)
            {
                var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(dlg.NewPassword);

                var update = Builders<User>.Update
                    .Set(u => u.PasswordHash, newPasswordHash);
                await _users.UpdateOneAsync(u => u.Id == SelectedUser.Id, update);
                await LoadUsersAsync();
                MessageBox.Show("Password reset successfully", "Success");
            }    
        }

        private async Task LoadUsersAsync()
        {
            Users.Clear();
                var allUsers = await _userService.GetAllUsersAsync();
            foreach (var u in allUsers)
            {
                Users.Add(u);
            }
        }
    }
}
