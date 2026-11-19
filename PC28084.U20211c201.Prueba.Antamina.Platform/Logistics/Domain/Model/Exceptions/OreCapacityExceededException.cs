using PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;
using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Exceptions;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.Exceptions;

/// <summary>
///     Exception thrown when the cargo weight exceeds the maximum capacity allowed for the ore grade.
/// </summary>
/// <param name="oreGrade">The ore grade of the cargo.</param>
/// <param name="maxTons">The maximum number of tons allowed for the ore grade.</param>
/// <param name="actualTons">The number of tons that was received.</param>
/// <param name="paramName">The name of the argument that violates the rule.</param>
/// <remarks>
///     It is an <see cref="ArgumentOutOfRangeException" /> that also exposes a resource key, so the response can be
///     translated according to the request culture.
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public sealed class OreCapacityExceededException(
    OreGradeType oreGrade,
    decimal maxTons,
    decimal actualTons,
    string paramName)
    : ArgumentOutOfRangeException(paramName, actualTons,
            $"The cargo weight of {actualTons} tons exceeds the maximum of {maxTons} tons allowed for {oreGrade}."),
        ILocalizableException
{
    /// <inheritdoc />
    public string ResourceKey => "Validation.OreCapacityExceeded";
}
