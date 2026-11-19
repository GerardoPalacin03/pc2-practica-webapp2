using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Exceptions;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;

/// <summary>
///     Value object that represents the operational shift in which a dispatch takes place.
/// </summary>
/// <remarks>
///     It is made of a dispatch date and a shift code. The shift code only accepts <c>DAY-A</c> or <c>NIGHT-B</c>
///     (case insensitive) and it is always stored in upper case.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public record ShiftWindow
{
    /// <summary>
    ///     The shift code of the day shift.
    /// </summary>
    public const string DayShiftCode = "DAY-A";

    /// <summary>
    ///     The shift code of the night shift.
    /// </summary>
    public const string NightShiftCode = "NIGHT-B";

    /// <summary>
    ///     Initializes a new instance of the <see cref="ShiftWindow" /> value object.
    /// </summary>
    /// <param name="dispatchDate">The date of the dispatch.</param>
    /// <param name="shiftCode">The code of the shift. It must be <c>DAY-A</c> or <c>NIGHT-B</c>, in any case.</param>
    /// <exception cref="InvalidShiftCodeException">
    ///     Thrown when <paramref name="shiftCode" /> is null, blank or is not one of the accepted codes.
    /// </exception>
    public ShiftWindow(DateOnly dispatchDate, string? shiftCode)
    {
        if (string.IsNullOrWhiteSpace(shiftCode)) throw new InvalidShiftCodeException(nameof(shiftCode));

        var normalizedShiftCode = shiftCode.Trim().ToUpperInvariant();
        if (normalizedShiftCode is not (DayShiftCode or NightShiftCode))
            throw new InvalidShiftCodeException(nameof(shiftCode));

        DispatchDate = dispatchDate;
        ShiftCode = normalizedShiftCode;
    }

    /// <summary>
    ///     Gets the date of the dispatch.
    /// </summary>
    public DateOnly DispatchDate { get; private set; }

    /// <summary>
    ///     Gets the code of the shift, in upper case.
    /// </summary>
    public string ShiftCode { get; private set; }

    /// <summary>
    ///     Indicates whether the shift is the night shift.
    /// </summary>
    /// <returns><c>true</c> when the shift code is <c>NIGHT-B</c>; otherwise, <c>false</c>.</returns>
    public bool IsNightShift()
    {
        return ShiftCode == NightShiftCode;
    }
}
