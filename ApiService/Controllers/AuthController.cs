using ApiService.DTO.Request;
using ApiService.DTO.Response;
using ApiService.Mapper;
using Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    public class AuthController(IAuthService authService) : BaseApiController
    {
        private readonly IAuthService _authService = authService;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginReq, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(loginReq.MapToUserDto(), cancellationToken);
            return new JsonResult(ApiResponseDefault<object>.Success(token, "Login success"));
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
