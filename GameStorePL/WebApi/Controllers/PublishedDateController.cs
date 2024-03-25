using Business.Services;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers;

/// <summary>
/// PublishedDate API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PublishedDateController : ControllerBase
{
    [HttpGet("options")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetPaymentOptions()
    {
        var options = DateTimeDurationOptionsService.Options;
        return Ok(options);
    }
}
