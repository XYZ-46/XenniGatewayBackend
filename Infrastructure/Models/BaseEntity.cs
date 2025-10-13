using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool IsDeleted { get; set; } = false;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }
        [Required]
        public long CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsEmpty() => Id == 0;
        public bool IsNotEmpty() => Id > 0;
    }
}
