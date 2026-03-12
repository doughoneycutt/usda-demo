namespace CropYieldTracker.Contracts.Fields;

public sealed class CropReportDto
{
    public string CropName { get; init; } = string.Empty;
    public decimal TotalProduction { get; init; }
    public string Unit { get; init; } = string.Empty;
    public decimal PercentOfField { get; init; }
}
