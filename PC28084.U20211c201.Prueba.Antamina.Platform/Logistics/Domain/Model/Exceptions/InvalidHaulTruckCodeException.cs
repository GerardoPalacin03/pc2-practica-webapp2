using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Exceptions;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Exceptions;

/// <summary>
///     Exception thrown when the code of a haul truck does not follow the required format.
/// </summary>
/// <param name="paramName">The name of the argument that violates the rule.</param>
/// <remarks>
///     It is an <see cref="ArgumentException" /> that also exposes a resource key, so the response can be translated
///     according to the request culture.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public sealed class InvalidHaulTruckCodeException(string paramName)
    : ArgumentException(
            "The truck code must follow the format CAT-797F-XXX or KOM-930E-XXX, where XXX is a number between 001 and 999.",
            paramName),
        ILocalizableException
{
    /// <inheritdoc />
    public string ResourceKey => "Validation.InvalidHaulTruckCode";
}
