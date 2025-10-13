using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{

    [Table("Token")]
    public class TokenModel : BaseActiveEntity
    {
        public long UserProfileId { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpireDate { get; set; }
    }
}
