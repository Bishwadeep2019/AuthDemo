using AuthDemo.Models;
using AuthDemo.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemo.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthenticateController(IAuthService authService) : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto dto)
        {
            var result = await authService.RegisterUser(dto);

            if (result == null)
                return BadRequest("User already exists");

            SetAccessTokenCookie(result.Tokens.AccessToken);
            SetRefreshTokenCookie(result.Tokens.RefreshToken);

            return Ok(new { message = "User Registered Successfully"});
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto userDetails)
        {

            var tokens = await authService.LoginUser(userDetails);

            SetAccessTokenCookie(tokens.AccessToken);
            SetRefreshTokenCookie(tokens.RefreshToken);

            return Ok("User logged in successfully.");
        }

        [HttpGet("protected")]
        [Authorize]
        public IActionResult ProtectedEndpoint()
        {
            return Ok(new
            {
                message = "You accessed a PROTECTED endpoint!",
                username = User.Identity?.Name,
                claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }

        private void SetAccessTokenCookie(string token)
        {
            Response.Cookies.Append("accessToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });
        }

        private void SetRefreshTokenCookie(string token)
        {
            Response.Cookies.Append("refreshToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }
    }
}
