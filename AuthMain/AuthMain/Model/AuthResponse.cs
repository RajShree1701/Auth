namespace AuthMain.Model
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
        public string UserRole { get; internal set; }
    }
}
