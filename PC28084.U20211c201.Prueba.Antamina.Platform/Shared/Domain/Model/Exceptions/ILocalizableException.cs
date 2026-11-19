namespace PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Exceptions;

/// <summary>
///     Contract for domain exceptions that expose a stable resource key.
/// </summary>
/// <remarks>
///     The domain layer must not depend on localization services. Instead, it throws exceptions that carry a
///     resource key, so the interfaces layer can translate the message according to the request culture.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public interface ILocalizableException
{
    /// <summary>
    ///     Gets the resource key used to look up the localized message.
    /// </summary>
    string ResourceKey { get; }
}
