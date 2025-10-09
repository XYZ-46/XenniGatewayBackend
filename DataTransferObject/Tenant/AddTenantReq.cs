using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.Tenant
{
    public record AddTenantReq
    {
        [Required]
        //[MinLength(10, ErrorMessage = "The {0} field must have a minimum length of {1} characters.")]
        //[MaxLength(1, ErrorMessage = "The {0} field must have a minimum length of {1} characters.")]
        public string TenantName { get; set; } = string.Empty;
    }
}
