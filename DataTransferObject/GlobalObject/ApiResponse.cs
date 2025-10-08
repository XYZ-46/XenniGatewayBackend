namespace DataTransferObject.GlobalObject
{
    public class ApiResponse<T>
    {
        public string? Message { get; set; } 
        public T? Data { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }

        public static ApiResponse<T> Success(T data, string message) => new() { Message = message, Data = data };
        public static ApiResponse<T> Fail(string message, Dictionary<string, string[]>? errors = null) => new() { Message = message, Errors = errors };

        // No Message as notification
        public static ApiResponse<T> Found(T data) => new() { Data = data };
        // Just Message as notification
        public static ApiResponse<T> NotFound(string message) => new() { Message = message };
    }
}
