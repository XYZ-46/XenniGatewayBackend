namespace Application.ResponseDTO
{
    public record TenantRes
    {
        public long Id { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
