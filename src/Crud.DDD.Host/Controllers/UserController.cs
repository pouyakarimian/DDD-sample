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
        public IActionResult Create([Required][FromBody] CreateUserCommand userCommand,
            CancellationToken cancellationToken)
        {
            _mediator.Send(userCommand, cancellationToken);

            return Ok();
        }
    }
}
