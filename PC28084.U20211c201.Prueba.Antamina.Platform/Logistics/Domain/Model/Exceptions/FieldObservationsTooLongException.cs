using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Exceptions;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Exceptions;

/// <summary>
///     Exception thrown when the field observations exceed the maximum length.
/// </summary>
/// <param name="actualLength">The length of the text that was received.</param>
/// <param name="maxLength">The maximum length allowed.</param>
/// <param name="paramName">The name of the argument that violates the rule.</param>
/// <remarks>
///     It is an <see cref="ArgumentOutOfRangeException" /> that also exposes a resource key, so the response can be
///     translated according to the request culture.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public sealed class FieldObservationsTooLongException(int actualLength, int maxLength, string paramName)
    : ArgumentOutOfRangeException(paramName, actualLength,
            $"The field observations must not exceed {maxLength} characters."),
        ILocalizableException
{
    /// <inheritdoc />
    public string ResourceKey => "Validation.FieldObservationsTooLong";
}
