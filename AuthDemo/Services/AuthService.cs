using AuthDemo.Data;
using AuthDemo.Models;
using AuthDemo.Services.Interface;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthDemo.Services.Entities;

namespace AuthDemo.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
    {
        public Task<TokenResponseDto> LoginUser(UserDto userDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRegisterDto> RegisterUser(UserDto userDetails)
        {
            if (await context.Users.AnyAsync(u => u.Username == userDetails.Username))
            {
                return null;
            }

            var user = new User();
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, userDetails.Password);

            user.Username = userDetails.Username;
            user.PasswordHash = hashedPassword;

            context.Users.Add(user);
            await context.SaveChangesAsync();


            return new UserRegisterDto
            {
                User = new User
                {
                    Username = user.Username,
                    Email = userDetails.Email
                },
                Tokens = new TokenResponseDto
                {
                    AccessToken = "sample_access_token",
                    RefreshToken = "sample"
                }
            };
        }
    }
}
