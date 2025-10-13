namespace Infrastructure.Models
{
    public abstract class BaseActiveEntity : BaseEntity
    {
        public bool IsActive { get; set; } = true;
    }
}
