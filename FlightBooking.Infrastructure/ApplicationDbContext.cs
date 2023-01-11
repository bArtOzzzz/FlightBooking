using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        /*public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
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
            modelBuilder.Entity<AirlineEntity>(
                entity =>
                {
                    entity.Property(e => e.Id)
                          .IsRequired();
                });

            modelBuilder.Entity<FlightEntity>(
                entity =>
                {
                    entity.Property(e => e.Id)
                          .IsRequired();

                    entity.HasOne(a => a.Airlines)
                          .WithMany(f => f.Flights)
                          .HasForeignKey(a => a.AirlineId);

                    entity.Property
                });
        }*/
    }
}
