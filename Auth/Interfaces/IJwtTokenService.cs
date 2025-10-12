using System.Security.Claims;

namespace Auth.Interfaces
{
    public interface IJwtTokenService
    {
        //string GenerateAccessToken(UserJwt User);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
