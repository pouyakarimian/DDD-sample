using AutoMapper;
using Crud.DDD.Application.Features.Catalog.Dtos;

namespace Crud.DDD.Application.Features.Catalog.Mapping
{
    public sealed class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<DDD.Core.Aggregates.CatalogAggregate.Catalog, CatalogDto>();
        }
    }
}
