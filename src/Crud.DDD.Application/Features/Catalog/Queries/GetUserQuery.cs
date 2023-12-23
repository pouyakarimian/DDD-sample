using AutoMapper;
using Crud.DDD.Application.Features.Catalog.Dtos;
using Crud.DDD.Core.Aggregates.CatalogAggregate.Repositories;
using Crud.DDD.Core.Common.Exeptions;
using FluentValidation;
using MediatR;

namespace Crud.DDD.Application.Features.Catalog.Queries
{

    public sealed record GetCatalogQuery(Guid Id) : IRequest<CatalogDto>;

    public sealed class GetCatalogQueryValidator : AbstractValidator<GetCatalogQuery>
    {
        public GetCatalogQueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id is required");
        }
    }

    public class GetCatalogQueryHandler : IRequestHandler<GetCatalogQuery, CatalogDto>
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public GetCatalogQueryHandler(ICatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }


        public async Task<CatalogDto> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
        {
            var catalog = await _catalogRepository
                .GetByIdNoTrackingAsync(request.Id, cancellationToken);

            if (catalog is null)
                throw new NotFoundExeption(nameof(catalog));

            return _mapper.Map<CatalogDto>(catalog);
        }
    }
}
