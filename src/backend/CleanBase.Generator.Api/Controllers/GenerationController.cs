using CleanBase.Generator.Api.Contracts;
using CleanBase.Generator.Application.Services;
using CleanBase.Generator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanBase.Generator.Api.Controllers;

[ApiController]
[Route("api/generation")]
public sealed class GenerationController(GenerationOrchestrator orchestrator) : ControllerBase
{
    [HttpPost("validate")]
    public async Task<IActionResult> Validate([FromBody] ProjectConfiguration configuration, CancellationToken cancellationToken)
    {
        var result = await orchestrator.ValidateAsync(configuration, cancellationToken);
        return result.IsValid ? Ok(result) : BadRequest(result);
    }

    [HttpPost("preview")]
    public async Task<IActionResult> Preview([FromBody] ProjectConfiguration configuration, CancellationToken cancellationToken)
    {
        var validation = await orchestrator.ValidateAsync(configuration, cancellationToken);
        if (!validation.IsValid)
        {
            return BadRequest(validation);
        }

        var preview = await orchestrator.PreviewAsync(configuration, cancellationToken);
        return Ok(preview);
    }

    [HttpPost]
    public async Task<IActionResult> Generate([FromBody] GenerateRequest request, CancellationToken cancellationToken)
    {
        var result = await orchestrator.GenerateAsync(request.Configuration, request.RunInBackground, cancellationToken);
        return string.IsNullOrWhiteSpace(result.JobId) ? BadRequest(result) : Accepted(result);
    }

    [HttpGet("jobs/{jobId}")]
    public async Task<IActionResult> GetJobStatus(string jobId, CancellationToken cancellationToken)
    {
        var job = await orchestrator.GetJobAsync(jobId, cancellationToken);
        return job is null ? NotFound() : Ok(job);
    }
}
