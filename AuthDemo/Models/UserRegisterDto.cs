using AuthDemo.Services.Entities;

namespace AuthDemo.Models
{
    public class UserRegisterDto
    {
        public User? User { get; set; }

        public TokenResponseDto? Tokens { get; set; }
    }
}
