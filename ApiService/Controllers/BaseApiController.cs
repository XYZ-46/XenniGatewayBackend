using ApiService.DataValidator.BaseValidator;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiService.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected async Task<(T? Model, ValidationResultModel Validation)> ValidateRequestAsync<T>()
        {
            string jsonBody = await new StreamReader(Request.Body).ReadToEndAsync();
            var newRequest = JsonSerializer.Deserialize<T>(jsonBody);

            if (newRequest is null)
            {
                var invalidJson = new ValidationResultModel();
                invalidJson.AddError("Body", "Invalid or malformed JSON.");
                return (default, invalidJson);
            }

            var validation = DataAnnotationValidator.Validate(newRequest);
            return (newRequest, validation);
        }

        protected IActionResult ValidationError(ValidationResultModel validation) => BadRequest(validation.ToResponse());

    }
}
