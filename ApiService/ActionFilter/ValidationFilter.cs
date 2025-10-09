using ApiService.DataValidator.BaseValidator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiService.ActionFilter
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var validation = new ValidationResultModel();

                foreach (var (key, value) in context.ModelState)
                {
                    foreach (var error in value.Errors) validation.AddError(key, error.ErrorMessage);
                }

                var response = validation.ToResponse(); // Convert to ApiResponse
                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }
    }
}
