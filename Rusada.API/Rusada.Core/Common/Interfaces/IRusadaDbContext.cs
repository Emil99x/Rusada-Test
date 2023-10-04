using Microsoft.EntityFrameworkCore;
using Rusada.Domain;
using Rusada.Domain.BaseEntities;

namespace Rusada.Core.Common.Interfaces
{
    public interface IRusadaDbContext : IDisposable
    {
        DbSet<Aircraft> Aircrafts { get; set; }
        DbSet<AircraftImage> AircraftImages { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
