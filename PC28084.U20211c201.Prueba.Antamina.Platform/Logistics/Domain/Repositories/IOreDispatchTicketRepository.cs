using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Aggregates;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Repositories;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Repositories;

/// <summary>
///     Repository contract for the <see cref="OreDispatchTicket" /> aggregate.
/// </summary>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public interface IOreDispatchTicketRepository : IBaseRepository<OreDispatchTicket>
{
    /// <summary>
    ///     Checks whether an ore dispatch ticket with the given ticket code already exists.
    /// </summary>
    /// <param name="ticketCode">The ticket code to look for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> if a ticket with that code exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsByTicketCodeAsync(AntaminaTicketId ticketCode, CancellationToken cancellationToken);

    /// <summary>
    ///     Checks whether a truck already has an ore dispatch ticket for the same dispatch date and shift code.
    /// </summary>
    /// <param name="truckCode">The code of the truck.</param>
    /// <param name="operationalShift">The shift window (dispatch date and shift code) to look for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> if the truck already has a ticket in that shift window; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsByTruckCodeAndShiftWindowAsync(HaulTruckCode truckCode, ShiftWindow operationalShift,
        CancellationToken cancellationToken);
}
