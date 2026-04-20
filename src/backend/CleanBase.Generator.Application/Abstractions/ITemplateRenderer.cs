using CleanBase.Generator.Domain.Entities;

namespace CleanBase.Generator.Application.Abstractions;

public interface ITemplateRenderer
{
    Task RenderAsync(ProjectConfiguration configuration, string destinationPath, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<string>> BuildPreviewPathsAsync(ProjectConfiguration configuration, CancellationToken cancellationToken);
}
