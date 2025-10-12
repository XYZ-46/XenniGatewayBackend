using ApiService.DTO.Request;
using ApiService.DTO.Response;
using ApiService.Mapper;
using Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace ApiService.Controllers
{
    public class AuthController(IJwtTokenService jwtService, IAuthService authService) : BaseApiController
    {
        private static readonly ConcurrentDictionary<string, string> _refreshTokens = new();
        private readonly IJwtTokenService _jwtService = jwtService;
        private readonly IAuthService _authService = authService;

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginReq)
        {
            // TODO: Validate credentials from DB
            //if (loginReq.Username != "admin" || loginReq.Password != "123456")
            //    return Unauthorized("Invalid username or password");

            //UserJwt userLoginJwt = new();
            //var accessToken = _jwtService.GenerateAccessToken(userLoginJwt);
            //var refreshToken = _jwtService.GenerateRefreshToken();

            //_refreshTokens[refreshToken] = loginReq.Username;

            //return new JsonResult(ApiResponseDefault<object>.Success(new { AccessToken = accessToken, RefreshToken = refreshToken }, "Login success"));
            return Ok(new { Message = "User registered successfully" });

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var user = registerRequest.MapToUserDto();
            var registered = await _authService.RegisterAsync(user);

            return new JsonResult(ApiResponseDefault<object>.Success(registered, "User created successfully"));
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(RefreshTokenRequest request)
        {
            //var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken) ?? throw new XenniException("Invalid access token");

            //var username = principal.Identity!.Name ?? principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if (username == null || !_refreshTokens.TryGetValue(request.RefreshToken, out var storedUser) || storedUser != username)
            //    return Unauthorized("Invalid refresh token");

            //UserJwt userRefreshJwt = new();
            //var newAccessToken = _jwtService.GenerateAccessToken(userRefreshJwt);
            //var newRefreshToken = _jwtService.GenerateRefreshToken();

            //return new JsonResult(ApiResponseDefault<object>.Success(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken }, "Login success"));
            return Ok();
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(RegisterRequest request)
        {
            // TODO: Save user in DB with hashed password
            return Ok(new { Message = "User registered successfully" });
        }
    }
}
