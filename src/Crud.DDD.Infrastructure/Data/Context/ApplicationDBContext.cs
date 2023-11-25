using Crud.DDD.Core.Aggregates.UserAggregate;
using Crud.DDD.Core.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Crud.DDD.Infrastructure.Data.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);

            GlobalSoftDeleteQuery(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void GlobalSoftDeleteQuery(ModelBuilder modelBuilder)
        {
            var softDeleteEntities = typeof(Entity<>).Assembly.GetTypes()
           .Where(type => type.IsClass && !type.IsAbstract && typeof(Entity<>)
           .IsAssignableFrom(type) && typeof(ISoftDelete).IsAssignableFrom((type)));

            foreach (var softDeleteEntity in softDeleteEntities)
                modelBuilder.Entity(softDeleteEntity).HasQueryFilter(GenerateQueryFilterLambda(softDeleteEntity));
        }

        private LambdaExpression GenerateQueryFilterLambda(Type type)
        {
            // we should generate:  e => e.Deleted == false
            // or: e => !e.Deleted

            // e =>
            var parameter = Expression.Parameter(type, "e");

            // false
            var falseConstant = Expression.Constant(false);

            // e.Deleted
            //check: BaseEntity<int> => BaseEntity<bool>
            var propertyAccess = Expression.PropertyOrField(parameter, "IsDeleted");

            // e.Deleted == false
            var equalExpression = Expression.Equal(propertyAccess, falseConstant);

            // e => e.Deleted == false
            var lambda = Expression.Lambda(equalExpression, parameter);

            return lambda;
        }

    }
}
