using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Aggregates;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Commands;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Application.Model;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Application.CommandServices;

/// <summary>
///     Command service contract for the <see cref="OreDispatchTicket" /> aggregate.
/// </summary>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public interface IOreDispatchTicketCommandService
{
    /// <summary>
    ///     Handles the <see cref="CreateOreDispatchTicketCommand" />.
    /// </summary>
    /// <param name="command">The command with the data of the ticket to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> with the created <see cref="OreDispatchTicket" />, or with the error that
    ///     prevented its creation: a conflict error for duplicates or an unprocessable entity error for a business rule
    ///     violation.
    /// </returns>
    Task<Result<OreDispatchTicket>> Handle(CreateOreDispatchTicketCommand command,
        CancellationToken cancellationToken);
}
