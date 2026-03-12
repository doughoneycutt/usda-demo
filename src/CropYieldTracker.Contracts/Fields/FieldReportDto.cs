namespace CropYieldTracker.Contracts.Fields;

public sealed class FieldReportDto
{
    public Guid FieldId { get; init; }
    public string FieldName { get; init; } = string.Empty;
    public decimal TotalProduction { get; init; }
    public IReadOnlyList<CropReportDto> Crops { get; init; } = [];
}
