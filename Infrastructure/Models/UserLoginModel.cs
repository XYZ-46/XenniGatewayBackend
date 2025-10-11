using AbstractionBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("UserLogin")]
    public class UserLoginModel : BaseEntity
    {
        public long UserProfileID { get; set; }
        public string PasswordHash { get; private set; } = string.Empty;
        public string PasswordSalt { get; private set; } = string.Empty;
    }
}
