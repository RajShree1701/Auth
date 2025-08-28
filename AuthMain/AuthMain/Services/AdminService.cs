using AuthMain.Model;
using MongoDB.Driver;

namespace AuthMain.Services
{
    public class AdminService
    {
        private readonly IMongoCollection<Admin> _admins;
        public AdminService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("AuthDb");
            _admins = database.GetCollection<Admin>("Admin");
        }

        public async Task<List<Admin>> GetAsync() =>
            await _admins.Find(_ => true).ToListAsync();

        public async Task<Admin?> GetAsync(string id)=>
            await _admins.Find(x=>x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync (Admin admin)=>
            await _admins.InsertOneAsync(admin);

        public async Task UpdateAsync(string id, Admin updateAdmin) =>
            await _admins.ReplaceOneAsync(x => x.Id == id, updateAdmin);

        public async Task DeleteAsync(string id) =>
            await _admins.DeleteOneAsync(x => x.Id == id);
    }
}
