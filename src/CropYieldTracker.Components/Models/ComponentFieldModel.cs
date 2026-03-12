namespace CropYieldTracker.Components.Models;

public sealed class ComponentFieldModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string County { get; init; } = string.Empty;
    public decimal TotalAcres { get; init; }
    public string SoilType { get; init; } = string.Empty;
    public string IrrigationType { get; init; } = string.Empty;
    public decimal ReportedCropAcres { get; init; }
    public IReadOnlyList<ComponentCropModel> Crops { get; init; } = [];
}
