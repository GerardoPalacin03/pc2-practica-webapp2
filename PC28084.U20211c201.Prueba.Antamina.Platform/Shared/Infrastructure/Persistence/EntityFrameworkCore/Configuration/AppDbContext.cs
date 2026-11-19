using Microsoft.EntityFrameworkCore;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context for the Antamina Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
/// <remarks>
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    ///     NRC8084 - 20211c201 - Apellidos, Nombres
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Logistics Context
        builder.ApplyLogisticsConfiguration();

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
