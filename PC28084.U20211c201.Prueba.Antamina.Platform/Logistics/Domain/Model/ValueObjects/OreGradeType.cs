namespace PC28084.U20211c201.Prueba.Antamina.Platform.Logistics.Domain.Model.ValueObjects;

/// <summary>
///     Represents the type of mineral concentrate that a truck hauls.
/// </summary>
/// <remarks>
///     The maximum cargo weight allowed depends on the grade (see the capacity rule of the ore dispatch ticket).
///     NRC8084 - 20211c201 - Apellidos, Nombres
/// </remarks>
public enum OreGradeType
{
    /// <summary>
    ///     Copper concentrate.
    /// </summary>
    CopperConcentrate = 1,

    /// <summary>
    ///     Zinc concentrate.
    /// </summary>
    ZincConcentrate = 2,

    /// <summary>
    ///     Molybdenum concentrate.
    /// </summary>
    MolybdenumConcentrate = 3
}
