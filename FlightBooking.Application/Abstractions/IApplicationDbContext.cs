using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<AirlineEntity> Airlines { get; set; }
        DbSet<FlightEntity> Flights { get; set; }
        DbSet<UsersEntity> Users { get; set; }
        DbSet<PersonInformationEntity> PersonInformations { get; set; }
        DbSet<AirplaneEntity> Airplanes { get; set; }
        DbSet<BoardingPassEntity> BoardingPasses { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
