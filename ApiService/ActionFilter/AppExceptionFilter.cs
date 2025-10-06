using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiService.ActionFilter
{
    public class AppExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
