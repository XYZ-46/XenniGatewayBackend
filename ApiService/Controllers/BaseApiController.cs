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
        protected async Task<(T? DTO, ValidationResultModel Validation)> ValidateRequestAsync<T>()
        {
            string jsonBody;
            T? newRequest;
            var validation = new ValidationResultModel();
            try
            {
                jsonBody = await new StreamReader(Request.Body).ReadToEndAsync();
                // 1️⃣ Check duplicate keys (including nested)
                var duplicates = JsonDuplicateKeyValidator.GetDuplicateKeys(jsonBody);
                if (duplicates.Count > 0)
                {
                    foreach (var dup in duplicates) validation.AddError(dup, $"Duplicate input field '{dup}' is not allowed.");
                    return (default, validation);
                }

                newRequest = JsonSerializer.Deserialize<T>(jsonBody);
            }
            catch
            {
                validation.AddError("Body", "Invalid or malformed JSON.");
                return (default, validation);
            }

            if (newRequest is null)
            {
                validation.AddError("Body", "Invalid or malformed JSON.");
                return (default, validation);
            }

            validation = DataAnnotationValidator.Validate(newRequest);
            return (newRequest, validation);
        }

        protected IActionResult ValidationError(ValidationResultModel validation) => BadRequest(validation.ToResponse());

    }
}
