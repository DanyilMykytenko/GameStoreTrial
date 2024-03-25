using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

/// <summary>
/// Seeding controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public class SeedController
{
    private readonly ISeedService _seedService;

    public SeedController(ISeedService seedService)
    {
        _seedService = seedService;
    }

    [HttpGet]
    public async Task SeedData()
    {
        await _seedService.SeedData();
    }
}
