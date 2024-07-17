using AutoMapper;
using AutoMarket.Extensions;
using AutoMarket.Orders.Application.Commands;
using AutoMarket.Orders.Application.Queries;
using AutoMarket.Orders.Shared.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Orders.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(
    ISender sender,
    IMapper mapper) : ControllerBase
{
    [HttpPost(Name = "createOrder")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderDto>> Create(CreateOrder.Request request,
        CancellationToken cancellationToken = default) =>
        await sender.Send(request, cancellationToken)
            .ThenAsync(order => mapper.Map<OrderDto>(order.Value))
            .ThenAsync(order => CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { id = order.Id },
                value: order
            ));

    [HttpGet(Name = "getOrder")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDto>> GetById([FromQuery]GetOrder.Query query,
        CancellationToken cancellationToken = default) =>
        await sender.Send(query, cancellationToken)
        .ThenAsync(order => mapper.Map<OrderDto>(order.Value));
}