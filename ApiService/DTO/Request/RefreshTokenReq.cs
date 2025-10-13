using System.ComponentModel.DataAnnotations;

namespace ApiService.DTO.Request
{
    public record RefreshTokenReq
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}