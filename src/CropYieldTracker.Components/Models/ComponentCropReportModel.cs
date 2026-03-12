namespace CropYieldTracker.Components.Models;

public sealed class ComponentCropReportModel
{
    public string CropName { get; init; } = string.Empty;
    public decimal TotalProduction { get; init; }
    public string Unit { get; init; } = string.Empty;
    public decimal PercentOfField { get; init; }
}
