using ApiService.DataValidator.BaseValidator;
using DataTransferObject.GlobalObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using System.Text.Json;

namespace ApiService.ActionFilter
{
    public class DuplicateKeyValidationFilter : IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            // 🧩 Step 1: Detect if this action needs body binding
            var hasBodyParam = context.ActionDescriptor.Parameters
                .OfType<Microsoft.AspNetCore.Mvc.Abstractions.ParameterDescriptor>()
                .Any(p =>
                    p.BindingInfo?.BindingSource == BindingSource.Body // explicitly [FromBody]
                    || (p.BindingInfo?.BindingSource == null && !p.ParameterType.IsPrimitive && p.ParameterType != typeof(string))
                // infer body binding if it's a complex type without explicit source
                );

            // If no body parameter, skip reading
            if (!hasBodyParam)
            {
                await next();
                return;
            }

            // Step 2: Read raw JSON body
            var request = context.HttpContext.Request;
            request.EnableBuffering();

            string jsonBody;
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
            {
                jsonBody = await reader.ReadToEndAsync();
                request.Body.Position = 0; // rewind for model binding
            }

            // Step 3: Handle empty body
            if (string.IsNullOrWhiteSpace(jsonBody))
            {
                var error = ApiResponseDefault<object>.Fail("Empty request body.");
                context.Result = new BadRequestObjectResult(error);
                return;
            }

            // Step 4: Handle malformed JSON
            try
            {
                using var _ = JsonDocument.Parse(jsonBody);
            }
            catch (JsonException)
            {
                var error = ApiResponseDefault<object>.Fail("Invalid or malformed JSON.");
                context.Result = new BadRequestObjectResult(error);
                return;
            }

            //  Step 5: Check duplicate keys
            var duplicates = JsonDuplicateKeyValidator.GetDuplicateKeys(jsonBody);
            if (duplicates.Count > 0)
            {
                var validation = new ValidationResultModel();
                foreach (var key in duplicates)
                    validation.AddError(key, $"Duplicate field '{key}' is not allowed.");

                context.Result = new BadRequestObjectResult(validation.ToResponse());
                return;
            }

            //  Step 6: Continue normal pipeline (model binding, controller, etc.)
            await next();
        }
    }
}
