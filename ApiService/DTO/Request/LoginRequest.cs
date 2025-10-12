using System.ComponentModel.DataAnnotations;

namespace ApiService.DTO.Request
{
    public record LoginRequest
    {
        // email as username
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
