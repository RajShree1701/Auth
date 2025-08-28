using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AuthMain.Model
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; }=null!;

        public string Password { get; set; } = null!;   

        public string Class { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

    }
}
