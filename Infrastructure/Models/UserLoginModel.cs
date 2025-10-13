using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("UserLogin")]
    public class UserLoginModel : BaseEntity
    {
        [Required]
        public long UserProfileId { get; set; }
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
