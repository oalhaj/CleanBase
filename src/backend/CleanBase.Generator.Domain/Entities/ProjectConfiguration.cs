namespace CleanBase.Generator.Domain.Entities;

public sealed class ProjectConfiguration
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string ProjectName { get; set; } = string.Empty;
    public string SolutionName { get; set; } = string.Empty;
    public string DatabaseType { get; set; } = "MongoDb";
    public string ArchitectureStyle { get; set; } = "Clean";
    public bool IncludeAuthentication { get; set; }
    public bool IncludeLogging { get; set; } = true;
    public bool IncludeSwagger { get; set; } = true;
    public bool IncludeScalar { get; set; }
    public bool IncludeDocker { get; set; }
    public bool IncludeUnitTests { get; set; }
    public bool UseCqrs { get; set; }
    public bool UseMediatR { get; set; }
    public bool UseFluentValidation { get; set; }
    public bool UseAutoMapper { get; set; }
    public bool UseRepositoryPattern { get; set; } = true;
    public bool UseUnitOfWork { get; set; }
    public string DotNetVersion { get; set; } = "net8.0";
    public string AngularVersion { get; set; } = "20";
    public string AngularArchitecture { get; set; } = "standalone-signals";
    public string AngularStyling { get; set; } = "scss";
    public string DestinationPath { get; set; } = string.Empty;
    public List<string> SampleModules { get; set; } = [];
}
