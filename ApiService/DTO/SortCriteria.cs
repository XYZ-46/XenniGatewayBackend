namespace ApiService.DTO
{
    public record SortCriteria
    {
        public const string ORDER_BY_DESCENDING = "desc";

        public string? Field { get; set; } = string.Empty;
        public string? Direction { get; set; } = string.Empty;

        public bool IsDescending => Direction?.ToLower() == ORDER_BY_DESCENDING;
    }
}