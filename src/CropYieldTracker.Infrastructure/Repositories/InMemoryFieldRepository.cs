using CropYieldTracker.Application.Abstractions;
using CropYieldTracker.Domain.Entities;
using CropYieldTracker.Domain.Lookups;

namespace CropYieldTracker.Infrastructure.Repositories;

public sealed class InMemoryFieldRepository : IFieldRepository
{
    private readonly List<Field> _fields = SeedFields();
    private readonly Lock _lock = new();

    public Task<IReadOnlyList<Field>> GetFieldsAsync(CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            return Task.FromResult<IReadOnlyList<Field>>(_fields.Select(CloneField).ToArray());
        }
    }

    public Task<Field?> GetFieldAsync(Guid fieldId, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var field = _fields.FirstOrDefault(existing => existing.Id == fieldId);
            return Task.FromResult(field is null ? null : CloneField(field));
        }
    }

    public Task SaveFieldAsync(Field field, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var existingIndex = _fields.FindIndex(existing => existing.Id == field.Id);
            if (existingIndex >= 0)
            {
                _fields[existingIndex].UpdateDetails(field.Name, field.County, field.TotalAcres, field.SoilType, field.IrrigationType);
            }
            else
            {
                _fields.Add(CloneField(field));
            }

            return Task.CompletedTask;
        }
    }

    public Task SaveCropAsync(CropEntry crop, CancellationToken cancellationToken = default)
    {
        lock (_lock)
        {
            var field = _fields.FirstOrDefault(existing => existing.Id == crop.FieldId)
                ?? throw new InvalidOperationException("Field was not found.");

            field.UpsertCrop(CloneCrop(crop));
            return Task.CompletedTask;
        }
    }

    private static Field CloneField(Field source)
    {
        var field = new Field(source.Id, source.Name, source.County, source.TotalAcres, source.SoilType, source.IrrigationType);
        foreach (var crop in source.Crops)
        {
            field.UpsertCrop(CloneCrop(crop));
        }

        return field;
    }

    private static CropEntry CloneCrop(CropEntry source)
    {
        return new CropEntry(source.Id, source.FieldId, source.CropName, source.Acres, source.YieldPerAcre, source.Unit);
    }

    private static List<Field> SeedFields()
    {
        var northField = new Field(Guid.Parse("76D4C1AF-A8A4-44E4-A60E-FC9A9DAA2901"), "North Pivot", "Weld County", 160, SoilTextureClass.Loam, IrrigationPractice.CenterPivot);
        northField.UpsertCrop(new CropEntry(Guid.Parse("3C042B76-5A4D-46BA-A1A6-2CE78F3A6801"), northField.Id, "Corn", 100, 175, "bu"));
        northField.UpsertCrop(new CropEntry(Guid.Parse("A6B616A8-2C77-4DA2-A76D-E95339AFC5E1"), northField.Id, "Soybean", 40, 55, "bu"));

        var southField = new Field(Guid.Parse("4D098B56-3807-4854-9F6F-DAB8D09E8A91"), "South Bench", "Logan County", 120, SoilTextureClass.SiltLoam, IrrigationPractice.Dryland);
        southField.UpsertCrop(new CropEntry(Guid.Parse("1E73BFC4-3A9D-4024-9180-8F2A2A31CC11"), southField.Id, "Wheat", 75, 68, "bu"));
        southField.UpsertCrop(new CropEntry(Guid.Parse("0B4D8EF2-4B33-4DE0-9FA6-2859457DFA61"), southField.Id, "Sunflower", 30, 22, "cwt"));

        return [northField, southField];
    }
}
