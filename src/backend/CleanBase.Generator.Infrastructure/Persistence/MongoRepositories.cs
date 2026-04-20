using CleanBase.Generator.Application.Abstractions;
using CleanBase.Generator.Domain.Entities;
using MongoDB.Driver;

namespace CleanBase.Generator.Infrastructure.Persistence;

public sealed class MongoProjectConfigurationRepository(IMongoDatabase database) : IProjectConfigurationRepository
{
    private readonly IMongoCollection<ProjectConfiguration> _collection = database.GetCollection<ProjectConfiguration>("project_configurations");

    public async Task SaveAsync(ProjectConfiguration configuration, CancellationToken cancellationToken)
        => await _collection.ReplaceOneAsync(x => x.Id == configuration.Id, configuration, new ReplaceOptions { IsUpsert = true }, cancellationToken);

    public async Task<ProjectConfiguration?> GetByIdAsync(string id, CancellationToken cancellationToken)
        => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
}

public sealed class MongoGenerationJobRepository(IMongoDatabase database) : IGenerationJobRepository
{
    private readonly IMongoCollection<GenerationJob> _collection = database.GetCollection<GenerationJob>("generation_jobs");

    public async Task CreateAsync(GenerationJob job, CancellationToken cancellationToken)
        => await _collection.InsertOneAsync(job, cancellationToken: cancellationToken);

    public async Task<GenerationJob?> GetByIdAsync(string id, CancellationToken cancellationToken)
        => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task UpdateAsync(GenerationJob job, CancellationToken cancellationToken)
        => await _collection.ReplaceOneAsync(x => x.Id == job.Id, job, new ReplaceOptions { IsUpsert = true }, cancellationToken);
}

public sealed class MongoTemplateRepository(IMongoDatabase database) : ITemplateRepository
{
    private readonly IMongoCollection<TemplateDefinition> _collection = database.GetCollection<TemplateDefinition>("templates");

    public async Task<IReadOnlyCollection<TemplateDefinition>> GetActiveTemplatesAsync(CancellationToken cancellationToken)
        => await _collection.Find(x => x.IsActive).ToListAsync(cancellationToken);
}
