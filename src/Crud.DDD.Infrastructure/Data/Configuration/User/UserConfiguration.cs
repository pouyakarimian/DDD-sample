using Crud.DDD.Core.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.DDD.Infrastructure.Data.Configuration.User
{
    public class UserConfiguration : IEntityTypeConfiguration<Crud.DDD.Core.Aggregates.UserAggregate.User>
    {
        public void Configure(EntityTypeBuilder<Core.Aggregates.UserAggregate.User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired(false).HasMaxLength(50);
            builder.OwnsOne(p => p.Email, ownedNav =>
            {
                ownedNav
                    .Property(email => email.Address)
                    .IsRequired() // NOT NULL
                    .HasMaxLength(254)
                    .HasColumnName("Email");

                // Unique Index
                ownedNav
                    .HasIndex(email => email.Address)
                    .HasFilter("IsDeleted=0")
                    .IsUnique();
            });

            builder.Ignore(p => p.DomainEvents);
        }
    }
}
