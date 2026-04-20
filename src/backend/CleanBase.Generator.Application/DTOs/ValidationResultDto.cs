namespace CleanBase.Generator.Application.DTOs;

public sealed class ValidationResultDto
{
    public bool IsValid { get; set; }
    public IReadOnlyCollection<string> Errors { get; set; } = [];
}
