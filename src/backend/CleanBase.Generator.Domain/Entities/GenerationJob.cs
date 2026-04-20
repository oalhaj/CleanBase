using CleanBase.Generator.Domain.Enums;

namespace CleanBase.Generator.Domain.Entities;

public sealed class GenerationJob
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string ConfigurationId { get; set; } = string.Empty;
    public GenerationJobStatus Status { get; set; } = GenerationJobStatus.Pending;
    public string? Summary { get; set; }
    public string? Error { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? StartedUtc { get; set; }
    public DateTime? CompletedUtc { get; set; }
}
