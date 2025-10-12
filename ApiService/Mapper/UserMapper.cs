﻿using ApiService.DTO.Request;
using Auth.DTO;

namespace ApiService.Mapper
{
    public static class UserMapper
    {
        public static UserDto MapToUserDto(this RegisterRequest registerRequest) => new()
        {
            Email = registerRequest.Email,
            TenantId = registerRequest.TenantId,
            FullName = registerRequest.FullName,
            NickName = registerRequest.NickName,
            Password = registerRequest.Password
        };
    }
}
