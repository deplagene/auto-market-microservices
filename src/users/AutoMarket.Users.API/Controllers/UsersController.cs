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
    public async Task<ActionResult<UserDto>> Create(CreateUser.Request request,
        CancellationToken cancellationToken = default) =>
            await sender.Send(request, cancellationToken)
                .ThenAsync(user => mapper.Map<UserDto>(user.Value));

    // [HttpDelete("{id:guid}", Name = "deleteUser")]
    // public async Task Delete(DeleteUser.Request request,
    //     CancellationToken cancellationToken = default) =>
    //         await sender.Send(request, cancellationToken);

    [HttpGet("{id:guid}", Name = "getUser")]
    public async Task<ActionResult<UserDto>> GetById([FromQuery]GetUser.Query query,
        CancellationToken cancellationToken = default) =>
            await sender.Send(query, cancellationToken)
            .ThenAsync(user => mapper.Map<UserDto>(user.Value));
}