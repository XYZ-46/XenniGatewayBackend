using Auth.Config;
using Auth.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Services
{
    public class JwtTokenService(IOptionsSnapshot<JwtSettings> settings) : IJwtTokenService
    {
        private readonly JwtSettings _settings = settings.Value;

        //public string GenerateAccessToken(UserJwt User)
        //{
        //    var claims = new List<Claim>
        //{
        //    new(JwtRegisteredClaimNames.Sid, User.Id.ToString()),
        //    new(JwtRegisteredClaimNames.Name, User.Name),
        //    new(JwtRegisteredClaimNames.Email, User.Email),
        //    new(JwtRegisteredClaimNames.Iss, _settings.Issuer),
        //    new(JwtRegisteredClaimNames.Aud, _settings.Audience),
        //    new("Role", User.RoleDisplay)
        //};

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: _settings.Issuer,
        //        audience: _settings.Audience,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = _settings.ValidateIssuer,
                ValidateAudience = _settings.ValidateAudience,
                ValidateIssuerSigningKey = _settings.ValidateIssuerSigningKey,
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey)),
                ValidateLifetime = false // we ignore expiration here
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
