using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crud.DDD.Infrastructure.Data.Configuration.User
{
    public class UserConfiguration : IEntityTypeConfiguration<Core.Aggregates.UserAggregate.User>
    {
        public void Configure(EntityTypeBuilder<Core.Aggregates.UserAggregate.User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired(false).HasMaxLength(50);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.HasIndex(p => p.Email).IsUnique();
        }
    }
}
