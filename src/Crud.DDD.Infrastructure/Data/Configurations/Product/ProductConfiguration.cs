using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.DDD.Infrastructure.Data.Configurations.Product
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Core.Aggregates.ProductAggregate.Product>
    {
        public void Configure(EntityTypeBuilder<Core.Aggregates.ProductAggregate.Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
            .ValueGeneratedNever();

            builder.Ignore(p => p.DomainEvents);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(300);

            builder.OwnsOne(p => p.SKU, ownedNav =>
            {
                ownedNav
                    .Property(p => p.Value)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("SKU");
            });
        }
    }
}
