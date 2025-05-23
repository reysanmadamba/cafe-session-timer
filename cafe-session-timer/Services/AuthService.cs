using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;
using cafe_session_timer.Model;
using cafe_session_timer.Services;

namespace cafe_session_timer.Services
{
    public class AuthService
    {
        private static readonly string AdminUser = "admin";
        private static readonly string AdminPassHash = BCrypt.Net.BCrypt.HashPassword("Passw0rd");


        private readonly UserService _userService;

        public AuthService(UserService userService)
        {
            _userService = userService; 
        }


        public static bool ValidateAdmin(string username, string password)
        {
            return username == AdminUser
                && BCrypt.Net.BCrypt.Verify(password, AdminPassHash);
        }
        
        public async Task<User> ValidateUser (string username, string password)
        {
            var user = await _userService.GetUserBUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            if (user.isLocked || user.TimeRemaining <= 0)
                return null;

            return user;
        }
       
    }
}
