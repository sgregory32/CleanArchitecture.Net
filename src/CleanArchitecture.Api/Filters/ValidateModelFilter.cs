using CleanArchitecture.Api.Filters.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            else if (!context.ModelState.IsValid)
            {
                String errorMessage = ErrorUtilities.BuildErrorMessage(GetModelStateErrors(context));

                ApiError apiError = new ApiError(errorMessage) { Detail = null };

                context.HttpContext.Response.StatusCode = 400;
                _logger.LogError("HTTP status code 400 occurred. " + errorMessage);
                context.Result = new JsonResult(apiError);
            }
        }

        public static List<string> GetModelStateErrors(ActionExecutingContext context)
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