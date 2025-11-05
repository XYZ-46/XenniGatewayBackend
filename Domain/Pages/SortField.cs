using System.Linq.Expressions;

namespace Domain.Pages
{
    public class SortField<T>
    {
        public Expression<Func<T, object>> Field { get; set; } = default!;
        public bool Descending { get; set; } = false;
    }

}
