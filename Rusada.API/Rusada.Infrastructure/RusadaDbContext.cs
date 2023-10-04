using Microsoft.EntityFrameworkCore;
using Rusada.Core.Common.Interfaces;
using Rusada.Domain;
using Rusada.Domain.BaseEntities;


namespace Rusada.Infrastructure
{
    public class RusadaDbContext : DbContext, IRusadaDbContext
    {
        public RusadaDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }

        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<AircraftImage> AircraftImages { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetSoftDelete();
            SetAudit();
            var entryCount = await base.SaveChangesAsync(cancellationToken);
            return entryCount;
        }

        private void SetAudit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(s => s.State is EntityState.Added or EntityState.Modified))
            {
                if (!entry.Entity.GetType().IsAssignableTo(typeof(IAuditEntity)))
                    continue;

                var entity = (IAuditEntity)entry.Entity;

                entity.UpdatedBy = "TODO";
                entity.UpdatedDate = DateTime.Now;


                if (entry.State != EntityState.Added) continue;

                entity.CreatedBy = "TODO";
                entity.CreatedDate = DateTime.Now;
            }
        }

        private void SetSoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries().Where(s => s.State is EntityState.Deleted))
            {
                if (!entry.Entity.GetType().IsAssignableTo(typeof(ISoftDeleteEntity)))
                    continue;
                var entity = (ISoftDeleteEntity)entry.Entity;
                entity.Deleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}