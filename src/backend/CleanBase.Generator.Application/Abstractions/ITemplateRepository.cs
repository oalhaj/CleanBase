using CleanBase.Generator.Domain.Entities;

namespace CleanBase.Generator.Application.Abstractions;

public interface ITemplateRepository
{
    Task<IReadOnlyCollection<TemplateDefinition>> GetActiveTemplatesAsync(CancellationToken cancellationToken);
}
