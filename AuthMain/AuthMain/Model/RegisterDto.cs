using MongoDB.Bson.Serialization.Attributes;

namespace AuthMain.Model
{
    public class RegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
       public string Role { get; set; }=null!;
    }
}
