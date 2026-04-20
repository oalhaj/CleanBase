using CleanBase.Generator.Domain.Entities;

namespace CleanBase.Generator.Api.Contracts;

public sealed class GenerateRequest
{
    public required ProjectConfiguration Configuration { get; set; }
    public bool RunInBackground { get; set; } = true;
}
