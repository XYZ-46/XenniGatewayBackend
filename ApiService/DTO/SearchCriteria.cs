namespace ApiService.DTO
{
    public record SearchCriteria
    {
        public string? Field { get; set; } = string.Empty;
        
        public string? Value { get; set; } = string.Empty;

        public string? StartValue { get; set; } = string.Empty;
        public string? EndValue { get; set; } = string.Empty;

        public string? Operator { get; set; } = string.Empty;
    }
}
