// <copyright file="PlatformsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;
using WebApi.Models;
using WebApi.RequestModels;

namespace WebApi.Controllers;

/// <summary>
/// Platforms API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformService _platformService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlatformsController"/> class.
    /// Constructor..
    /// </summary>
    /// <param name="platformService">Dependency platform service.</param>
    public PlatformsController(IPlatformService platformService, IMapper mapper)
    {
        _platformService = platformService;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all platforms.
    /// </summary>
    /// <returns>Returns array of platforms.</returns>
    [HttpGet]
    [Authorize(Policy = GlobalConstants.PlatformTypeGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var platforms = await _platformService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PlatformDTO>>(platforms));
    }

    /// <summary>
    /// Gets platform by id.
    /// </summary>
    /// <param name="id">PlatformId.</param>
    /// <returns> Returns platform by id.</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = GlobalConstants.PlatformTypeGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var platform = await _platformService.GetByIdAsync(int.Parse(id));
        return Ok(_mapper.Map<PlatformDTO>(platform));
    }

    /// <summary>
    /// Creates platform.
    /// </summary>
    /// <param name="dto">PlatformDTO.</param>
    /// <returns>Returns Ok.</returns>
    [HttpPost]
    [Logging]
    [Authorize(Policy = GlobalConstants.PlatformTypeCreatePolicy)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(CreateUpdatePlatformRequest dto)
    {
        var model = _mapper.Map<CreateUpdatePlatformModel>(dto);
        await _platformService.AddAsync(model);
        return Ok();
    }

    [HttpGet("games/{key}")]
    [Authorize(Policy = GlobalConstants.PlatformTypeGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByGameAsync(string key)
    {
        var platforms = await _platformService.GetByGameAsync(key);
        return Ok(_mapper.Map<IEnumerable<PlatformDTO>>(platforms));
    }

    /// <summary>
    /// Updates platform.
    /// </summary>
    /// <param name="dto">PlatformDTO.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpPut]
    [Logging]
    [Authorize(Policy = GlobalConstants.PlatformTypeUpdatePolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(CreateUpdatePlatformRequest dto)
    {
        var model = _mapper.Map<CreateUpdatePlatformModel>(dto);
        await _platformService.UpdateAsync(model);
        return NoContent();
    }

    /// <summary>
    /// Deletes platform by id.
    /// </summary>
    /// <param name="id">PlatformId.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpDelete("{id}")]
    [Logging]
    [Authorize(Policy = GlobalConstants.PlatformTypeDeletePolicy)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        await _platformService.DeleteAsync(int.Parse(id));
        return NoContent();
    }
}
