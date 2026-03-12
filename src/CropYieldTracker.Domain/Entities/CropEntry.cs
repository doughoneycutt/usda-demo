namespace CropYieldTracker.Domain.Entities;

public sealed class CropEntry
{
    public CropEntry(Guid id, Guid fieldId, string cropName, decimal acres, decimal yieldPerAcre, string unit)
    {
        Id = id;
        FieldId = fieldId;
        CropName = cropName;
        Acres = acres;
        YieldPerAcre = yieldPerAcre;
        Unit = unit;
    }

    public Guid Id { get; }
    public Guid FieldId { get; }
    public string CropName { get; private set; }
    public decimal Acres { get; private set; }
    public decimal YieldPerAcre { get; private set; }
    public string Unit { get; private set; }
    public decimal TotalProduction => Math.Round(Acres * YieldPerAcre, 2);

    public void Update(string cropName, decimal acres, decimal yieldPerAcre, string unit)
    {
        CropName = cropName;
        Acres = acres;
        YieldPerAcre = yieldPerAcre;
        Unit = unit;
    }
}
