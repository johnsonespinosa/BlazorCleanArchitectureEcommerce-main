using Application.Interfaces;
using Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly IUser _user;
        private readonly TimeProvider _dateTime;

        public AuditableEntityInterceptor(
            IUser user,
            TimeProvider dateTime)
        {
            _user = user;
            _dateTime = dateTime;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                var utcNow = _dateTime.GetUtcNow();
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (_user?.Id != null)
                            entry.Entity.CreatedBy = _user.Id;
                        entry.Entity.Created = utcNow;
                        break;
                    case EntityState.Modified:
                        if (_user?.Id != null)
                            entry.Entity.LastModifiedBy = _user.Id;
                        entry.Entity.LastModified = utcNow;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        entry.Entity.Deleted = utcNow;
                        if (_user?.Id != null)
                            entry.Entity.DeletedBy = _user.Id;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
        }
    }
}
