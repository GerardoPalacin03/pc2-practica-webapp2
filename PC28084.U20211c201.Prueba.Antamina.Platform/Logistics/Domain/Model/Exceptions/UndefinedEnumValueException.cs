using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Exceptions;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Exceptions;

/// <summary>
///     Exception thrown when a number that does not match any member of an enum is received.
/// </summary>
/// <param name="paramName">The name of the argument that violates the rule.</param>
/// <param name="actualValue">The value that was received.</param>
/// <remarks>
///     It is an <see cref="ArgumentOutOfRangeException" /> that also exposes a resource key, so the response can be
///     translated according to the request culture.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public sealed class UndefinedEnumValueException(string paramName, object actualValue)
    : ArgumentOutOfRangeException(paramName, actualValue, "The value received is not defined for the field."),
        ILocalizableException
{
    /// <inheritdoc />
    public string ResourceKey => "Validation.UndefinedEnumValue";
}
