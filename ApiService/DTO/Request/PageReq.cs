namespace ApiService.DTO.Request
{
    public record PageReq
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public List<SearchCriteria>? Search { get; set; } = [];
        public List<SortCriteria>? Sort { get; set; } = [];

        public bool IsDefault => Sort?.Count == 0 && Search?.Count == 0 && PageIndex <= 1 && PageSize <= 5;

        public bool IsNoSort => Sort?.Count == 0;

        public bool IsNoSearch => Search?.Count == 0;
    }
}
