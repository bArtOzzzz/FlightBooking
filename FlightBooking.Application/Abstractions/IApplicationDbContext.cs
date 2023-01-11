using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<AirlineEntity> Airlines { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
