using AutoMapper;
using AutoMarket.Cars.Application.Commands;
using AutoMarket.Cars.Shared.Dtos;
using MediatR;
using AutoMarket.Extensions;
using Microsoft.AspNetCore.Mvc;
using AutoMarket.Cars.Application.Queries;

namespace AutoMarket.Cars.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController(
    ISender sender,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CarDto>> Create([FromBody]CreateCar.Request request,
        CancellationToken cancellationToken = default) =>
            await sender.Send(request, cancellationToken)
                .ThenAsync(car => mapper.Map<CarDto>(car.Value));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id,
        CancellationToken cancellationToken = default) =>
            await sender.Send(new DeleteCar.Request(id), cancellationToken)
                .ThenAsync(_ => Ok());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CarDto>> Get(Guid id,
        CancellationToken cancellationToken = default) =>
            await sender.Send(new GetCar.Query(id), cancellationToken)
                .ThenAsync(car => mapper.Map<CarDto>(car.Value));
}