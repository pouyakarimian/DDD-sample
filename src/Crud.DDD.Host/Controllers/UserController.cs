﻿using Crud.DDD.Application.Features.User.Commands;
using Crud.DDD.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Crud.DDD.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([Required] Guid userId, CancellationToken cancellationToken)
        {
            var getUser = new GetUserQuery
            {
                UserId = userId
            };

            var userDto = await _mediator.Send(getUser, cancellationToken);

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllAsync([FromBody] GetAllUsersQuery usersQuery, CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(usersQuery, cancellationToken);

            return Ok(users);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([Required][FromBody] CreateUserCommand userCommand,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(userCommand, cancellationToken);

            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAsync(Guid userId)
        {
            await _mediator.Send(new DeleteUserCommand(userId));

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([Required][FromBody] UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(updateUserCommand, cancellationToken);

            return Ok();
        }
    }
}
