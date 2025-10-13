using Auth.DTO;
using Infrastructure.Models;

namespace Auth.Mapper
{
    public static class UserMapper
    {
        public static UserProfileModel MapToCreateProfileModel(this UserRequestDto registerRequest) => new()
        {
            Email = registerRequest.Email,
            TenantId = registerRequest.TenantId,
            FullName = registerRequest.FullName,
            NickName = registerRequest.NickName

        };
    }
}
