using Crud.DDD.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Crud.DDD.Infrastructure.Data.Interceptors
{
    public partial class AuditingInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;

        public AuditingInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }


        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
                return await base.SavedChangesAsync(eventData, result, cancellationToken);

            var user = await _currentUserService.GetCurrentUserAsync();

            var entries = eventData.Context.ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                var fullAuditedModel = entry.Entity as IFullAudited;

                var softDeleteModel = entry.Entity as ISoftDelete;

                if (fullAuditedModel != null && entry.State == EntityState.Added)
                {
                    fullAuditedModel.CreateTime = DateTime.Now;
                    fullAuditedModel.CreateUserId = user.Id;
                }

                if (fullAuditedModel != null && entry.State == EntityState.Modified)
                {
                    fullAuditedModel.CreateTime = DateTime.Now;
                    fullAuditedModel.ModifyUserId = user.Id;
                }

                if (softDeleteModel != null && entry.State == EntityState.Deleted)
                {
                    softDeleteModel.DeleteTime = DateTime.Now;
                    softDeleteModel.DeleteTime = DateTime.Now;
                    softDeleteModel.IsDeleted = true;
                }
            }

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
