using AuthDemo.Models;
using AuthDemo.Services.Interface;

using Microsoft.AspNetCore.Mvc;

using AuthDemo.Services.Entities;

namespace AuthDemo.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthenticateController(IAuthService authService) : Controller
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDto>> Register(UserDto userDetails)
        {
            // Registration logic here
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto userDetails)
        {
            // Login logic here
            return Ok("User registered successfully.");
        }
    }
}
