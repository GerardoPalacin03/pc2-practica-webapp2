using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Exceptions;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.ValueObjects;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;

/// <summary>
///     Value object that uniquely identifies an ore dispatch ticket across Antamina.
/// </summary>
/// <remarks>
///     The GUID is generated in another bounded context, so this bounded context only checks that it is not empty.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public readonly record struct AntaminaTicketId : IEntityId<Guid>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AntaminaTicketId" /> value object.
    /// </summary>
    /// <param name="value">The GUID that identifies the ticket. It must not be empty.</param>
    /// <exception cref="InvalidTicketCodeException">Thrown when <paramref name="value" /> is an empty GUID.</exception>
    public AntaminaTicketId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidTicketCodeException(nameof(value));

        Value = value;
    }

    /// <inheritdoc />
    public Guid Value { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Value.ToString();
    }
}
