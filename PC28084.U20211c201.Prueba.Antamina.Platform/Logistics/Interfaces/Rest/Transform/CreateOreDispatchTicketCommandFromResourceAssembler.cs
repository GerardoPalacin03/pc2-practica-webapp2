using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Commands;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Interfaces.Rest.Resources;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Interfaces.Rest.Transform;

/// <summary>
///     Assembler that transforms a <see cref="CreateOreDispatchTicketResource" /> into a
///     <see cref="CreateOreDispatchTicketCommand" />.
/// </summary>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public static class CreateOreDispatchTicketCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreateOreDispatchTicketResource" /> to a <see cref="CreateOreDispatchTicketCommand" />.
    /// </summary>
    /// <param name="resource">The resource received by the API. It must not be null.</param>
    /// <returns>A new <see cref="CreateOreDispatchTicketCommand" />.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="resource" /> is null.</exception>
    public static CreateOreDispatchTicketCommand ToCommandFromResource(CreateOreDispatchTicketResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new CreateOreDispatchTicketCommand(
            resource.TicketCode,
            resource.TruckCode,
            resource.DispatchDate,
            resource.ShiftCode,
            resource.CargoWeightTons,
            resource.OreGrade,
            resource.SupervisorUsername,
            resource.CargoWeightUnit,
            resource.Status,
            resource.FieldObservations);
    }
}
