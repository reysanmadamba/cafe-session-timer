using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace cafe_session_timer.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(string connectioString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _users = database.GetCollection<User>("users");
        }

        public async Task<List<User>> getAllUserAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetUserBUsernameAsync(string username)
        {
            return await _users.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(UserService user)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.id, user);

        public  async Task DeleteUserAsync(string id)
            {
                await _users.DeleteOneAsync(u => u.Id == id);
            }
        }
    }
}
