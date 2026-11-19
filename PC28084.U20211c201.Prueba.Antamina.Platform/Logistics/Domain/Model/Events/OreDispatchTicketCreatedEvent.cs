using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Events;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Events;

/// <summary>
///     Domain event raised when a new ore dispatch ticket is created.
/// </summary>
/// <param name="TicketCode">The code of the created ticket.</param>
/// <param name="TruckCode">The code of the truck assigned to the ticket.</param>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public record OreDispatchTicketCreatedEvent(AntaminaTicketId TicketCode, HaulTruckCode TruckCode) : IEvent;
