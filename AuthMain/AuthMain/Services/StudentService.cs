using AuthMain.Model;
using MongoDB.Driver;

namespace AuthMain.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _student;
        public StudentService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("AuthDb");
            _student = database.GetCollection<Student>("Student");
        }

        public async Task<List<Student>> GetAsync() =>
            await _student.Find(_ => true).ToListAsync();

        public async Task<Student?> GetAsync(string id) =>
            await _student.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Student student) =>
            await _student.InsertOneAsync(student);

        public async Task UpdateAsync(string id, Student updateStudent) =>
            await _student.ReplaceOneAsync(x => x.Id == id, updateStudent);

        public async Task DeleteAsync(string id) =>
            await _student.DeleteOneAsync(x => x.Id == id);
    }
}
