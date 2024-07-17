using AutoMapper;
using AutoMarket.Extensions;
using AutoMarket.Users.Application.Commands;
using AutoMarket.Users.Application.Queries;
using AutoMarket.Users.Shared.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarket.Users.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    ISender sender,
    IMapper mapper) : ControllerBase
{
    [HttpPost(Name = "createUser")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Create(CreateUser.Request request,
        CancellationToken cancellationToken = default) =>
            await sender.Send(request, cancellationToken)
                .ThenAsync(user => mapper.Map<UserDto>(user.Value))
                .ThenAsync(user => CreatedAtAction(
                    actionName: nameof(GetById),
                    routeValues: new { id = user.Id },
                    value: user
                ));

    // [HttpDelete("{id:guid}", Name = "deleteUser")]
    // public async Task Delete(DeleteUser.Request request,
    //     CancellationToken cancellationToken = default) =>
    //         await sender.Send(request, cancellationToken);

    [HttpGet(Name = "getUser")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetById([FromQuery]GetUser.Query query,
        CancellationToken cancellationToken = default) =>
            await sender.Send(query, cancellationToken)
            .ThenAsync(user => mapper.Map<UserDto>(user.Value));
}