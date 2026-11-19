using System.Text.RegularExpressions;
using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Exceptions;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;

/// <summary>
///     Value object that identifies a haul truck of the mine.
/// </summary>
/// <remarks>
///     The code must be <c>CAT-797F-XXX</c> or <c>KOM-930E-XXX</c>, where <c>XXX</c> is a correlative number of 3
///     digits between 001 and 999. The value 000 is forbidden.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public readonly record struct HaulTruckCode
{
    private static readonly Regex FormatPattern =
        new("^(CAT-797F|KOM-930E)-(?<correlative>[0-9]{3})$", RegexOptions.CultureInvariant);

    private const string ForbiddenCorrelative = "000";

    /// <summary>
    ///     Initializes a new instance of the <see cref="HaulTruckCode" /> value object.
    /// </summary>
    /// <param name="value">The code of the truck. It must follow the format described in the remarks.</param>
    /// <exception cref="InvalidHaulTruckCodeException">
    ///     Thrown when <paramref name="value" /> is null or does not follow the required format.
    /// </exception>
    public HaulTruckCode(string? value)
    {
        if (value is null) throw new InvalidHaulTruckCodeException(nameof(value));

        var match = FormatPattern.Match(value);
        if (!match.Success || match.Groups["correlative"].Value == ForbiddenCorrelative)
            throw new InvalidHaulTruckCodeException(nameof(value));

        Value = value;
    }

    /// <summary>
    ///     Gets the code of the truck.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Value;
    }
}
