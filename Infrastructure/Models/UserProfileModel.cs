using AbstractionBase;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("UserProfile")]
    public class UserProfileModel : BaseEntity
    {

        public string NickName { get; private set; } = string.Empty;
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
