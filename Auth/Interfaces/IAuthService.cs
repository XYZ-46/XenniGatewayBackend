using Auth.DTO;
using Domain.Entities;

namespace Auth.Interfaces
{
    public interface IAuthService 
    {
        Task<LoginResponse> LoginAsync(UserDto userLoginRequest, CancellationToken cancellationToken = default);
        Task<UserCreatedDto> RegisterAsync(UserDto userRegisterRequest, CancellationToken cancellationToken = default);
    }
}
