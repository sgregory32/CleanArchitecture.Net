using CleanArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Filters
{
    /// <summary>
    /// Filter to validate if Category exists based on Category Id, and log detail if not.
    /// Returns HTTP 404 error: NotFoundObjectResult.
    /// </summary>
    
    public class ValidateCategoryExistsFilter : TypeFilterAttribute
    {
        public ValidateCategoryExistsFilter()
            : base(typeof(ValidateCategoryExistsFilterImpl))
        {
        }
        private class ValidateCategoryExistsFilterImpl : IAsyncActionFilter
        {
            private readonly AppDbContext _dbContext;
            private readonly ILogger<ValidateCategoryExistsFilter> _logger;

            public ValidateCategoryExistsFilterImpl(AppDbContext dbContext, ILogger<ValidateCategoryExistsFilter> logger)
            {
                _dbContext = dbContext;
                _logger = logger;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    var id = context.ActionArguments["id"] as int?;

                    if (id.HasValue)
                    {
                        if (_dbContext.Category.AsNoTracking().All(c => c.Id != id.Value))
                        {
                            _logger.LogError("HTTP status code 404 occurred. Category.Category.Id: " + id.Value + " not found.");
                            context.Result = new NotFoundObjectResult(id.Value);
                            return;
                        }
                    }
                }
                await next().ConfigureAwait(false);
            }
        }
    }
}
