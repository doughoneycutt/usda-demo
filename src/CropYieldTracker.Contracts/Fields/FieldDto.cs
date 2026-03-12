using CropYieldTracker.Domain.Entities;
using CropYieldTracker.Domain.Lookups;

namespace CropYieldTracker.Contracts.Fields;

public sealed class FieldDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string County { get; init; } = string.Empty;
    public decimal TotalAcres { get; init; }
    public SoilTextureClass SoilType { get; init; }
    public IrrigationPractice IrrigationType { get; init; }
    public decimal ReportedCropAcres { get; init; }
    public IReadOnlyList<CropDto> Crops { get; init; } = [];

    public static FieldDto FromDomain(Field field)
    {
        return new FieldDto
        {
            Id = field.Id,
            Name = field.Name,
            County = field.County,
            TotalAcres = field.TotalAcres,
            SoilType = field.SoilType,
            IrrigationType = field.IrrigationType,
            ReportedCropAcres = field.ReportedCropAcres,
            Crops = field.Crops.Select(crop => CropDto.FromDomain(crop)).ToArray()
        };
    }
}
