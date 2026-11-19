using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Commands;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Events;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Exceptions;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Aggregates.Internal;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.ValueObjects;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Aggregates;

/// <summary>
///     Ore dispatch ticket aggregate root of the Logistics bounded context.
/// </summary>
/// <remarks>
///     A ticket records that a haul truck carries a cargo of a given ore grade during an operational shift. The rules
///     of each value object are enforced by the value object itself. The rules that cross several attributes (the
///     capacity per ore grade) and the rules of the plain attributes (supervisor and observations) are enforced here,
///     so an invalid ticket can never be created.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public partial class OreDispatchTicket : AggregateRoot
{
    /// <summary>
    ///     The minimum length of the supervisor username.
    /// </summary>
    public const int SupervisorUsernameMinLength = 5;

    /// <summary>
    ///     The maximum length of the supervisor username.
    /// </summary>
    public const int SupervisorUsernameMaxLength = 25;

    /// <summary>
    ///     The maximum length of the field observations.
    /// </summary>
    public const int FieldObservationsMaxLength = 120;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OreDispatchTicket" /> aggregate. Required by EF Core.
    /// </summary>
    protected OreDispatchTicket()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="OreDispatchTicket" /> aggregate from a command.
    /// </summary>
    /// <param name="command">The <see cref="CreateOreDispatchTicketCommand" /> with the data of the ticket.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when any value object or attribute violates a business rule. The exceptions derive from
    ///     <see cref="ArgumentException" />, <see cref="ArgumentNullException" /> or
    ///     <see cref="ArgumentOutOfRangeException" />.
    /// </exception>
    public OreDispatchTicket(CreateOreDispatchTicketCommand command)
    {
        TicketCode = new AntaminaTicketId(command.TicketCode);
        TruckCode = new HaulTruckCode(command.TruckCode);
        OperationalShift = new ShiftWindow(command.DispatchDate, command.ShiftCode);
        CargoWeight = command.CargoWeightUnit is null
            ? new MineralWeight(command.CargoWeightTons)
            : new MineralWeight(command.CargoWeightTons, command.CargoWeightUnit);
        OreGrade = EnsureDefined(command.OreGrade, nameof(command.OreGrade));
        EnsureCargoWithinCapacity(OreGrade, CargoWeight);
        Status = command.Status is null
            ? DispatchStatus.Scheduled
            : EnsureDefined(command.Status.Value, nameof(command.Status));
        SupervisorUsername = EnsureValidSupervisorUsername(command.SupervisorUsername);
        FieldObservations = EnsureValidFieldObservations(command.FieldObservations);
        AddDomainEvent(new OreDispatchTicketCreatedEvent(TicketCode, TruckCode));
    }

    /// <summary>
    ///     Gets the unique identifier of the ticket, generated when it is persisted.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the code that identifies the ticket.
    /// </summary>
    public AntaminaTicketId TicketCode { get; private set; }

    /// <summary>
    ///     Gets the code of the truck that hauls the cargo.
    /// </summary>
    public HaulTruckCode TruckCode { get; private set; }

    /// <summary>
    ///     Gets the operational shift of the dispatch.
    /// </summary>
    public ShiftWindow OperationalShift { get; private set; } = null!;

    /// <summary>
    ///     Gets the weight of the cargo.
    /// </summary>
    public MineralWeight CargoWeight { get; private set; } = null!;

    /// <summary>
    ///     Gets the type of mineral concentrate that is hauled.
    /// </summary>
    public OreGradeType OreGrade { get; private set; }

    /// <summary>
    ///     Gets the status of the ticket.
    /// </summary>
    public DispatchStatus Status { get; private set; } = DispatchStatus.Scheduled;

    /// <summary>
    ///     Gets the username of the supervisor on duty.
    /// </summary>
    public string SupervisorUsername { get; private set; } = string.Empty;

    /// <summary>
    ///     Gets the optional observations made in the field.
    /// </summary>
    public string? FieldObservations { get; private set; }

    /// <summary>
    ///     Checks that a number received for an enum matches one of its members.
    /// </summary>
    private static TEnum EnsureDefined<TEnum>(TEnum value, string paramName) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(value)) throw new UndefinedEnumValueException(paramName, value);

        return value;
    }

    /// <summary>
    ///     Checks the cross invariant: the cargo cannot exceed the capacity allowed for the ore grade.
    /// </summary>
    private static void EnsureCargoWithinCapacity(OreGradeType oreGrade, MineralWeight cargoWeight)
    {
        var maxTons = GetMaxTons(oreGrade);
        if (cargoWeight.Tons > maxTons)
            throw new OreCapacityExceededException(oreGrade, maxTons, cargoWeight.Tons, nameof(cargoWeight));
    }

    private static decimal GetMaxTons(OreGradeType oreGrade)
    {
        return oreGrade switch
        {
            OreGradeType.CopperConcentrate => 380.50m,
            OreGradeType.ZincConcentrate => 400.00m,
            OreGradeType.MolybdenumConcentrate => 350.00m,
            _ => throw new UndefinedEnumValueException(nameof(oreGrade), oreGrade)
        };
    }

    private static string EnsureValidSupervisorUsername(string? supervisorUsername)
    {
        if (string.IsNullOrWhiteSpace(supervisorUsername)
            || supervisorUsername.Length is < SupervisorUsernameMinLength or > SupervisorUsernameMaxLength)
            throw new InvalidSupervisorUsernameException(nameof(supervisorUsername));

        return supervisorUsername;
    }

    private static string? EnsureValidFieldObservations(string? fieldObservations)
    {
        if (fieldObservations is { Length: > FieldObservationsMaxLength })
            throw new FieldObservationsTooLongException(fieldObservations.Length, FieldObservationsMaxLength,
                nameof(fieldObservations));

        return fieldObservations;
    }
}
