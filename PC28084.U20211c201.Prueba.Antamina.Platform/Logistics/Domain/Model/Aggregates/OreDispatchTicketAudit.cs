using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Entities;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Aggregates;

/// <summary>
///     Audit part of the <see cref="OreDispatchTicket" /> aggregate.
/// </summary>
/// <remarks>
///     The timestamps are filled automatically by the <c>AuditableEntityInterceptor</c> when the aggregate is saved.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public partial class OreDispatchTicket : IAuditableEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }
}
