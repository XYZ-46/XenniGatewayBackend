namespace Auth.DTO
{
    public record UserRegisterResponse
    {
        public long UserId { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public long CreatedById { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
    }
}
