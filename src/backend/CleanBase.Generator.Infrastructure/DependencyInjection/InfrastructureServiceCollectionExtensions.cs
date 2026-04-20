using CleanBase.Generator.Application.Abstractions;
using CleanBase.Generator.Infrastructure.Generation;
using CleanBase.Generator.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CleanBase.Generator.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(MongoOptions.SectionName).Get<MongoOptions>() ?? new MongoOptions();
        services.AddSingleton(options);

        services.AddSingleton<IMongoClient>(_ => new MongoClient(options.ConnectionString));
        services.AddSingleton(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(options.DatabaseName));

        services.AddScoped<IProjectConfigurationRepository, MongoProjectConfigurationRepository>();
        services.AddScoped<IGenerationJobRepository, MongoGenerationJobRepository>();
        services.AddScoped<ITemplateRepository, MongoTemplateRepository>();
        services.AddScoped<ITemplateRenderer, FileSystemTemplateRenderer>();

        return services;
    }
}
