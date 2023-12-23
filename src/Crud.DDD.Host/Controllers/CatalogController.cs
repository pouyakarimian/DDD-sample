using Crud.DDD.Application.Features.Catalog.Dtos;
using Crud.DDD.Application.Features.Catalog.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Crud.DDD.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogDto>> GetAsync([Required] Guid id)
        {
            await _mediator.Send(new GetCatalogQuery(id));

            return Ok();
        }
    }
}
