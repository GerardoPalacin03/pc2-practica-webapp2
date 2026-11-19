using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Errors;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Exceptions;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Errors;

/// <summary>
///     Catalog of the errors of the Logistics bounded context.
/// </summary>
/// <remarks>
///     The error code doubles as the resource key used to localize the error detail. The error type decides the HTTP
///     status: conflicts are HTTP 409 and business rule violations are HTTP 422.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public static class LogisticsErrors
{
    /// <summary>
    ///     Error returned when an ore dispatch ticket with the same ticket code is already registered.
    /// </summary>
    public static readonly Error DuplicateTicketCode =
        Error.Conflict("Logistics.DuplicateTicketCode",
            "An ore dispatch ticket with the same ticket code already exists.");

    /// <summary>
    ///     Error returned when the truck already has a ticket for the same dispatch date and shift code.
    /// </summary>
    public static readonly Error TruckAlreadyAssignedToShift =
        Error.Conflict("Logistics.TruckAlreadyAssignedToShift",
            "The truck already has an ore dispatch ticket for the same dispatch date and shift.");

    /// <summary>
    ///     Generic error returned when the data violates an argument rule without a specific resource key.
    /// </summary>
    public static readonly Error InvalidOreDispatchTicketData =
        Error.UnprocessableEntity("Logistics.InvalidOreDispatchTicketData",
            "The ore dispatch ticket data is invalid.");

    /// <summary>
    ///     Creates an unprocessable entity error from an argument exception thrown by the domain.
    /// </summary>
    /// <param name="exception">The exception thrown while applying a business rule.</param>
    /// <returns>
    ///     An <see cref="Error" /> of type unprocessable entity. When the exception exposes a resource key, it is used
    ///     as the error code.
    /// </returns>
    public static Error FromArgumentException(ArgumentException exception)
    {
        var code = exception is ILocalizableException localizableException
            ? localizableException.ResourceKey
            : InvalidOreDispatchTicketData.Code;

        return Error.UnprocessableEntity(code, exception.Message);
    }
}
