using AuthMain.Model;
using MongoDB.Driver;

namespace AuthMain.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IConfiguration _config;

        public UserService(IConfiguration config)
        {
            _config = config;
           
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("AuthDb");
            _users = database.GetCollection<User>("Users");
        }

        public async Task CreateAsync(User user) => await _users.InsertOneAsync(user);
        public async  Task<User?> GetByEmailAsync(string email) =>
            await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

        //public async Task<User?> GetByIdAsync(string id) =>
        //    await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, User user) =>
            await _users.ReplaceOneAsync(u => u.Id == id, user);

        
    }
}
