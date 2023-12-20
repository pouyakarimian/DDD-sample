using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.DDD.Infrastructure.Data.Configurations.User
{
    public class UserConfiguration : IEntityTypeConfiguration<Crud.DDD.Core.Aggregates.UserAggregate.User>
    {
        public void Configure(EntityTypeBuilder<Core.Aggregates.UserAggregate.User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
            .ValueGeneratedNever();

            builder.Ignore(p => p.DomainEvents);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(p => p.UserName)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.HasIndex(p => p.UserName)
                .IsUnique()
                .HasFilter("IsDeleted=0");

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

        }
    }
}
