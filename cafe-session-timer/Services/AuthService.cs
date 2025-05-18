using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;

namespace cafe_session_timer.Services
{
    internal class AuthService
    {
        private static readonly string AdminUser = "admin";
        private static readonly string AdminPassHash = BCrypt.Net.BCrypt.HashPassword("P@ssw0rd");


        public static bool ValidateAdmin(string username, string password)
        {
            return username == AdminUser
                && BCrypt.Net.BCrypt.Verify(password, AdminPassHash);
        }
       
    }
}
