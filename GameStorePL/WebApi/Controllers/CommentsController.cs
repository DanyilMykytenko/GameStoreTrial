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
/// Comments API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all comments.
    /// </summary>
    /// <returns>Returns array of comments.</returns>
    [HttpGet]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentService.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<CommentDTO>>(comments));
    }

    [HttpGet("ban/{id}")]
    [Authorize(Policy = GlobalConstants.UserUpdatePolicy)]
    public async Task<IActionResult> BanUser(string id)
    {
        await _commentService.BanUser(int.Parse(id));
        return Ok();
    }

    [HttpGet("ban/duration")]
    public IActionResult GetBanDurationOptions()
    {
        var options = new string[] { "1 hour", "1 day", "1 week", "1 month", "Permanently" };
        return Ok(options);
    }

    /// <summary>
    /// Gets comment by id.
    /// </summary>
    /// <param name="id">CommentId.</param>
    /// <returns> Returns comment by id.</returns>
    [HttpGet("{id}")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var comment = await _commentService.GetByIdAsync(int.Parse(id));
        return Ok(_mapper.Map<CommentDTO>(comment));
    }

    /// <summary>
    /// Creates comment.
    /// </summary>
    /// <param name="dto">CommentDTO.</param>
    /// <returns>Returns Ok.</returns>
    [HttpPost]
    [Logging]
    [Authorize(Policy = GlobalConstants.CommentCreatePolicy)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(CreateUpdateCommentRequest dto)
    {
        var model = _mapper.Map<CreateUpdateCommentModel>(dto);
        await _commentService.AddAsync(model);
        return Ok();
    }

    /// <summary>
    /// Updates comment.
    /// </summary>
    /// <param name="dto">CommentDTO.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpPut]
    [Authorize(Policy = GlobalConstants.CommentCreatePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(CreateUpdateCommentRequest dto)
    {
        var model = _mapper.Map<CreateUpdateCommentModel>(dto);
        await _commentService.UpdateAsync(model);
        return NoContent();
    }

    /// <summary>
    /// Deletes comment by id.
    /// </summary>
    /// <param name="id">CommentId.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = GlobalConstants.CommentDeletePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        await _commentService.DeleteAsync(int.Parse(id));
        return NoContent();
    }
}
