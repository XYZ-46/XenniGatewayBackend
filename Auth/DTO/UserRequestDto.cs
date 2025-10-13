namespace Auth.DTO
{
    public record UserRequestDto
    {
        public string UserName { get; set; } = string.Empty;
        public long TenantId { get; set; }
        public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
