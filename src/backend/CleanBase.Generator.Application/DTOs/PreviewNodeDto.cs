namespace CleanBase.Generator.Application.DTOs;

public sealed class PreviewNodeDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsDirectory { get; set; }
    public List<PreviewNodeDto> Children { get; set; } = [];
}
