namespace ApiService.GlobalException
{
    public class XenniException(string message, int statusCode = 400) : Exception(message)
    {
        public int StatusCode { get; } = statusCode;
        public string Details { get; set; } = string.Empty;

    }
}
