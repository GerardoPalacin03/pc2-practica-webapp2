using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Exceptions;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.ValueObjects;

/// <summary>
///     Value object that represents the weight of a mineral cargo.
/// </summary>
/// <remarks>
///     The tons must be strictly greater than zero and the unit cannot be null or blank. When the unit is not
///     specified, it is <see cref="DefaultUnit" /> (metric tons).
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public record MineralWeight
{
    /// <summary>
    ///     The unit used when none is specified: metric tons.
    /// </summary>
    public const string DefaultUnit = "MT";

    /// <summary>
    ///     Initializes a new instance of the <see cref="MineralWeight" /> value object.
    /// </summary>
    /// <param name="tons">The weight in tons. It must be strictly greater than zero.</param>
    /// <param name="unit">The unit of the weight. It must not be null or blank. By default, <c>MT</c>.</param>
    /// <exception cref="NonPositiveTonsException">Thrown when <paramref name="tons" /> is lower than or equal to zero.</exception>
    /// <exception cref="BlankWeightUnitException">Thrown when <paramref name="unit" /> is null or blank.</exception>
    public MineralWeight(decimal tons, string unit = DefaultUnit)
    {
        if (tons <= decimal.Zero) throw new NonPositiveTonsException(tons, nameof(tons));
        if (string.IsNullOrWhiteSpace(unit)) throw new BlankWeightUnitException(nameof(unit));

        Tons = tons;
        Unit = unit;
    }

    /// <summary>
    ///     Gets the weight in tons.
    /// </summary>
    public decimal Tons { get; private set; }

    /// <summary>
    ///     Gets the unit of the weight.
    /// </summary>
    public string Unit { get; private set; }

    /// <summary>
    ///     Calculates the estimated cost of hauling the cargo.
    /// </summary>
    /// <param name="ratePerTon">The cost of hauling one ton. It must be greater than or equal to zero.</param>
    /// <returns>The estimated haulage fee, rounded to 2 decimals.</returns>
    /// <exception cref="NegativeHaulageRateException">Thrown when <paramref name="ratePerTon" /> is lower than zero.</exception>
    public decimal CalculateHaulageFee(decimal ratePerTon)
    {
        if (ratePerTon < decimal.Zero) throw new NegativeHaulageRateException(ratePerTon, nameof(ratePerTon));

        return Math.Round(Tons * ratePerTon, 2);
    }
}
