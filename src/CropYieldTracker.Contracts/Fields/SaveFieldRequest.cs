using System.ComponentModel.DataAnnotations;
using CropYieldTracker.Domain.Lookups;

namespace CropYieldTracker.Contracts.Fields;

public sealed class SaveFieldRequest
{
    public Guid? Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string County { get; set; } = string.Empty;

    [Range(1, 100000)]
    public decimal TotalAcres { get; set; }

    [EnumDataType(typeof(SoilTextureClass))]
    public SoilTextureClass SoilType { get; set; }

    [EnumDataType(typeof(IrrigationPractice))]
    public IrrigationPractice IrrigationType { get; set; }
}
