using CustomerManager.API.Http;
using CustomerManager.Domain.Common.Exceptions;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CustomerManager.API.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    [AllowAnonymous]
    [Route("error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error([FromServices] IHostEnvironment hostEnvironment, [FromServices] ILogger<ApiController> logger)
    {
        IExceptionHandlerFeature? exceptionHandlerFeature =
            HttpContext.Features.Get<IExceptionHandlerFeature>();

        if (exceptionHandlerFeature is null)
            return NotFound();

        if (exceptionHandlerFeature.Error is ErrorException errorException)
            return Problem(new List<Error> { errorException.Error });

        logger.LogError("An unexpected error has occurred. exception: {exception}, stack trace: {stackTrace}", exceptionHandlerFeature.Error.Message, exceptionHandlerFeature.Error.StackTrace);

        if (!hostEnvironment.IsDevelopment())
        {
            return Problem(new List<Error> { ErrorOr.Error.Unexpected() });
        }

        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        return Problem(errors[0]);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}