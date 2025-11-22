namespace AuthDemo.Models
{
    public class UserDto
    {
        public string Username { get; set; } = String.Empty;
        public required string Password { get; set; }
        public string Email { get; set; } = String.Empty;
    }
}
