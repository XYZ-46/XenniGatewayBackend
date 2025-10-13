using Auth.DTO;
using Auth.Interfaces;
using Auth.Mapper;
using Auth.Security;
using Domain.Entities;
using Domain.Exception;
using Domain.Interfaces;

namespace Auth.Services
{
    public class AuthService(IJwtTokenService jwtTokenService, IUserService userDomainService) : IAuthService
    {
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly IUserService _userService = userDomainService;

        public async Task<LoginResponse> LoginAsync(UserRequestDto userLoginRequest, CancellationToken cancellationToken = default)
        {
            if (!_userService.IsRegisteredAsync(userLoginRequest.Email, cancellationToken).Result) throw new XenniException("Invalid credentials.");

            var passwordHash = await _userService.GetPasswordActiveAsync(userLoginRequest.Email, cancellationToken);
            if (!PasswordHasher.Verify(userLoginRequest.Password, passwordHash ?? "")) throw new XenniException("Invalid credentials.");

            var token = _jwtTokenService.GenerateAccessToken(userLoginRequest);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            return new LoginResponse() { AccessToken = token, RefreshToken = refreshToken };
        }

        public async Task<UserCreatedDto> RegisterAsync(UserRequestDto userRegisterRequest, CancellationToken cancellationToken = default)
        {
            var PasswordHash = PasswordHasher.Hash(userRegisterRequest.Password);
            var userProfile = userRegisterRequest.MapToCreateProfileModel();

            return await _userService.RegisterAsync(userProfile, PasswordHash, cancellationToken);
        }
        public async Task<TokenDto> RefreshToken(UserRequestDto tokenRequest, CancellationToken cancellationToken = default)
        {

            var token = _jwtTokenService.GenerateAccessToken(tokenRequest);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            return new TokenDto() { AccessToken = token, RefreshToken = refreshToken };
        }
    }
}
