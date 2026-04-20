using CleanBase.Generator.Domain.Entities;

namespace CleanBase.Generator.Application.Abstractions;

public interface IGenerationJobRepository
{
    Task CreateAsync(GenerationJob job, CancellationToken cancellationToken);
    Task<GenerationJob?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task UpdateAsync(GenerationJob job, CancellationToken cancellationToken);
}
