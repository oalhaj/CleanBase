using CleanBase.Generator.Application.DTOs;
using CleanBase.Generator.Domain.Entities;
using CleanBase.Generator.Domain.Rules;

namespace CleanBase.Generator.Application.Services;

public sealed class ConfigurationValidator
{
    private static readonly string[] AllowedDatabases = ["MongoDb", "SqlServer", "PostgreSql"];
    private static readonly string[] AllowedArchitectureStyles = ["Clean", "CleanCqrs"];

    public ValidationResultDto Validate(ProjectConfiguration configuration)
    {
        var errors = new List<string>();

        if (!NamingRules.IsValidProjectName(configuration.ProjectName))
            errors.Add("ProjectName must be PascalCase and 3-50 characters.");

        if (!NamingRules.IsValidSolutionName(configuration.SolutionName))
            errors.Add("SolutionName must be PascalCase/Dot notation and 3-100 characters.");

        if (!AllowedDatabases.Contains(configuration.DatabaseType))
            errors.Add("DatabaseType is invalid.");

        if (!AllowedArchitectureStyles.Contains(configuration.ArchitectureStyle))
            errors.Add("ArchitectureStyle is invalid.");

        if (configuration.UseMediatR && !configuration.UseCqrs)
            errors.Add("MediatR requires CQRS to be enabled.");

        if (configuration.IncludeScalar && !configuration.IncludeSwagger)
            errors.Add("Scalar requires Swagger to be enabled.");

        if (string.IsNullOrWhiteSpace(configuration.DestinationPath))
            errors.Add("DestinationPath is required.");

        return new ValidationResultDto
        {
            IsValid = errors.Count == 0,
            Errors = errors
        };
    }
}
