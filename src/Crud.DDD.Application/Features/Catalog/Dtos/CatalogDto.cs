namespace Crud.DDD.Application.Features.Catalog.Dtos
{
    public record CatalogDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
