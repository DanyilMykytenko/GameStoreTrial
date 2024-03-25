using AutoMapper;
using Business.Infrastructure.Interfaces;
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
/// Publisher API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PublishersController : ControllerBase
{
    private readonly IPublisherService _publisherService;
    private readonly IPublisherSupplierService _publisherSupplierService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="PublishersController"/> class.
    /// </summary>
    /// <param name="publisherService">Dependency game service.</param>
    public PublishersController(IPublisherService publisherService, IMapper mapper, IPublisherSupplierService publisherSupplierService)
    {
        _publisherService = publisherService;
        _mapper = mapper;
        _publisherSupplierService = publisherSupplierService;
    }

    /// <summary>
    /// Gets all publishers.
    /// </summary>
    /// <returns>Return array of games.</returns>
    [HttpGet]
    [Authorize(Policy = GlobalConstants.PublisherGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var publishers = await _publisherSupplierService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PublisherDTO>>(publishers));
    }

    [HttpGet("games/{key}")]
    [Authorize(Policy = GlobalConstants.PublisherGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByGameAsync(string key)
    {
        var publisher = await _publisherSupplierService.GetByGameAsync(key);
        return Ok(_mapper.Map<PublisherDTO>(publisher));
    }

    /// <summary>
    /// Gets publisher by id.
    /// </summary>
    /// <param name="companyName">String id.</param>
    /// <returns>Returns publisher by id.</returns>
    [HttpGet("{companyname}")]
    [Authorize(Policy = GlobalConstants.PublisherGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCompanyName(string companyName)
    {
        var publisher = await _publisherSupplierService.GetByCompanyNameAsync(companyName);
        return Ok(_mapper.Map<PublisherDTO>(publisher));
    }

    /// <summary>
    /// Creates new publisher.
    /// </summary>
    /// <param name="dto">PublisherDTO.</param>
    /// <returns>Returns Ok.</returns>
    [HttpPost]
    [Authorize(Policy = GlobalConstants.PublisherCreatePermission)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(CreateUpdatePublisherRequest dto)
    {
        var model = _mapper.Map<CreateUpdatePublisherModel>(dto);
        await _publisherService.AddAsync(model);
        return Ok();
    }

    /// <summary>
    /// Updates game.
    /// </summary>
    /// <param name="dto">GameDTO.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpPut]
    [Authorize(Policy = GlobalConstants.PublisherUpdatePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(CreateUpdatePublisherRequest dto)
    {
        var model = _mapper.Map<CreateUpdatePublisherModel>(dto);
        await _publisherSupplierService.UpdateAsync(model);
        return NoContent();
    }

    /// <summary>
    /// Deletes game by key.
    /// </summary>
    /// <param name="id">String key.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = GlobalConstants.PublisherDeletePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var publisher = await _publisherService.GetByIdAsync(int.Parse(id));
        await _publisherSupplierService.DeleteAsync(publisher.Id);
        return NoContent();
    }
}
