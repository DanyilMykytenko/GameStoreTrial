using Business.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers;

/// <summary>
/// PaggingOptions API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PaggingOptionsController : ControllerBase
{
    [HttpGet("options")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetPaymentOptions()
    {
        var options = PaggingOptionsService.Options;
        return Ok(options);
    }
}