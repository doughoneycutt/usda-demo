namespace CropYieldTracker.Components.Models;

public sealed class ComponentFieldReportModel
{
    public Guid FieldId { get; init; }
    public string FieldName { get; init; } = string.Empty;
    public decimal TotalProduction { get; init; }
    public IReadOnlyList<ComponentCropReportModel> Crops { get; init; } = [];
}
