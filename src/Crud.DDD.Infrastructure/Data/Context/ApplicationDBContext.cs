using Crud.DDD.Core.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Crud.DDD.Infrastructure.Data.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<DbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
