using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using cafe_session_timer.Model;

namespace cafe_session_timer.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(string connectionString, string databaseName)
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

        public async Task UpdateUserAsync(User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

        public async Task DeleteUserAsync(string id)
            {
                await _users.DeleteOneAsync(u => u.Id == id);
            }

        public async Task AddTimeToUserAsync(string userId, int minutesToAdd)
        {
            var update = Builders<User>.Update
                .Inc(u => u.TimeRemaining, minutesToAdd);
            await _users.UpdateOneAsync(u => u.Id == userId, update);
        }

        public async Task UpdateLoginStatusAsync(string userId, bool isLoggedIn)
        {
            var update = Builders<User>.Update
                .Set(u => u.isLoggedIn, isLoggedIn);
            await _users.UpdateOneAsync(u => u.Id == userId, update);
        }

        public async Task ToggleUserLockAsync(string userId, bool isLocked)
        {
            var update = Builders<User>.Update
                .Set(u => isLocked, isLocked);
            await _users.UpdateOneAsync(u => u.Id == userId, update);
        }
        }
    }

