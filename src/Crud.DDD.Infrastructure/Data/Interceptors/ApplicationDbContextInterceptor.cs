using Crud.DDD.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Crud.DDD.Infrastructure.Data.Interceptors
{
    public partial class ApplicationDbContextInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            if (eventData.Context is null)
                return base.SavingChanges(eventData, result);

            //TODO:Should read from token or seasion
            var userId = Guid.NewGuid();

            var entries = eventData.Context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                var fullAuditedModel = entry.Entity as IFullAudited;

                var softDeleteModel = entry.Entity as ISoftDelete;

                if (fullAuditedModel != null && entry.State == EntityState.Added)
                {
                    fullAuditedModel.CreateTime = DateTime.Now;
                    fullAuditedModel.CreateUserId = userId;
                }

                if (fullAuditedModel != null && entry.State == EntityState.Modified)
                {
                    fullAuditedModel.CreateTime = DateTime.Now;
                    fullAuditedModel.ModifyUserId = userId;
                }

                if (softDeleteModel != null && entry.State == EntityState.Deleted)
                {
                    softDeleteModel.DeleteTime = DateTime.Now;
                    softDeleteModel.DeleteTime = DateTime.Now;
                    softDeleteModel.IsDeleted = true;
                }
            }

            return base.SavingChanges(eventData, result);
        }
    }
}
