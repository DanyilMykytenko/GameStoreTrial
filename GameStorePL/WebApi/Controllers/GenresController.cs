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
/// Genre API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly IGenreCategoryService _genreCategoryService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenresController"/> class.
    /// </summary>
    /// <param name="genreService">Dependency genre service.</param>
    public GenresController(IGenreService genreService, IMapper mapper, IGenreCategoryService genreCategoryService)
    {
        _genreService = genreService;
        _mapper = mapper;
        _genreCategoryService = genreCategoryService;
    }

    /// <summary>
    /// Gets all genres.
    /// </summary>
    /// <returns>Returns array of genres.</returns>
    [HttpGet]
    [Authorize(Policy = GlobalConstants.GenreGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var genres = await _genreCategoryService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<GenreDTO>>(genres));
    }

    [HttpGet("games/{key}")]
    [Authorize(Policy = GlobalConstants.GenreGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByGameAsync(string key)
    {
        var genres = await _genreCategoryService.GetByGameAsync(key);
        return Ok(_mapper.Map<IEnumerable<GenreDTO>>(genres));
    }

    /// <summary>
    /// Gets genre by id.
    /// </summary>
    /// <param name="id">GenreId.</param>
    /// <returns>Returns genre by id.</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = GlobalConstants.GenreGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var genre = await _genreCategoryService.GetByIdAsync(int.Parse(id));
        return Ok(_mapper.Map<GenreDTO>(genre));
    }

    /// <summary>
    /// Creates new genre.
    /// </summary>
    /// <param name="dto">GenreDTO.</param>
    /// <returns>Returns Ok.</returns>
    [HttpPost]
    [Authorize(Policy = GlobalConstants.GenreCreatePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(CreateUpdateGenreRequest dto)
    {
        var model = _mapper.Map<CreateUpdateGenreModel>(dto);
        await _genreService.AddAsync(model);
        return Ok();
    }

    /// <summary>
    /// Updates genre.
    /// </summary>
    /// <param name="dto">GenreDTO.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpPut]
    [Authorize(Policy = GlobalConstants.GenreUpdatePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(CreateUpdateGenreRequest dto)
    {
        var model = _mapper.Map<CreateUpdateGenreModel>(dto);
        await _genreCategoryService.UpdateAsync(model);
        return NoContent();
    }

    /// <summary>
    /// Deletes genre.
    /// </summary>
    /// <param name="id">GenreId.</param>
    /// <returns>Return NoContent.</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = GlobalConstants.GenreDeletePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        await _genreCategoryService.DeleteAsync(int.Parse(id));
        return NoContent();
    }
}
