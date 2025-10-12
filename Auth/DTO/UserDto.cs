namespace Auth.DTO
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public long TenantId { get; set; }
        public string Password { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string TenantName { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long CreatedById { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
