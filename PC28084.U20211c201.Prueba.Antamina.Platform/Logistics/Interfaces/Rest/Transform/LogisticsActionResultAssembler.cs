using Microsoft.AspNetCore.Mvc;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Aggregates;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Application.Model;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Errors;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Interfaces.Rest.Transform;

/// <summary>
///     Assembler that transforms the results of the Logistics application services into HTTP action results.
/// </summary>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public static class LogisticsActionResultAssembler
{
    /// <summary>
    ///     Converts the result of creating an ore dispatch ticket into an action result.
    /// </summary>
    /// <param name="controller">The controller that produces the response.</param>
    /// <param name="result">The result returned by the command service.</param>
    /// <param name="problemDetailsFactory">The factory used to build localized problem details on failure.</param>
    /// <param name="successAction">The function that builds the response when the result is successful.</param>
    /// <returns>
    ///     The response built by <paramref name="successAction" /> on success; otherwise, a problem details response
    ///     with the HTTP status that matches the error type.
    /// </returns>
    public static IActionResult ToActionResultFromCreateOreDispatchTicketResult(
        ControllerBase controller,
        Result<OreDispatchTicket> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<OreDispatchTicket, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = result.DomainError;
        return problemDetailsFactory.CreateProblemDetails(
            controller,
            ToStatusCodeFromErrorType(error.Type),
            error.Type.ToString(),
            error.Code);
    }

    /// <summary>
    ///     Maps the type of an error to the HTTP status that the API returns.
    /// </summary>
    /// <param name="errorType">The type of the error.</param>
    /// <returns>
    ///     400 for validation, 409 for conflict, 422 for unprocessable entity, and so on; 500 for any other type.
    /// </returns>
    private static int ToStatusCodeFromErrorType(EErrorType errorType)
    {
        return errorType switch
        {
            EErrorType.Validation => StatusCodes.Status400BadRequest,
            EErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            EErrorType.Forbidden => StatusCodes.Status403Forbidden,
            EErrorType.NotFound => StatusCodes.Status404NotFound,
            EErrorType.Conflict => StatusCodes.Status409Conflict,
            EErrorType.UnprocessableEntity => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
