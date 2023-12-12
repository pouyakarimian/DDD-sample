using Crud.DDD.Core.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Crud.DDD.Infrastructure.Data.Context
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public UnitOfWork(ApplicationDBContext context,
            IMediator mediator,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            var currentUser = await GetCurrentUserAsync();

            HandleAdded(currentUser);

            HandleDeleteded(currentUser);

            HandleModified(currentUser);

            await _context.SaveChangesAsync(cancellationToken);

            await AfterSaveChangesAsync(cancellationToken);
        }

        //public async Task CommitAsync(CancellationToken cancellationToken)
        //{
        //    var currentUser = await GetCurrentUserAsync();
        //    var strategy = _context.Database.CreateExecutionStrategy();

        //    // Executing the strategy.
        //    await strategy.ExecuteAsync(async () =>
        //    {
        //        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        //        //_logger.LogInformation("----- Begin transaction: '{TransactionId}'", transaction.TransactionId);

        //        try
        //        {
        //            // Getting the domain events and event stores from the tracked entities in the EF Core context.
        //            //var (domainEvents, eventStores) = BeforeSaveChanges();

        //            //var rowsAffected = await _context.SaveChangesAsync();

        //            //_logger.LogInformation("----- Commit transaction: '{TransactionId}'", transaction.TransactionId);

        //            HandleAdded(currentUser);

        //            HandleDeleteded(currentUser);

        //            HandleModified(currentUser);

        //            //await transaction.CommitAsync(cancellationToken);

        //            // Triggering the events and saving the stores.
        //            //await AfterSaveChangesAsync(domainEvents, eventStores);

        //            //_logger.LogInformation(
        //            //    "----- Transaction successfully confirmed: '{TransactionId}', Rows Affected: {RowsAffected}",
        //            //    transaction.TransactionId,
        //            //    rowsAffected);
        //        }
        //        catch (Exception)
        //        {
        //            //_logger.LogError(
        //            //    ex,
        //            //    "An unexpected exception occurred while committing the transaction: '{TransactionId}', message: {Message}",
        //            //    transaction.TransactionId,
        //            //    ex.Message);

        //            await transaction.RollbackAsync();

        //            throw;
        //        }
        //    });
        //}

        private void HandleAdded(CurrentUserInfo currentUser)
        {
            var added = _context.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Added);

            foreach (var entry in added)
            {
                if (entry is { State: EntityState.Added, Entity: IFullAudited fullAuditedModel })
                {
                    fullAuditedModel.CreateTime = DateTime.Now;
                    fullAuditedModel.CreateUserId = currentUser.Id;
                }
            }
        }

        private void HandleModified(CurrentUserInfo currentUser)
        {
            var entries = _context.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry is { State: EntityState.Modified, Entity: IFullAudited fullAuditedModel })
                {
                    fullAuditedModel.ModifyTime = DateTime.Now;
                    fullAuditedModel.ModifyUserId = currentUser.Id;
                }
            }
        }

        private void HandleDeleteded(CurrentUserInfo currentUser)
        {
            var entries = _context.ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry is { State: EntityState.Deleted, Entity: ISoftDelete softDeletedModel })
                {
                    entry.State = EntityState.Modified;
                    softDeletedModel.DeleteTime = DateTime.Now;
                    softDeletedModel.DeleteUserId = currentUser.Id;
                    softDeletedModel.IsDeleted = true;
                }
            }
        }

        private async Task<CurrentUserInfo> GetCurrentUserAsync()
        {
            return await _currentUserService.GetCurrentUserAsync();
        }

        private async Task AfterSaveChangesAsync(CancellationToken cancellationToken)
        {
            var domainEntities = _context.ChangeTracker
                .Entries<IEntity>()
                .Where(p => p.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(entry => entry.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entry => entry.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .AsParallel()
                .Select(@event => _mediator.Publish(@event))
                .ToList();

            await Task.WhenAll(tasks);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }



        /// <summary>
        /// Executes logic before saving changes to the database.
        /// </summary>
        /// <returns>A tuple containing the list of domain events and event stores.</returns>
        //private (IReadOnlyList<BaseEvent> domainEvents, IReadOnlyList<EventStore> eventStores) BeforeSaveChanges()
        //{
        //    // Get all domain entities with pending domain events
        //    var domainEntities = _writeDbContext
        //        .ChangeTracker
        //        .Entries<BaseEntity>()
        //        .Where(entry => entry.Entity.DomainEvents.Any())
        //        .ToList();

        //    // Get all domain events from the domain entities
        //    var domainEvents = domainEntities
        //        .SelectMany(entry => entry.Entity.DomainEvents)
        //        .ToList();

        //    // Convert domain events to event stores
        //    var eventStores = domainEvents
        //        .ConvertAll(@event => new EventStore(@event.AggregateId, @event.GetGenericTypeName(), @event.ToJson()));

        //    // Clear domain events from the entities
        //    domainEntities.ForEach(entry => entry.Entity.ClearDomainEvents());

        //    return (domainEvents.AsReadOnly(), eventStores.AsReadOnly());
        //}

        ///// <summary>
        ///// Performs necessary actions after saving changes, such as publishing domain events and storing event stores.
        ///// </summary>
        ///// <param name="domainEvents">The list of domain events.</param>
        ///// <param name="eventStores">The list of event stores.</param>
        ///// <returns>A task representing the asynchronous operation.</returns>
        //private async Task AfterSaveChangesAsync(
        //    IReadOnlyList<BaseEvent> domainEvents,
        //    IReadOnlyList<EventStore> eventStores)
        //{
        //    // If there are no domain events or event stores, return without performing any actions.
        //    if (!domainEvents.Any() || !eventStores.Any())
        //        return;

        //    // Publish each domain event in parallel using _mediator.
        //    var tasks = domainEvents
        //        .AsParallel()
        //        .Select(@event => _mediator.Publish(@event))
        //        .ToList();

        //    // Wait for all the published events to be processed.
        //    await Task.WhenAll(tasks);

        //    // Store the event stores using _eventStoreRepository.
        //    await _eventStoreRepository.StoreAsync(eventStores);
        //}

    }
}
