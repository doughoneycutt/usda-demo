using CropYieldTracker.Application.Abstractions;
using CropYieldTracker.Contracts.Fields;
using CropYieldTracker.Domain.Entities;

namespace CropYieldTracker.Application.Services;

public sealed class FieldService(IFieldRepository repository) : IFieldService
{
    public async Task<IReadOnlyList<FieldDto>> GetFieldsAsync(CancellationToken cancellationToken = default)
    {
        var fields = await repository.GetFieldsAsync(cancellationToken);
        return fields.OrderBy(field => field.Name).Select(FieldDto.FromDomain).ToArray();
    }

    public async Task<FieldDto?> GetFieldAsync(Guid fieldId, CancellationToken cancellationToken = default)
    {
        var field = await repository.GetFieldAsync(fieldId, cancellationToken);
        return field is null ? null : FieldDto.FromDomain(field);
    }

    public async Task<IReadOnlyList<CropDto>> GetCropsAsync(CancellationToken cancellationToken = default)
    {
        var fields = await repository.GetFieldsAsync(cancellationToken);
        return fields
            .SelectMany(field => field.Crops.Select(crop => CropDto.FromDomain(crop, field.Name)))
            .OrderBy(crop => crop.FieldName)
            .ThenBy(crop => crop.CropName)
            .ToArray();
    }

    public async Task<IReadOnlyList<FieldReportDto>> GetFieldReportsAsync(CancellationToken cancellationToken = default)
    {
        var fields = await repository.GetFieldsAsync(cancellationToken);
        return fields
            .Select(field =>
            {
                var totalProduction = field.Crops.Sum(crop => crop.TotalProduction);
                return new FieldReportDto
                {
                    FieldId = field.Id,
                    FieldName = field.Name,
                    TotalProduction = totalProduction,
                    Crops = field.Crops
                        .OrderByDescending(crop => crop.TotalProduction)
                        .Select(crop => new CropReportDto
                        {
                            CropName = crop.CropName,
                            TotalProduction = crop.TotalProduction,
                            Unit = crop.Unit,
                            PercentOfField = totalProduction == 0 ? 0 : Math.Round((crop.TotalProduction / totalProduction) * 100, 1)
                        })
                        .ToArray()
                };
            })
            .OrderBy(report => report.FieldName)
            .ToArray();
    }

    public async Task<FieldDto> SaveFieldAsync(SaveFieldRequest request, CancellationToken cancellationToken = default)
    {
        var existingField = request.Id.HasValue ? await repository.GetFieldAsync(request.Id.Value, cancellationToken) : null;
        var field = existingField ?? new Field(
            request.Id ?? Guid.NewGuid(),
            request.Name.Trim(),
            request.County.Trim(),
            request.TotalAcres,
            request.SoilType,
            request.IrrigationType);

        field.UpdateDetails(
            request.Name.Trim(),
            request.County.Trim(),
            request.TotalAcres,
            request.SoilType,
            request.IrrigationType);

        await repository.SaveFieldAsync(field, cancellationToken);
        return FieldDto.FromDomain(field);
    }

    public async Task<CropDto> SaveCropAsync(SaveCropRequest request, CancellationToken cancellationToken = default)
    {
        var field = await repository.GetFieldAsync(request.FieldId, cancellationToken)
            ?? throw new InvalidOperationException("Field was not found.");

        var existingCrop = field.Crops.FirstOrDefault(crop => crop.Id == request.Id);
        var crop = existingCrop ?? new CropEntry(
            request.Id ?? Guid.NewGuid(),
            request.FieldId,
            request.CropName.Trim(),
            request.Acres,
            request.YieldPerAcre,
            request.Unit.Trim());

        crop.Update(request.CropName.Trim(), request.Acres, request.YieldPerAcre, request.Unit.Trim());

        await repository.SaveCropAsync(crop, cancellationToken);
        return CropDto.FromDomain(crop, field.Name);
    }
}
