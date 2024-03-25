using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;
using WebApi.Models;
using WebApi.RequestModels;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrdersController"/> class.
    /// </summary>
    /// <param name="orderService">Dependency game service.</param>
    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all orders from history.
    /// </summary>
    /// <returns>Return array of orders.</returns>
    [HttpGet("history")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllFromHistory([FromQuery] HistoryRequestModel dto)
    {
        var model = _mapper.Map<HistoryModel>(dto);
        var orders = await _orderService.GetHistory(model);
        return Ok(_mapper.Map<IEnumerable<OrderDTO>>(orders));
    }

    /// <summary>
    /// Gets all orders.
    /// </summary>
    /// <returns>Return array of orders.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    /// <summary>
    /// Gets order by id.
    /// </summary>
    /// <param name="id">String id.</param>
    /// <returns>Returns order by id.</returns>
    [HttpGet("{id}")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var order = await _orderService.GetOrderWithDetails(int.Parse(id));
        return Ok(_mapper.Map<OrderDetailsDTO>(order));
    }

    /// <summary>
    /// Creates new order.
    /// </summary>
    /// <param name="dto">OrderDto.</param>
    /// <returns>Returns Ok.</returns>
    [HttpPost]
    [Logging]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(CreateUpdateOrderRequest dto)
    {
        var model = _mapper.Map<CreateUpdateOrderModel>(dto);
        await _orderService.AddAsync(model);
        return Ok();
    }

    /// <summary>
    /// Updates order.
    /// </summary>
    /// <param name="dto">OrderDTO.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpPut]
    [Authorize(Policy = "Moderator")]
    [Authorize(Policy = "Administrator")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(CreateUpdateOrderRequest dto)
    {
        var model = _mapper.Map<CreateUpdateOrderModel>(dto);
        await _orderService.UpdateAsync(model);
        return NoContent();
    }

    /// <summary>
    /// Deletes order by id.
    /// </summary>
    /// <param name="id">String id.</param>
    /// <returns>Returns NoContent.</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = GlobalConstants.OrderDetailsDeletePolicy)]
    [Logging]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var order = await _orderService.GetByIdAsync(int.Parse(id));
        await _orderService.DeleteAsync(order.Id);
        return NoContent();
    }

    [HttpPost("pay")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Pay(PaymentOptionDTO dto)
    {
        var detail = await _orderService.ExecuteTransaction(dto.Method, _mapper.Map<VisaTransactionModel>(dto.Model));
        return Ok(_mapper.Map<OrderDTO>(detail));
    }

    [HttpGet("options")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetPaymentOptions()
    {
        var options = PaymentOptionsService.Options;
        return Ok(new { PaymentMethods = options });
    }
}