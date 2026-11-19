using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Application.CommandServices;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Aggregates;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Commands;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Errors;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Repositories;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Application.Model;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Repositories;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Application.Internal.CommandServices;

/// <summary>
///     Command service that orchestrates the creation of ore dispatch tickets.
/// </summary>
/// <param name="oreDispatchTicketRepository">The ore dispatch ticket repository.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <remarks>
///     This service only coordinates: the rules of the value objects and of the aggregate live in the domain, while
///     the uniqueness rules need the repository, so they are checked here.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public class OreDispatchTicketCommandService(
    IOreDispatchTicketRepository oreDispatchTicketRepository,
    IUnitOfWork unitOfWork)
    : IOreDispatchTicketCommandService
{
    /// <inheritdoc />
    public async Task<Result<OreDispatchTicket>> Handle(CreateOreDispatchTicketCommand command,
        CancellationToken cancellationToken)
    {
        OreDispatchTicket oreDispatchTicket;

        try
        {
            oreDispatchTicket = new OreDispatchTicket(command);
        }
        catch (ArgumentException exception)
        {
            return Result<OreDispatchTicket>.Failure(LogisticsErrors.FromArgumentException(exception));
        }

        if (await oreDispatchTicketRepository.ExistsByTicketCodeAsync(oreDispatchTicket.TicketCode,
                cancellationToken))
            return Result<OreDispatchTicket>.Failure(LogisticsErrors.DuplicateTicketCode);

        if (await oreDispatchTicketRepository.ExistsByTruckCodeAndShiftWindowAsync(oreDispatchTicket.TruckCode,
                oreDispatchTicket.OperationalShift, cancellationToken))
            return Result<OreDispatchTicket>.Failure(LogisticsErrors.TruckAlreadyAssignedToShift);

        await oreDispatchTicketRepository.AddAsync(oreDispatchTicket, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<OreDispatchTicket>.Success(oreDispatchTicket);
    }
}
