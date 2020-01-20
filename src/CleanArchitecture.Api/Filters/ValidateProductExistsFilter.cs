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
    /// Filter to validate if Product exists based on Product Id, and log detail if not.
    /// Returns HTTP 404 error: NotFoundObjectResult.
    /// </summary>

    public class ValidateProductExistsFilter : TypeFilterAttribute
    {
        public ValidateProductExistsFilter() : base(typeof(ValidateProductExistsFilterImpl))
        {
        }

        private class ValidateProductExistsFilterImpl : IAsyncActionFilter
        {
            private readonly AppDbContext _dbContext;
            private readonly ILogger<ValidateProductExistsFilter> _logger;

            public ValidateProductExistsFilterImpl(AppDbContext dbContext, ILogger<ValidateProductExistsFilter> logger)
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
                        if (_dbContext.Product.AsNoTracking().All(p => p.Id != id.Value))
                        {
                            _logger.LogError("HTTP status code 404 occurred. Product.Product.Id: " + id.Value + " not found.");
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
