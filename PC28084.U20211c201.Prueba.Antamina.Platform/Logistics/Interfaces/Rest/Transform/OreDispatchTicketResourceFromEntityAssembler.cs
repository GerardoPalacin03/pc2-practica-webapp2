using System.Globalization;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Aggregates;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Interfaces.Rest.Resources;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Interfaces.Rest.Transform;

/// <summary>
///     Assembler that transforms an <see cref="OreDispatchTicket" /> aggregate into an
///     <see cref="OreDispatchTicketResource" />.
/// </summary>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public static class OreDispatchTicketResourceFromEntityAssembler
{
    private const string DateFormat = "yyyy-MM-dd";

    /// <summary>
    ///     The rate per ton used to estimate the haulage fee that is shown in the resource.
    /// </summary>
    private const decimal HaulageRatePerTon = 1.45m;

    /// <summary>
    ///     Converts an <see cref="OreDispatchTicket" /> to its <see cref="OreDispatchTicketResource" /> representation.
    /// </summary>
    /// <param name="entity">The ore dispatch ticket aggregate. It must not be null.</param>
    /// <returns>An <see cref="OreDispatchTicketResource" /> that represents the ticket.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity" /> is null.</exception>
    public static OreDispatchTicketResource ToResourceFromEntity(OreDispatchTicket entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new OreDispatchTicketResource(
            entity.Id,
            entity.TicketCode.Value.ToString(),
            entity.TruckCode.Value,
            entity.OperationalShift.DispatchDate.ToString(DateFormat, CultureInfo.InvariantCulture),
            entity.OperationalShift.ShiftCode,
            entity.CargoWeight.Tons,
            entity.CargoWeight.Unit,
            entity.OreGrade.ToString(),
            entity.Status.ToString(),
            (int)entity.Status,
            entity.CargoWeight.CalculateHaulageFee(HaulageRatePerTon),
            entity.SupervisorUsername,
            entity.FieldObservations,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
