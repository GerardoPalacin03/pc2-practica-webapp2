using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Exceptions;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Exceptions;

/// <summary>
///     Exception thrown when the shift code is null, blank or different from DAY-A and NIGHT-B.
/// </summary>
/// <param name="paramName">The name of the argument that violates the rule.</param>
/// <remarks>
///     It is an <see cref="ArgumentException" /> that also exposes a resource key, so the response can be translated
///     according to the request culture.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public sealed class InvalidShiftCodeException(string paramName)
    : ArgumentException("The shift code must be DAY-A or NIGHT-B.", paramName),
        ILocalizableException
{
    /// <inheritdoc />
    public string ResourceKey => "Validation.InvalidShiftCode";
}
