using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseAbstraction
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public bool IsDeleted { get; set; } = false;
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; } 
        public string CreatedBy { get; set; } = "SYSTEM";

        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
