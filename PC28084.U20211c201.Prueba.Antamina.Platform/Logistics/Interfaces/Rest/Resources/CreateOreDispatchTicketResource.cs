using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Interfaces.Rest.Resources;

/// <summary>
///     Resource received by the REST API to create an ore dispatch ticket.
/// </summary>
/// <param name="TicketCode">The GUID that identifies the ticket.</param>
/// <param name="TruckCode">The code of the haul truck: CAT-797F-XXX or KOM-930E-XXX, with XXX between 001 and 999.</param>
/// <param name="DispatchDate">The date of the dispatch (yyyy-MM-dd).</param>
/// <param name="ShiftCode">The code of the shift: DAY-A or NIGHT-B (case insensitive).</param>
/// <param name="CargoWeightTons">The weight of the cargo in tons. It must be greater than zero.</param>
/// <param name="OreGrade">The type of mineral concentrate: CopperConcentrate, ZincConcentrate or MolybdenumConcentrate.</param>
/// <param name="SupervisorUsername">The username of the supervisor on duty, from 5 to 25 characters.</param>
/// <param name="CargoWeightUnit">The optional unit of the weight. By default, MT.</param>
/// <param name="Status">
///     The optional status: Scheduled (default), InTransit, WeighedAtPlant or Rejected.
/// </param>
/// <param name="FieldObservations">Optional observations made in the field, up to 120 characters.</param>
/// <remarks>
///     The attributes of the value objects travel as separate primitive attributes. The optional attributes have a
///     default value so the OpenAPI documentation does not mark them as required.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public record CreateOreDispatchTicketResource(
    Guid TicketCode,
    string? TruckCode,
    DateOnly DispatchDate,
    string? ShiftCode,
    decimal CargoWeightTons,
    OreGradeType OreGrade,
    string? SupervisorUsername,
    string? CargoWeightUnit = null,
    DispatchStatus? Status = null,
    string? FieldObservations = null);
