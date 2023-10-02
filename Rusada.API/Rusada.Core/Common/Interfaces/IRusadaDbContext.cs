using Microsoft.EntityFrameworkCore;
using Rusada.Domain;

namespace Rusada.Core.Common.Interfaces
{
    public interface IRusadaDbContext : IDisposable
    {
        DbSet<Aircraft> Aircrafts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
