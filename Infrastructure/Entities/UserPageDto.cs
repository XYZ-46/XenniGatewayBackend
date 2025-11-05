using Infrastructure.Models;

namespace Infrastructure.Entities
{
    public class UserPageDto : IdProperty
    {
        public long UserId { get; set; }
        public string? NickName { get; set; } = string.Empty;
        public string? Fullname { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public long? TenantId { get; set; }
        public string? TenantName { get; set; } = string.Empty;
        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public string? CreatedName { get; set; } = string.Empty;

        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedName { get; set; } = string.Empty;
        public long? UpdatedBy { get; set; }
    }
}
