using CleanArchitecture.Api.Filters.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Api.Filters
{
    /// <summary>
    /// Filter to validate if model state is valid, and log detail if not.
    /// Returns HTTP 400 error: BadRequest.
    /// </summary>
    
    public class ValidateModelFilter : ActionFilterAttribute
    {
        private readonly ILogger<ValidateModelFilter> _logger;

        public ValidateModelFilter(ILogger<ValidateModelFilter> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                StringBuilder errorMessage = BuildErrorMessage(GetModelStateErrors(context));

                ApiError apiError = new ApiError(errorMessage.ToString()) { Detail = null };

                context.HttpContext.Response.StatusCode = 400;
                _logger.LogError("HTTP status code 400 occurred. " + errorMessage.ToString());
                context.Result = new JsonResult(apiError);
            }
        }

        private static StringBuilder BuildErrorMessage(List<string> modelErrors)
        {
            StringBuilder msg = new StringBuilder();

            msg.Append("Invalid ModelState: ");

            foreach (var error in modelErrors)
            {
                msg.Append(error + " ");
            }

            return msg;
        }

        private static List<string> GetModelStateErrors(ActionExecutingContext context)
        {
            List<string> modelErrors = new List<string>();
            foreach (var modelState in context.ModelState.Values)
            {
                foreach (var modelError in modelState.Errors)
                {
                    modelErrors.Add(modelError.ErrorMessage);
                }
            }
            return modelErrors;
        }
    }
}