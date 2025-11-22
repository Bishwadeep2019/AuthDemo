using AuthDemo.Data;
using AuthDemo.Models;
using AuthDemo.Services.Entities;
using AuthDemo.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthService(AppDbContext  context, IConfiguration configuration) : IAuthService
{
    public Task<TokenResponseDto> LoginUser(UserDto userDetails)
    {
        throw new NotImplementedException();
    }

    public async Task<UserRegisterDto> RegisterUser(UserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Password))
            throw new ArgumentException("Password cannot be empty");

        if (await context.Users.AnyAsync(u => u.Username == dto.Username))
            throw new InvalidOperationException("User already exists");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email
        };

        var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, dto.Password);

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserRegisterDto
        {
            User = new UserDto
            {
                Username = user.Username,
                Email = user.Email
            },
            Tokens = new TokenResponseDto
            {
                AccessToken = CreateAccessToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            }
        };
    }

    private string CreateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: configuration["AppSettings:Issuer"],
            audience: configuration["AppSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        user.RefreshToken = token;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await context.SaveChangesAsync();
        return token;
    }
}
