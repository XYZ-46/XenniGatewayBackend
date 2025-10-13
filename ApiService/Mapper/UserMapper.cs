using ApiService.DTO.Request;
using Auth.DTO;

namespace ApiService.Mapper
{
    public static class UserMapper
    {
        public static UserRequestDto MapToUserDto(this LoginReq registerRequest) => new()
        {
            UserName = registerRequest.Username,
            Email = registerRequest.Username,
            Password = registerRequest.Password
        };

        public static UserRequestDto MapToUserDto(this RefreshTokenReq refToken) => new()
        {
            AccessToken = refToken.AccessToken,
            RefreshToken = refToken.RefreshToken
        };

        public static UserRequestDto MapToUserDto(this RegisterReq registerRequest) => new()
        {
            Email = registerRequest.Email,
            TenantId = registerRequest.TenantId,
            FullName = registerRequest.FullName,
            NickName = registerRequest.NickName,
            Password = registerRequest.Password
        };
    }
}
