using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Events;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Application.Internal.EventHandlers;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="OreDispatchTicketCreatedEvent" />.
/// </summary>
/// <param name="logger">The logger.</param>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public class OreDispatchTicketCreatedEventHandler(ILogger<OreDispatchTicketCreatedEventHandler> logger)
    : IEventHandler<OreDispatchTicketCreatedEvent>
{
    /// <inheritdoc />
    public Task Handle(OreDispatchTicketCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Ore dispatch ticket created: ticket {TicketCode} for truck {TruckCode}",
            domainEvent.TicketCode.Value, domainEvent.TruckCode.Value);

        return Task.CompletedTask;
    }
}
