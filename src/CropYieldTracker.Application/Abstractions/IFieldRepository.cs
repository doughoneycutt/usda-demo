using CropYieldTracker.Domain.Entities;

namespace CropYieldTracker.Application.Abstractions;

public interface IFieldRepository
{
    Task<IReadOnlyList<Field>> GetFieldsAsync(CancellationToken cancellationToken = default);
    Task<Field?> GetFieldAsync(Guid fieldId, CancellationToken cancellationToken = default);
    Task SaveFieldAsync(Field field, CancellationToken cancellationToken = default);
    Task SaveCropAsync(CropEntry crop, CancellationToken cancellationToken = default);
}
