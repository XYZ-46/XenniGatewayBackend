namespace ApiService.DTO.Response
{
    public class ApiResponseDefault<T>
    {
        public string? Message { get; set; } 
        public T? Data { get; set; }
        public Dictionary<string, string[]>? ErrorDetails { get; set; }

        public static ApiResponseDefault<T> Success(T data, string message) => new() { Message = message, Data = data };
        public static ApiResponseDefault<T> Fail(string message, Dictionary<string, string[]>? errors = null) => new() { Message = message, ErrorDetails = errors };

        // No Message as notification
        public static ApiResponseDefault<T> Found(T data) => new() { Data = data };
        // Just Message as notification
        public static ApiResponseDefault<T> NotFound(string message) => new() { Message = message };
    }
}
