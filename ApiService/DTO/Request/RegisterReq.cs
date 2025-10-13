using System.ComponentModel.DataAnnotations;

namespace ApiService.DTO.Request
{
    public record RegisterReq
    {
        public string FullName { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;

        [Required]
        public long TenantId { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}