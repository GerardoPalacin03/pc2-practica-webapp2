namespace PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Exceptions;

/// <summary>
///     Exception thrown when the unit of a mineral weight is null, empty or blank.
/// </summary>
/// <param name="paramName">The name of the argument that violates the rule.</param>
/// <remarks>
///     It is an <see cref="ArgumentNullException" /> that also exposes a resource key, so the response can be
///     translated according to the request culture.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public sealed class BlankWeightUnitException(string paramName)
    : ArgumentNullException(paramName, "The unit of a mineral weight must not be null or blank."),
        ILocalizableException
{
    /// <inheritdoc />
    public string ResourceKey => "Validation.BlankWeightUnit";
}
