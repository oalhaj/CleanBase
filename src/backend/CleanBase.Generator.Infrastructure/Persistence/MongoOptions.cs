namespace CleanBase.Generator.Infrastructure.Persistence;

public sealed class MongoOptions
{
    public const string SectionName = "Mongo";
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string DatabaseName { get; set; } = "cleanbase_generator";
}
