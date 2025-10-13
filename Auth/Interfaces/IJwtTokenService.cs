using Auth.DTO;
using System.Security.Claims;

namespace Auth.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(UserRequestDto User);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
