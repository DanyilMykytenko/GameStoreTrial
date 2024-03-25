// <copyright file="GamesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
using WebApi.ResponseModels;

namespace WebApi.Controllers;

/// <summary>
/// Game API.
/// </summary>
[ApiController]
[Route("[controller]")]

public class GamesController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;
    private readonly IGameProductService _gameProductService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GamesController"/> class.
    /// </summary>
    /// <param name="gameProductService">Dependency game service.</param>
    public GamesController(ICommentService commentService, IMapper mapper, IGameProductService gameProductService)
    {
        _commentService = commentService;
        _mapper = mapper;
        _gameProductService = gameProductService;
    }

    /// <summary>
    /// Gets all games.
    /// </summary>
    /// <returns>Return array of games.</returns>
    [HttpGet]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] GetGamesRequestModel model)
    {
        var games = await _gameProductService.GetAllAsync(model);
        return Ok(_mapper.Map<GetGamesResponse>(games));
    }

    /// <summary>
    /// Gets game by key.
    /// </summary>
    /// <param name="key">String key.</param>
    /// <returns>Returns game by key.</returns>
    [HttpGet("{key}")]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByKey(string key)
    {
        var game = await _gameProductService.GetByKeyAsync(key);
        return Ok(_mapper.Map<GameDTO>(game));
    }

    [HttpGet("id/{id}")]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var game = await _gameProductService.GetByIdAsync(int.Parse(id));
        return Ok(_mapper.Map<GameDTO>(game));
    }

    /// <summary>
    /// Creates new game.
    /// </summary>
    /// <param name="dto">GameDto.</param>
    /// <returns>Returns Ok.</returns>
    [HttpPost]
    [Authorize(Policy = GlobalConstants.GameCreatePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(CreateUpdateGameRequest dto)
    {
        var model = _mapper.Map<CreateUpdateGameModel>(dto);
        await _gameProductService.AddAsync(model);
        return Ok();
    }

    /// <summary>
    /// Updates game.
    /// </summary>
    /// <param name="dto">GameDTO.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpPut]
    [Authorize(Policy = GlobalConstants.GameUpdatePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(CreateUpdateGameRequest dto)
    {
        var model = _mapper.Map<CreateUpdateGameModel>(dto);
        _gameProductService.Update(model);
        return NoContent();
    }

    /// <summary>
    /// Deletes game by key.
    /// </summary>
    /// <param name="key">String key.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpDelete("{key}")]
    [Authorize(Policy = GlobalConstants.GameDeletePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string key)
    {
        var game = await _gameProductService.GetByKeyAsync(key);
        await _gameProductService.DeleteByIdAsync(game.Id);
        return NoContent();
    }

    /// <summary>
    /// Download txt file with game data.
    /// </summary>
    /// <param name="key">String key.</param>
    /// <returns>Returns .txt File.</returns>
    [HttpGet("{key}/download")]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Download(string key)
    {
        await _gameProductService.Download(key);
        return Ok();
    }

    /// <summary>
    /// Get games by specific genre.
    /// </summary>
    /// <param name="id">GenreId.</param>
    /// <returns>Returns an array of games using specific genre.</returns>
    [HttpGet("genres/{id}")]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByGenre(string id)
    {
        var games = await _gameProductService.GetByGenreAsync(int.Parse(id));
        return Ok(_mapper.Map<IEnumerable<GameDTO>>(games));
    }

    /// <summary>
    /// Get games by specific platform.
    /// </summary>
    /// <param name="id">PlatformId.</param>
    /// <returns>Returns an array of games using specific platform.</returns>
    [HttpGet("platforms/{id}")]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByPlatform(string id)
    {
        var games = await _gameProductService.GetByPlatformAsync(int.Parse(id));
        return Ok(_mapper.Map<IEnumerable<GameDTO>>(games));
    }

    [HttpGet("publishers/{companyName}")]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByPublisher(string companyName)
    {
        var games = await _gameProductService.GetByPublisherAsync(companyName);
        return Ok(_mapper.Map<IEnumerable<GameDTO>>(games));
    }

    /// <summary>
    /// Adding game to busket.
    /// </summary>
    /// <param name="key">String gamekey.</param>
    [HttpGet("{key}/buy")]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddGameToBusket(string key)
    {
        await _gameProductService.AddGameToBusket(key);
        return Ok();
    }

    /// <summary>
    /// Removing game from busket.
    /// </summary>
    /// <param name="key">String gamekey.</param>
    /// <returns>NoContent.</returns>
    [HttpDelete("{key}/cancel")]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveGameFromBusket(string key)
    {
        await _gameProductService.RemoveGameFromBusket(key);
        return Ok();
    }

    [HttpPost("{key}/newcomment")]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddNewComment(string key, CreateUpdateCommentRequest comment)
    {
        var model = _mapper.Map<CreateUpdateCommentModel>(comment);
        await _gameProductService.AddNewComment(key, model);
        return Ok();
    }

    [HttpGet("{key}/comments")]
    [Authorize(Policy = GlobalConstants.GameGetPolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetComments(string key)
    {
        var comments = await _commentService.GetAllCommentsAsync(key);
        return Ok(_mapper.Map<IEnumerable<CommentDTO>>(comments));
    }

    [HttpDelete("comment/{id}")]
    [Authorize(Policy = GlobalConstants.CommentDeletePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteComment(string id)
    {
        await _commentService.DeleteAsync(int.Parse(id));
        return Ok();
    }
}