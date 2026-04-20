using CleanBase.Generator.Domain.Entities;

namespace CleanBase.Generator.Application.Abstractions;

public interface IProjectConfigurationRepository
{
    Task SaveAsync(ProjectConfiguration configuration, CancellationToken cancellationToken);
    Task<ProjectConfiguration?> GetByIdAsync(string id, CancellationToken cancellationToken);
}
