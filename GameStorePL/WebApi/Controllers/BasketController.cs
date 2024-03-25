using AutoMapper;
using Business.Interfaces;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;
using WebApi.Models;

namespace WebApi.Controllers;

/// <summary>
/// Basket API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly IMapper _mapper;

    public BasketController(IBasketService basketService, IMapper mapper)
    {
        _basketService = basketService;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all games.
    /// </summary>
    /// <returns>Return array of games.</returns>
    [HttpGet]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var busket = await _basketService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<OrderDetailsDTO>>(busket.First().Details));
    }
}