using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthMain.Model
{
    public class Admin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? Id { get; set; }

        public string AdminName { get; set; } = null!;

        public string? Email { get; set; }

        public string ? Password { get; set; }

        public string Role { get; set; } = "Admin";

        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }
    }
}
