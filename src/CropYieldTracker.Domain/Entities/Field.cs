using CropYieldTracker.Domain.Lookups;

namespace CropYieldTracker.Domain.Entities;

public sealed class Field
{
    private readonly List<CropEntry> _crops = [];

    public Field(Guid id, string name, string county, decimal totalAcres, SoilTextureClass soilType, IrrigationPractice irrigationType)
    {
        Id = id;
        Name = name;
        County = county;
        TotalAcres = totalAcres;
        SoilType = soilType;
        IrrigationType = irrigationType;
    }

    public Guid Id { get; }
    public string Name { get; private set; }
    public string County { get; private set; }
    public decimal TotalAcres { get; private set; }
    public SoilTextureClass SoilType { get; private set; }
    public IrrigationPractice IrrigationType { get; private set; }
    public IReadOnlyList<CropEntry> Crops => _crops;
    public decimal ReportedCropAcres => _crops.Sum(crop => crop.Acres);

    public void UpdateDetails(string name, string county, decimal totalAcres, SoilTextureClass soilType, IrrigationPractice irrigationType)
    {
        Name = name;
        County = county;
        TotalAcres = totalAcres;
        SoilType = soilType;
        IrrigationType = irrigationType;
    }

    public void UpsertCrop(CropEntry crop)
    {
        var existingIndex = _crops.FindIndex(existing => existing.Id == crop.Id);
        if (existingIndex >= 0)
        {
            _crops[existingIndex] = crop;
            return;
        }

        _crops.Add(crop);
    }
}
