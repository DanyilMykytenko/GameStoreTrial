using Business.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers;

/// <summary>
/// SortingOptions API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class SortingOptionsController : ControllerBase
{
    [HttpGet("options")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetPaymentOptions()
    {
        var options = SortingOptionsService.Options;
        return Ok(options);
    }
}
