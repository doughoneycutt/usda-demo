using System.ComponentModel.DataAnnotations;

namespace CropYieldTracker.Contracts.Fields;

public sealed class SaveCropRequest
{
    public Guid? Id { get; set; }

    [Required]
    public Guid FieldId { get; set; }

    [Required]
    public string CropName { get; set; } = string.Empty;

    [Range(1, 100000)]
    public decimal Acres { get; set; }

    [Range(0.01, 100000)]
    public decimal YieldPerAcre { get; set; }

    [Required]
    public string Unit { get; set; } = "bu";
}
