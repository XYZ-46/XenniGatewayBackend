using Auth.DTO;
using Auth.Interfaces;
using Auth.Mapper;
using Auth.Security;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Mapper;

namespace Auth.Services
{
    public class AuthService(IJwtTokenService jwtTokenService, IUserDomainService userDomainService) : IAuthService
    {
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly IUserDomainService _userDomainService = userDomainService;
        //private readonly IUserDomainService _userDomainService = userDomainService;

        public async Task<LoginResponse> LoginAsync(UserDto userLoginRequest, CancellationToken cancellationToken = default)
        {
            //var user = await _userProfileRepo.GetByEmailAsync(request.Email) ?? throw new XenniException("Invalid credentials.");

            //if (!PasswordHasher.Verify(request.Password, user.PasswordHash))
            //    throw new UnauthorizedAccessException("Invalid credentials.");

            //var token = _tokenGen.GenerateToken(user);

            //return new AuthResponse
            //{
            //    Token = token,
            //    ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            //};

            return new LoginResponse();
        }

        public async Task<UserCreatedDto> RegisterAsync(UserDto userRegisterRequest, CancellationToken cancellationToken = default)
        {

            var PasswordHash = PasswordHasher.Hash(userRegisterRequest.Password);
            var userProfile = userRegisterRequest.MapToCreateProfileModel();

            var userCreated = await _userDomainService.RegisterAsync(userProfile, PasswordHash, cancellationToken);

            return userCreated;
        }
    }
}
