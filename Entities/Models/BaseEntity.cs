using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool IsDeleted { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; } = 0; // User ID 0 for system

        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; } // User ID 0 for system
        public bool IsEmpty() => Id == 0;
    }
}
