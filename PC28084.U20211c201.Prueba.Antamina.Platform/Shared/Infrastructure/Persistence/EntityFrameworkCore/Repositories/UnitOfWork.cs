using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Repositories;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Unit of work for the application.
/// </summary>
/// <remarks>
///     This class is used to save changes to the database context.
///     It implements the IUnitOfWork interface.
/// </remarks>
/// <param name="context">
///     The database context for the application
/// </param>
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    // inheritedDoc
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}