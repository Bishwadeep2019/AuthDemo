using AuthDemo.Models;

namespace AuthDemo.Services.Interface
{
    public interface IAuthService
    {
        public Task<UserRegisterDto> RegisterUser(UserDto userDetails);

        public Task<TokenResponseDto> LoginUser(UserDto userDetails);
    }
}
