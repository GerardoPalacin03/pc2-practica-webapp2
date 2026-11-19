namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Interfaces.Rest.Resources;

/// <summary>
///     Resource returned by the REST API to represent an ore dispatch ticket.
/// </summary>
/// <param name="Id">The generated identifier of the ticket.</param>
/// <param name="TicketCode">The code that identifies the ticket.</param>
/// <param name="TruckCode">The code of the haul truck.</param>
/// <param name="DispatchDate">The date of the dispatch (yyyy-MM-dd).</param>
/// <param name="ShiftCode">The code of the shift, in upper case.</param>
/// <param name="CargoWeightTons">The weight of the cargo in tons.</param>
/// <param name="CargoWeightUnit">The unit of the weight.</param>
/// <param name="OreGrade">The name of the ore grade.</param>
/// <param name="Status">The name of the dispatch status.</param>
/// <param name="StatusCode">The numeric value of the dispatch status, for example 100.</param>
/// <param name="EstimatedHaulageFee">The estimated haulage fee of the cargo, rounded to 2 decimals.</param>
/// <param name="SupervisorUsername">The username of the supervisor on duty.</param>
/// <param name="FieldObservations">The optional observations made in the field.</param>
/// <param name="CreatedAt">The date and time when the record was created.</param>
/// <param name="UpdatedAt">The date and time when the record was last updated.</param>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public record OreDispatchTicketResource(
    int Id,
    string TicketCode,
    string TruckCode,
    string DispatchDate,
    string ShiftCode,
    decimal CargoWeightTons,
    string CargoWeightUnit,
    string OreGrade,
    string Status,
    int StatusCode,
    decimal EstimatedHaulageFee,
    string SupervisorUsername,
    string? FieldObservations,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
