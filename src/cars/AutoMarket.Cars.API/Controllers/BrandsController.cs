using AutoMapper;
using AutoMarket.Cars.Application.Commands;
using AutoMarket.Cars.Application.Queries;
using AutoMarket.Cars.Shared.Dtos;
using AutoMarket.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Cars.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController(
    ISender sender,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<BrandDto>> Create(CreateBrand.Request request
        , CancellationToken cancellationToken = default) =>
        await sender.Send(request, cancellationToken)
        .ThenAsync(brand => mapper.Map<BrandDto>(brand));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BrandDto>> Get(Guid id,
        CancellationToken cancellationToken = default) =>
        await sender.Send(new GetBrand.Query(id), cancellationToken)
        .ThenAsync(brand => mapper.Map<BrandDto>(brand));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id,
        CancellationToken cancellationToken = default) =>
        await sender.Send(new DeleteBrand.Request(id), cancellationToken)
        .ThenAsync(_ => Ok());
}