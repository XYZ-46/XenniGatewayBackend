using Auth.DTO;
using Domain.Entities;

namespace Auth.Interfaces
{
    public interface IAuthService 
    {
        Task<LoginResponse> LoginAsync(UserRequestDto userLoginRequest, CancellationToken cancellationToken = default);
        Task<UserCreatedDto> RegisterAsync(UserRequestDto userRegisterRequest, CancellationToken cancellationToken = default);
        Task<TokenDto> RefreshToken(UserRequestDto tokenRequest, CancellationToken cancellationToken = default);
    }
}
