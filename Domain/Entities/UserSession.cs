namespace Domain.Entities
{
    public class UserSession
    {
        public long UserId { get; set; }
        public long TenantId { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
