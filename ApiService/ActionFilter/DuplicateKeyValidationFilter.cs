using ApiService.DataValidator.BaseValidator;
using DataTransferObject.GlobalObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
                    var options = new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true,
                        PropertyNameCaseInsensitive = true,
                        ReadCommentHandling = JsonCommentHandling.Skip,
                        NumberHandling = JsonNumberHandling.AllowReadingFromString
                    };

                    JsonSerializer.Deserialize<object>(jsonBody, options);
                }
                catch (JsonException)
                {
                    throw new XenniException("Invalid or malformed JSON");
                }

                // Step 4: Detect duplicate keys (nested + case-insensitive)
                var duplicates = GetDuplicateKeys(jsonBody);
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


        // -----------------------------
        // Duplicate key detection logic
        // -----------------------------
        private List<string> GetDuplicateKeys(string json)
        {
            var duplicates = new List<string>();

            try
            {
                var bytes = Encoding.UTF8.GetBytes(json);
                var readerOptions = new JsonReaderOptions
                {
                    AllowTrailingCommas = true,
                    CommentHandling = JsonCommentHandling.Skip
                };
                var reader = new Utf8JsonReader(bytes, readerOptions);

                var pathStack = new Stack<string>();
                var keyStack = new Stack<HashSet<string>>();
                keyStack.Push(new HashSet<string>(System.StringComparer.OrdinalIgnoreCase));

                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.StartObject:
                            keyStack.Push(new HashSet<string>(System.StringComparer.OrdinalIgnoreCase));
                            break;

                        case JsonTokenType.EndObject:
                            keyStack.Pop();
                            if (pathStack.Count > 0) pathStack.Pop();
                            break;

                        case JsonTokenType.StartArray:
                            pathStack.Push("[]");
                            break;

                        case JsonTokenType.EndArray:
                            pathStack.Pop();
                            break;

                        case JsonTokenType.PropertyName:
                            string key = reader.GetString()!;
                            string fullPath = pathStack.Count > 0 ? string.Join(".", pathStack) + "." + key : key;

                            var currentKeys = keyStack.Peek();
                            if (!currentKeys.Add(key))
                            {
                                duplicates.Add(fullPath);
                            }

                            pathStack.Push(key);
                            break;

                        case JsonTokenType.String:
                        case JsonTokenType.Number:
                        case JsonTokenType.True:
                        case JsonTokenType.False:
                        case JsonTokenType.Null:
                            if (pathStack.Count > 0) pathStack.Pop();
                            break;
                    }
                }
            }
            catch (JsonException)
            {
                // Ignore, already handled in main filter
            }

            return duplicates;
        }
    }

}
