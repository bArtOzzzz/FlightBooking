using FlightBooking.Application.Abstractions;
using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            Database.EnsureCreated();
        }

        public DbSet<AirlineEntity> Airlines { get; set; } = default!;
        public DbSet<FlightEntity> Flights { get; set; } = default!;
        public DbSet<UsersEntity> Users { get; set; } = default!;
        public DbSet<PersonInformationEntity> PersonInformations { get; set; } = default!;
        public DbSet<AirplaneEntity> Airplanes { get; set; } = default!;
        public DbSet<BoardingPassEntity> BoardingPasses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Local variables
            Guid[] GuidAirlinesArr = { Guid.NewGuid(),
                                       new Guid("96e1738a-568d-4a23-be7d-f2f3382f1cf9"),
                                       new Guid("b01e92b9-2c8b-4437-b290-eae73e1017f0") };

            Guid[] GuidAirplanesArr = { Guid.NewGuid(),
                                        Guid.NewGuid(),
                                        Guid.NewGuid(),
                                        Guid.NewGuid(),
                                        Guid.NewGuid(),
                                        Guid.NewGuid(),
                                        Guid.NewGuid() };

            Guid[] GuidUserArr = { Guid.NewGuid(),
                                   Guid.NewGuid(),
                                   Guid.NewGuid() };

            Guid[] GuidFlightArr = { Guid.NewGuid(),
                                     Guid.NewGuid(),
                                     Guid.NewGuid(),
                                     Guid.NewGuid() };
            #endregion

            #region Relationships configuration
            modelBuilder.Entity<AirlineEntity>(
                entity =>
                {
                    entity.Property(e => e.Id)
                          .IsRequired();
                });

            modelBuilder.Entity<AirplaneEntity>(
                entity =>
                {
                    entity.Property(e => e.Id)
                          .IsRequired();
                });

            modelBuilder.Entity<PersonInformationEntity>(
                entity =>
                {
                    entity.Property(e => e.Id)
                          .IsRequired();
                });

            modelBuilder.Entity<UsersEntity>(
                entity =>
                {
                    entity.Property(e => e.Id)
                          .IsRequired();

                    entity.HasOne(e => e.PersonInformation)
                          .WithOne(pi => pi.User)
                          .HasForeignKey<PersonInformationEntity>(e => e.UserId);
                });

            modelBuilder.Entity<FlightEntity>(
                entity =>
                {
                    entity.Property(e => e.Id)
                          .IsRequired();

                    entity.HasOne(a => a.Airlines)
                          .WithMany(f => f.Flights)
                          .HasForeignKey(a => a.AirlineId);

                    entity.HasOne(a => a.Airplane)
                          .WithMany(f => f.Flights)
                          .HasForeignKey(a => a.AirplaneId);
                });

            modelBuilder.Entity<BoardingPassEntity>(
                entity =>
                {
                    entity.Property(e => e.Id)
                          .IsRequired();

                    entity.HasOne(f => f.Flight)
                          .WithMany(b => b.BoardingPasses)
                          .HasForeignKey(f => f.FlightId);

                    entity.HasOne(u => u.User)
                          .WithMany(b => b.BoardingPasses)
                          .HasForeignKey(u => u.UserId);
                });
            #endregion

            #region SEEDDATA
            modelBuilder.Entity<AirlineEntity>().HasData(
                new AirlineEntity
                {
                    Id = GuidAirlinesArr[0],
                    AirlineName = "Belavia",
                    Rating = 4.3,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new AirlineEntity
                {
                    Id = GuidAirlinesArr[1],
                    AirlineName = "American Airlines",
                    Rating = 4.7,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new AirlineEntity
                {
                    Id = GuidAirlinesArr[2],
                    AirlineName = "Airnorth",
                    Rating = 3.9,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                });

            modelBuilder.Entity<AirplaneEntity>().HasData(
                new AirplaneEntity
                {
                    Id = GuidAirplanesArr[0],
                    ModelName = "ATR EVO",
                    MaximumSeats = 525,
                    MaximumWeight = 46750,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new AirplaneEntity
                {
                    Id = GuidAirplanesArr[1],
                    ModelName = "CRIAC CR929",
                    MaximumSeats = 320,
                    MaximumWeight = 32400,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new AirplaneEntity
                {
                    Id = GuidAirplanesArr[2],
                    ModelName = "Comac C919",
                    MaximumSeats = 180,
                    MaximumWeight = 22600,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new AirplaneEntity
                {
                    Id = GuidAirplanesArr[3],
                    ModelName = "Airbus A220",
                    MaximumSeats = 125,
                    MaximumWeight = 35600,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new AirplaneEntity
                {
                    Id = GuidAirplanesArr[4],
                    ModelName = "Comac C919",
                    MaximumSeats = 150,
                    MaximumWeight = 22600,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new AirplaneEntity
                {
                    Id = GuidAirplanesArr[5],
                    ModelName = "Airbus A350",
                    MaximumSeats = 300,
                    MaximumWeight = 31000,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new AirplaneEntity
                {
                    Id = GuidAirplanesArr[6],
                    ModelName = "ATR EVO",
                    MaximumSeats = 525,
                    MaximumWeight = 46750,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                });

            modelBuilder.Entity<PersonInformationEntity>().HasData(
                new PersonInformationEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = GuidUserArr[0],
                    Citizenship = "Belarus",
                    IdentificationNumber = "2763984J836PB3",
                    ExpirePasportDate = new DateTime(2027, 7, 20),
                    Name = "Александр",
                    Surname = "Иванов",
                    BirthDate = new DateTime(1983, 3, 17),
                    Gender = "М",
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new PersonInformationEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = GuidUserArr[1],
                    Citizenship = "America",
                    IdentificationNumber = "2763984J836PB3",
                    ExpirePasportDate = new DateTime(2024, 1, 15),
                    Name = "Владислав",
                    Surname = "Лазарев",
                    BirthDate = new DateTime(1993, 6, 11),
                    Gender = "М",
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new PersonInformationEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = GuidUserArr[2],
                    Citizenship = "Poland",
                    IdentificationNumber = "2763984J836PB3",
                    ExpirePasportDate = new DateTime(2025, 11, 13),
                    Name = "Мария",
                    Surname = "Лебедева",
                    BirthDate = new DateTime(1998, 8, 3),
                    Gender = "Ж",
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                });

            modelBuilder.Entity<UsersEntity>().HasData(
                new UsersEntity
                {
                    Id = GuidUserArr[0],
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new UsersEntity
                {
                    Id = GuidUserArr[1],
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new UsersEntity
                {
                    Id = GuidUserArr[2],
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                });

            modelBuilder.Entity<FlightEntity>().HasData(
                new FlightEntity
                {
                    Id = GuidFlightArr[0],
                    AirlineId = GuidAirlinesArr[0],
                    AirplaneId = GuidAirplanesArr[0],
                    Departurer = "Minsk",
                    Arrival = "Boston",
                    DepartureDate = new DateTime(2023, 2, 17, 22, 15, 0),
                    ArrivingDate = new DateTime(2023, 2, 19, 18, 20, 0),
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new FlightEntity
                {
                    Id = GuidFlightArr[1],
                    AirlineId = GuidAirlinesArr[0],
                    AirplaneId = GuidAirplanesArr[1],
                    Departurer = "Pekin",
                    Arrival = "Paris",
                    DepartureDate = new DateTime(2023, 5, 13, 10, 30, 0),
                    ArrivingDate = new DateTime(2023, 5, 13, 18, 55, 0),
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new FlightEntity
                {
                    Id = GuidFlightArr[2],
                    AirlineId = GuidAirlinesArr[1],
                    AirplaneId = GuidAirplanesArr[2],
                    Departurer = "Singapore",
                    Arrival = "Mykonos",
                    DepartureDate = new DateTime(2023, 3, 20, 20, 55, 0),
                    ArrivingDate = new DateTime(2023, 3, 23, 11, 15, 0),
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new FlightEntity
                {
                    Id = GuidFlightArr[3],
                    AirlineId = GuidAirlinesArr[2],
                    AirplaneId = GuidAirplanesArr[3],
                    Departurer = "frankfurt",
                    Arrival = "yaunde",
                    DepartureDate = new DateTime(2023, 7, 3, 6, 50, 0),
                    ArrivingDate = new DateTime(2023, 7, 6, 3, 50, 0),
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                });

            modelBuilder.Entity<BoardingPassEntity>().HasData(
                new BoardingPassEntity
                {
                    Id = Guid.NewGuid(),
                    FlightId = GuidFlightArr[0],
                    UserId = GuidUserArr[0],
                    CreatedDate = new DateTime(2023, 5, 12, 12, 17, 03).ToString("MM-dd-yy"),
                    CreatedTime = new DateTime(2023, 5, 12, 12, 17, 03).ToString("HH:m:s"),
                    Prise = 778,
                    isExpired = false,
                    BookingExpireDate = new DateTime(2023, 5, 15),
                },

                new BoardingPassEntity
                {
                    Id = Guid.NewGuid(),
                    FlightId = GuidFlightArr[1],
                    UserId = GuidUserArr[0],
                    CreatedDate = new DateTime(2023, 2, 6, 8, 43, 17).ToString("MM-dd-yy"),
                    CreatedTime = new DateTime(2023, 2, 6, 8, 43, 17).ToString("HH:m:s"),
                    Prise = 1661,
                    isExpired = false,
                    BookingExpireDate = new DateTime(2023, 2, 9)
                },

                new BoardingPassEntity
                {
                    Id = Guid.NewGuid(),
                    FlightId = GuidFlightArr[2],
                    UserId = GuidUserArr[1],
                    CreatedDate = new DateTime(2023, 7, 24, 19, 36, 16).ToString("MM-dd-yy"),
                    CreatedTime = new DateTime(2023, 7, 24, 19, 36, 16).ToString("HH:m:s"),
                    Prise = 1672,
                    isExpired = false,
                    BookingExpireDate = new DateTime(2023, 7, 27)
                },

                new BoardingPassEntity
                {
                    Id = Guid.NewGuid(),
                    FlightId = GuidFlightArr[3],
                    UserId = GuidUserArr[2],
                    CreatedDate = new DateTime(2023, 3, 16, 15, 22, 48).ToString("MM-dd-yy"),
                    CreatedTime = new DateTime(2023, 3, 16, 15, 22, 48).ToString("HH:m:s"),
                    Prise = 1401,
                    isExpired = false,
                    BookingExpireDate = new DateTime(2023, 3, 19)
                });
            #endregion
        }
    }
}
