using Microsoft.EntityFrameworkCore;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Aggregates;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Repositories;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     EF Core implementation of the <see cref="IOreDispatchTicketRepository" />.
/// </summary>
/// <param name="context">The database context.</param>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public class OreDispatchTicketRepository(AppDbContext context)
    : BaseRepository<OreDispatchTicket>(context), IOreDispatchTicketRepository
{
    /// <inheritdoc />
    public async Task<bool> ExistsByTicketCodeAsync(AntaminaTicketId ticketCode,
        CancellationToken cancellationToken)
    {
        return await Context.Set<OreDispatchTicket>()
            .AnyAsync(ticket => ticket.TicketCode == ticketCode, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByTruckCodeAndShiftWindowAsync(HaulTruckCode truckCode,
        ShiftWindow operationalShift, CancellationToken cancellationToken)
    {
        var dispatchDate = operationalShift.DispatchDate;
        var shiftCode = operationalShift.ShiftCode;

        return await Context.Set<OreDispatchTicket>()
            .AnyAsync(ticket => ticket.TruckCode == truckCode
                                && ticket.OperationalShift.DispatchDate == dispatchDate
                                && ticket.OperationalShift.ShiftCode == shiftCode,
                cancellationToken);
    }
}
