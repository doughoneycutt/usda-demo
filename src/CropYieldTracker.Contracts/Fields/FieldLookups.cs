using CropYieldTracker.Domain.Lookups;

namespace CropYieldTracker.Contracts.Fields;

public static class FieldLookups
{
    public static IReadOnlyList<SoilTextureClass> SoilTypes { get; } = Enum
        .GetValues<SoilTextureClass>()
        .Where(value => value != SoilTextureClass.Unknown)
        .ToArray();

    public static IReadOnlyList<IrrigationPractice> IrrigationTypes { get; } = Enum
        .GetValues<IrrigationPractice>()
        .Where(value => value != IrrigationPractice.Unknown)
        .ToArray();

    public static string GetSoilTypeLabel(SoilTextureClass soilType)
    {
        return soilType switch
        {
            SoilTextureClass.Sand => "Sand",
            SoilTextureClass.LoamySand => "Loamy sand",
            SoilTextureClass.SandyLoam => "Sandy loam",
            SoilTextureClass.Loam => "Loam",
            SoilTextureClass.SiltLoam => "Silt loam",
            SoilTextureClass.Silt => "Silt",
            SoilTextureClass.SandyClayLoam => "Sandy clay loam",
            SoilTextureClass.ClayLoam => "Clay loam",
            SoilTextureClass.SiltyClayLoam => "Silty clay loam",
            SoilTextureClass.SandyClay => "Sandy clay",
            SoilTextureClass.SiltyClay => "Silty clay",
            SoilTextureClass.Clay => "Clay",
            _ => "Unknown"
        };
    }

    public static string GetIrrigationTypeLabel(IrrigationPractice irrigationType)
    {
        return irrigationType switch
        {
            IrrigationPractice.Dryland => "Dryland / non-irrigated",
            IrrigationPractice.CenterPivot => "Center pivot sprinkler",
            IrrigationPractice.LinearMove => "Linear move sprinkler",
            IrrigationPractice.WheelLine => "Wheel line sprinkler",
            IrrigationPractice.SolidSet => "Solid set sprinkler",
            IrrigationPractice.SurfaceFurrow => "Surface furrow",
            IrrigationPractice.SurfaceBorder => "Surface border",
            IrrigationPractice.SurfaceBasin => "Surface basin",
            IrrigationPractice.SurfaceDrip => "Surface drip",
            IrrigationPractice.SubsurfaceDrip => "Subsurface drip",
            IrrigationPractice.MicroSprinkler => "Micro-sprinkler",
            _ => "Unknown"
        };
    }
}
