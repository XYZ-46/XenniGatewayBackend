using ApiService.Config;
using ApiService.DataValidator;
using ApiService.Helper;
using Domain.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.Text.Json;

namespace ApiService.ActionFilter
{
    public class DuplicateKeyValidationFilter : IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            // Step 1: Check if action has a body parameter
            bool hasBodyParam = context.ActionDescriptor.Parameters
                .OfType<Microsoft.AspNetCore.Mvc.Abstractions.ParameterDescriptor>()
                .Any(p =>
                    p.BindingInfo?.BindingSource == Microsoft.AspNetCore.Mvc.ModelBinding.BindingSource.Body
                    || (p.BindingInfo?.BindingSource == null && !p.ParameterType.IsPrimitive && p.ParameterType != typeof(string))
                );

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
                request.Body.Position = 0;
            }

            var validation = new ValidationResultModel();

            if (string.IsNullOrWhiteSpace(jsonBody))
            {
                throw new XenniException("Empty request body");
            }
            else
            {
                // Step 3: Check JSON syntax with relaxed rules
                try
                {
                    JsonSerializer.Deserialize<object>(jsonBody, JsonOpt.ReadOptions);
                }
                catch (JsonException)
                {
                    throw new XenniException("Invalid or malformed JSON");
                }

                // Step 4: Detect duplicate keys (nested + case-insensitive)
                var duplicates = JsonChecker.GetDuplicateJsonKeys(jsonBody);
                foreach (var key in duplicates)
                {
                    validation.AddError(key, $"Duplicate field '{key}' is not allowed.");
                }
            }

            // Step 5: Short-circuit if invalid
            if (!validation.IsValid)
            {
                context.Result = new BadRequestObjectResult(validation.ToResponse());
                return;
            }

            // Step 6: Continue pipeline
            await next();
        }


    }

}
