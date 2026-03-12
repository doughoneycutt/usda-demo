using CropYieldTracker.Contracts.Fields;

namespace CropYieldTracker.Application.Abstractions;

public interface IFieldService
{
    Task<IReadOnlyList<FieldDto>> GetFieldsAsync(CancellationToken cancellationToken = default);
    Task<FieldDto?> GetFieldAsync(Guid fieldId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CropDto>> GetCropsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FieldReportDto>> GetFieldReportsAsync(CancellationToken cancellationToken = default);
    Task<FieldDto> SaveFieldAsync(SaveFieldRequest request, CancellationToken cancellationToken = default);
    Task<CropDto> SaveCropAsync(SaveCropRequest request, CancellationToken cancellationToken = default);
}
