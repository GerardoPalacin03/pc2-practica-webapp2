using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Resources;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Resources.Errors;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Interfaces.Rest.ProblemDetails;

/// <summary>
///     Builds the localized HTTP 400 response returned when the request itself is invalid.
/// </summary>
/// <remarks>
///     It covers a malformed JSON body or fields with an invalid primitive type (for example, a text where a number
///     is expected, a wrong date format or an enum name that does not exist). Business rule violations are not handled
///     here: they are HTTP 422 and are produced by the application layer.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public static class InvalidRequestResponseFactory
{
    /// <summary>
    ///     Creates the HTTP 400 response for an invalid request.
    /// </summary>
    /// <param name="context">The action context of the failed model binding.</param>
    /// <returns>A <see cref="BadRequestObjectResult" /> with a localized problem details body.</returns>
    public static IActionResult Create(ActionContext context)
    {
        var services = context.HttpContext.RequestServices;
        var commonLocalizer = services.GetRequiredService<IStringLocalizer<CommonMessages>>();
        var errorLocalizer = services.GetRequiredService<IStringLocalizer<ErrorMessages>>();

        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = commonLocalizer["BadRequest"],
            Detail = errorLocalizer["Request.InvalidBody"],
            Instance = context.HttpContext.Request.Path
        };

        var result = new BadRequestObjectResult(problemDetails);
        result.ContentTypes.Add("application/problem+json");
        return result;
    }
}
