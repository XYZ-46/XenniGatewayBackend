namespace Domain.Pages
{
    public class SearchFilter
    {
        public string Field { get; set; } = string.Empty;
        public FilterOperator Operator { get; set; } = FilterOperator.Equal;
        public string Value { get; set; } = string.Empty;
        public string? Value2 { get; set; } // For Between operator
    }
}
