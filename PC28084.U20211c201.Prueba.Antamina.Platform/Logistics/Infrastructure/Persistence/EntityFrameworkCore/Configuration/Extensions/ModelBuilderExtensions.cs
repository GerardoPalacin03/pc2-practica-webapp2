using Microsoft.EntityFrameworkCore;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Aggregates;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Fluent API configuration of the Logistics bounded context.
/// </summary>
/// <remarks>
///     The persistence concepts are kept here, apart from the domain aggregates. The column names are converted to
///     snake case and the table name to plural snake case by the shared naming convention.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the persistence configuration of the Logistics bounded context.
    /// </summary>
    /// <param name="builder">The model builder of the database context.</param>
    public static void ApplyLogisticsConfiguration(this ModelBuilder builder)
    {
        // OreDispatchTicket aggregate
        builder.Entity<OreDispatchTicket>().HasKey(t => t.Id);
        builder.Entity<OreDispatchTicket>().Property(t => t.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<OreDispatchTicket>().Property(t => t.TicketCode)
            .HasConversion(new StronglyTypedIdConverter<AntaminaTicketId, Guid>(id => id.Value,
                value => new AntaminaTicketId(value)))
            .IsRequired();
        builder.Entity<OreDispatchTicket>().HasIndex(t => t.TicketCode)
            .IsUnique()
            .HasDatabaseName("ux_ore_dispatch_tickets_ticket_code");

        builder.Entity<OreDispatchTicket>().Property(t => t.TruckCode)
            .HasConversion(code => code.Value, value => new HaulTruckCode(value))
            .HasMaxLength(12)
            .IsRequired();

        builder.Entity<OreDispatchTicket>().OwnsOne(t => t.OperationalShift,
            s =>
            {
                s.WithOwner().HasForeignKey("Id");
                s.Property(shift => shift.DispatchDate)
                    .HasColumnName("DispatchDate")
                    .HasConversion(date => date.ToDateTime(TimeOnly.MinValue),
                        dateTime => DateOnly.FromDateTime(dateTime))
                    .IsRequired();
                s.Property(shift => shift.ShiftCode)
                    .HasColumnName("ShiftCode")
                    .HasMaxLength(10)
                    .IsRequired();
            });
        builder.Entity<OreDispatchTicket>().Navigation(t => t.OperationalShift).IsRequired();

        builder.Entity<OreDispatchTicket>().OwnsOne(t => t.CargoWeight,
            w =>
            {
                w.WithOwner().HasForeignKey("Id");
                w.Property(weight => weight.Tons)
                    .HasColumnName("CargoWeightTons")
                    .HasPrecision(18, 3)
                    .IsRequired();
                w.Property(weight => weight.Unit)
                    .HasColumnName("CargoWeightUnit")
                    .HasMaxLength(10)
                    .IsRequired();
            });
        builder.Entity<OreDispatchTicket>().Navigation(t => t.CargoWeight).IsRequired();

        builder.Entity<OreDispatchTicket>().Property(t => t.OreGrade)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        // The dispatch status is persisted as an integer (100, 205, 310 or 999)
        builder.Entity<OreDispatchTicket>().Property(t => t.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Entity<OreDispatchTicket>().Property(t => t.SupervisorUsername)
            .HasMaxLength(OreDispatchTicket.SupervisorUsernameMaxLength)
            .IsRequired();

        builder.Entity<OreDispatchTicket>().Property(t => t.FieldObservations)
            .HasMaxLength(OreDispatchTicket.FieldObservationsMaxLength)
            .IsRequired(false);
    }
}
