using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Commands;

/// <summary>
///     Command that orders the creation of an ore dispatch ticket.
/// </summary>
/// <param name="TicketCode">The GUID that identifies the ticket.</param>
/// <param name="TruckCode">The code of the haul truck.</param>
/// <param name="DispatchDate">The date of the dispatch.</param>
/// <param name="ShiftCode">The code of the shift (DAY-A or NIGHT-B).</param>
/// <param name="CargoWeightTons">The weight of the cargo in tons.</param>
/// <param name="OreGrade">The type of mineral concentrate.</param>
/// <param name="SupervisorUsername">The username of the supervisor on duty.</param>
/// <param name="CargoWeightUnit">The unit of the weight. When it is <c>null</c>, the default unit (MT) is used.</param>
/// <param name="Status">The status of the ticket. When it is <c>null</c>, the ticket is created as scheduled.</param>
/// <param name="FieldObservations">Optional observations made in the field.</param>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public record CreateOreDispatchTicketCommand(
    Guid TicketCode,
    string? TruckCode,
    DateOnly DispatchDate,
    string? ShiftCode,
    decimal CargoWeightTons,
    OreGradeType OreGrade,
    string? SupervisorUsername,
    string? CargoWeightUnit,
    DispatchStatus? Status,
    string? FieldObservations);
