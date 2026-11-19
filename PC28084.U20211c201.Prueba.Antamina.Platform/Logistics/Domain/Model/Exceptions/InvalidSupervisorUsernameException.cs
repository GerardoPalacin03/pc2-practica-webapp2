using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Exceptions;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Exceptions;

/// <summary>
///     Exception thrown when the supervisor username is null, blank or its length is out of the allowed range.
/// </summary>
/// <param name="paramName">The name of the argument that violates the rule.</param>
/// <remarks>
///     It is an <see cref="ArgumentException" /> that also exposes a resource key, so the response can be translated
///     according to the request culture.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public sealed class InvalidSupervisorUsernameException(string paramName)
    : ArgumentException("The supervisor username is required and must have between 5 and 25 characters.", paramName),
        ILocalizableException
{
    /// <inheritdoc />
    public string ResourceKey => "Validation.InvalidSupervisorUsername";
}
