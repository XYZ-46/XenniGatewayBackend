namespace Domain.Entities
{
    public record UserCreatedDto
    {
        public long UserId { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public long TenantId { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public long CreatedById { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
    }
}
