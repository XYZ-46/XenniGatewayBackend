using DataTransferObject.GlobalObject;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiService.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger)
        {
            ApiResponseDefault<object> response;
            int statusCode;

            if (ex is XenniException)
            {
                // Known business / application error
                statusCode = (int)HttpStatusCode.BadRequest;
                response = ApiResponseDefault<object>.Fail(ex.Message);
            }
            else
            {
                // Unexpected / unhandled error
                statusCode = (int)HttpStatusCode.InternalServerError;
                response = ApiResponseDefault<object>.Fail("Internal server error");

                // Log full details for debugging
                logger.LogError(ex, "Unhandled exception occurred.");
            }

            context.Response.StatusCode = statusCode;

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }

    }
}
