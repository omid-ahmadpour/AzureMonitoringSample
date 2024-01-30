using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace MonitoringExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly Counter<int> matchesCreatedCounter;

    public UserController(Meter meter)
    {
        this.matchesCreatedCounter = meter.CreateCounter<int>("matches_created");
    }

    [HttpGet("hello")]
    public IActionResult Hello()
    {
        return Ok("Hello, ASP.NET Core API!");
    }

    [HttpPost("createMatch")]
    public IActionResult CreateMatch()
    {
        var matchId = Guid.NewGuid();

        matchesCreatedCounter.Add(1);

        return Ok($"Match created with ID: {matchId}");
    }
}