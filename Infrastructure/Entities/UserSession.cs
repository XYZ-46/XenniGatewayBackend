namespace Infrastructure.Entities
{
    public class UserSession
    {
        public long UserId { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long TenantId { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
