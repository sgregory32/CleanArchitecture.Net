using CleanArchitecture.Api.Filters;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Base Api Controller annotated with Api route, and model validation & Api exception filters.
/// </summary>

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ValidateModelFilter))]
    [ServiceFilter(typeof(ApiExceptionFilter))]
    public abstract class BaseController : ControllerBase
    {
    }
}