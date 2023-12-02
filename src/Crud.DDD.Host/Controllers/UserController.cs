using Crud.DDD.Application.Features.User.Commands;
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


        [HttpPost]
        public async Task<IActionResult> Create([Required][FromBody] CreateUserCommand userCommand,
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
    }
}
