using CleanArchitecture.Api.Filters.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Api.Filters
{
    /// <summary>
    /// Filter to catch, display, & log all unhandled Api exceptions. 
    /// Returns Http 500: InternalServerError.
    /// </summary>
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            string stack = null;
            var errorMessage = "HTTP status code 500 occurred. Unhandled exception: " + context.Exception.GetBaseException().Message;

#if DEBUG
            stack = context.Exception.StackTrace;
#endif

            ApiError apiError = new ApiError(errorMessage) { Detail = stack };

            context.HttpContext.Response.StatusCode = 500;
            _logger.LogError(context.Exception, errorMessage);
            context.Result = new JsonResult(apiError);

            base.OnException(context);
        }
    }
}
