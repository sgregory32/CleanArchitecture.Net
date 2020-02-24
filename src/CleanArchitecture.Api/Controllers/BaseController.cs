using CleanArchitecture.Api.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Base Api Controller annotated with Api route, and model validation & Api exception filters.
/// </summary>

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors()]
    [ServiceFilter(typeof(ValidateModelFilter))]
    [ServiceFilter(typeof(ApiExceptionFilter))]
    public abstract class BaseController : ControllerBase
    {
    }
}