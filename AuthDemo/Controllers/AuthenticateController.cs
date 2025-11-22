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

            return Ok(new { message = "User Registered Successfully"});
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto userDetails)
        {
            // Login logic here
            return Ok("User registered successfully.");
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
    }
}
