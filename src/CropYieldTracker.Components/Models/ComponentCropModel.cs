namespace CropYieldTracker.Components.Models;

public sealed class ComponentCropModel
{
    public Guid Id { get; init; }
    public Guid FieldId { get; init; }
    public string FieldName { get; init; } = string.Empty;
    public string CropName { get; init; } = string.Empty;
    public decimal Acres { get; init; }
    public decimal YieldPerAcre { get; init; }
    public string Unit { get; init; } = string.Empty;
    public decimal TotalProduction { get; init; }
}
