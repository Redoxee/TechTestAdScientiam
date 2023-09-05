using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TestTechnique.WebApi.Controllers;

/// <summary>
/// Controller API for handle errors.
/// </summary>
/// <remarks>Not visible from Swagger.</remarks>
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    /// <summary>
    /// Error handler for local development.
    /// </summary>
    /// <param name="webHostEnvironment"></param>
    /// <returns>The detailed exception.</returns>
    /// <exception cref="InvalidOperationException">Throw if invoked in non-development environments.</exception>
    [Route("/error-local-development")]
    public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.EnvironmentName != "Development")
        {
            throw new InvalidOperationException("This shouldn't be invoked in non-development environments.");
        }

        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

        return Problem(detail: context.Error.StackTrace, title: context.Error.Message);
    }

    /// <summary>
    /// Generic error handler.
    /// </summary>
    /// <returns></returns>
    [Route("/error")]
    public IActionResult Error() => Problem();
}