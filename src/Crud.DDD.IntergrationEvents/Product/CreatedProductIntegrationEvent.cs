namespace Crud.DDD.IntergrationEvents.Product
{
    public sealed record CreatedProductIntegrationEvent(Guid Id, string Name, string SKU);
}
