using CleanBase.Generator.Application.Abstractions;
using CleanBase.Generator.Application.DTOs;
using CleanBase.Generator.Domain.Entities;
using CleanBase.Generator.Domain.Enums;

namespace CleanBase.Generator.Application.Services;

public sealed class GenerationOrchestrator(
    ConfigurationValidator validator,
    IProjectConfigurationRepository configurationRepository,
    IGenerationJobRepository jobRepository,
    ITemplateRenderer renderer)
{
    public async Task<ValidationResultDto> ValidateAsync(ProjectConfiguration configuration, CancellationToken cancellationToken)
        => await Task.FromResult(validator.Validate(configuration));

    public async Task<IReadOnlyCollection<string>> PreviewAsync(ProjectConfiguration configuration, CancellationToken cancellationToken)
        => await renderer.BuildPreviewPathsAsync(configuration, cancellationToken);

    public async Task<GenerateResultDto> GenerateAsync(ProjectConfiguration configuration, bool runInBackground, CancellationToken cancellationToken)
    {
        var validation = validator.Validate(configuration);
        if (!validation.IsValid)
        {
            return new GenerateResultDto
            {
                JobId = string.Empty,
                Status = GenerationJobStatus.Failed.ToString(),
                Message = string.Join(" | ", validation.Errors)
            };
        }

        await configurationRepository.SaveAsync(configuration, cancellationToken);

        var job = new GenerationJob
        {
            ConfigurationId = configuration.Id,
            Status = runInBackground ? GenerationJobStatus.Pending : GenerationJobStatus.Running
        };

        await jobRepository.CreateAsync(job, cancellationToken);

        if (runInBackground)
        {
            _ = Task.Run(async () => await ExecuteGenerationJobAsync(job, configuration, CancellationToken.None), CancellationToken.None);
            return new GenerateResultDto { JobId = job.Id, Status = job.Status.ToString(), Message = "Generation queued." };
        }

        await ExecuteGenerationJobAsync(job, configuration, cancellationToken);
        return new GenerateResultDto { JobId = job.Id, Status = job.Status.ToString(), Message = job.Summary ?? string.Empty };
    }

    public async Task<GenerationJob?> GetJobAsync(string id, CancellationToken cancellationToken)
        => await jobRepository.GetByIdAsync(id, cancellationToken);

    private async Task ExecuteGenerationJobAsync(GenerationJob job, ProjectConfiguration configuration, CancellationToken cancellationToken)
    {
        try
        {
            job.Status = GenerationJobStatus.Running;
            job.StartedUtc = DateTime.UtcNow;
            await jobRepository.UpdateAsync(job, cancellationToken);

            await renderer.RenderAsync(configuration, configuration.DestinationPath, cancellationToken);

            job.Status = GenerationJobStatus.Completed;
            job.CompletedUtc = DateTime.UtcNow;
            job.Summary = $"Generated solution '{configuration.SolutionName}' at '{configuration.DestinationPath}'.";
            await jobRepository.UpdateAsync(job, cancellationToken);
        }
        catch (Exception ex)
        {
            job.Status = GenerationJobStatus.Failed;
            job.CompletedUtc = DateTime.UtcNow;
            job.Error = ex.Message;
            await jobRepository.UpdateAsync(job, cancellationToken);
        }
    }
}
