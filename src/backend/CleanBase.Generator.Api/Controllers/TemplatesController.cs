using CleanBase.Generator.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CleanBase.Generator.Api.Controllers;

[ApiController]
[Route("api/templates")]
public sealed class TemplatesController(ITemplateRepository templateRepository) : ControllerBase
{
    [HttpGet("active")]
    public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
    {
        var templates = await templateRepository.GetActiveTemplatesAsync(cancellationToken);
        return Ok(templates);
    }
}
