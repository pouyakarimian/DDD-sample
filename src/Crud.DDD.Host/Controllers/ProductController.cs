using Crud.DDD.Application.Features.Product.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Crud.DDD.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([Required][FromBody] CreateProductCommand productCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(productCommand, cancellationToken);

            return Ok();
        }
    }
}
