using CropYieldTracker.Domain.Entities;

namespace CropYieldTracker.Contracts.Fields;

public sealed class CropDto
{
    public Guid Id { get; init; }
    public Guid FieldId { get; init; }
    public string FieldName { get; init; } = string.Empty;
    public string CropName { get; init; } = string.Empty;
    public decimal Acres { get; init; }
    public decimal YieldPerAcre { get; init; }
    public string Unit { get; init; } = string.Empty;
    public decimal TotalProduction { get; init; }

    public static CropDto FromDomain(CropEntry crop, string fieldName = "")
    {
        return new CropDto
        {
            Id = crop.Id,
            FieldId = crop.FieldId,
            FieldName = fieldName,
            CropName = crop.CropName,
            Acres = crop.Acres,
            YieldPerAcre = crop.YieldPerAcre,
            Unit = crop.Unit,
            TotalProduction = crop.TotalProduction
        };
    }
}
