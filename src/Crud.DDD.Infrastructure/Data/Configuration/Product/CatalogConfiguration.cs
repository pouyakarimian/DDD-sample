using Crud.DDD.Core.Aggregates.CatalogAggregate;
using Microsoft.EntityFrameworkCore;

namespace Crud.DDD.Infrastructure.Data.Configuration.Product
{
    public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Catalog> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
            .ValueGeneratedNever();

            builder.Ignore(p => p.DomainEvents);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(p => p.NormalizedName)
               .IsRequired()
               .HasMaxLength(128);

            builder.HasMany(p => p.Products)
                .WithOne()
                .HasForeignKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Metadata.FindNavigation(nameof(Catalog.Products))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
