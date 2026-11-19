namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;

/// <summary>
///     Represents the status of an ore dispatch ticket.
/// </summary>
/// <remarks>
///     The values are not consecutive and are persisted as integers in the database.
///     <see cref="Scheduled" /> is the default status of a new ticket.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public enum DispatchStatus
{
    /// <summary>
    ///     The dispatch is scheduled. This is the default status.
    /// </summary>
    Scheduled = 100,

    /// <summary>
    ///     The truck is hauling the ore.
    /// </summary>
    InTransit = 205,

    /// <summary>
    ///     The cargo was weighed at the plant.
    /// </summary>
    WeighedAtPlant = 310,

    /// <summary>
    ///     The dispatch was rejected.
    /// </summary>
    Rejected = 999
}
