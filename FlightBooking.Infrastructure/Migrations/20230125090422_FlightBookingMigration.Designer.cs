﻿// <auto-generated />
using System;
using FlightBooking.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightBooking.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230125090422_FlightBookingMigration")]
    partial class FlightBookingMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlightBooking.Domain.Entities.AirlineEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AirlineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Airlines");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3c8c69b4-c5f7-420c-ad9a-f35cab7bd0cf"),
                            AirlineName = "Belavia",
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            Rating = 4.2999999999999998
                        },
                        new
                        {
                            Id = new Guid("96e1738a-568d-4a23-be7d-f2f3382f1cf9"),
                            AirlineName = "American Airlines",
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            Rating = 4.7000000000000002
                        },
                        new
                        {
                            Id = new Guid("b01e92b9-2c8b-4437-b290-eae73e1017f0"),
                            AirlineName = "Airnorth",
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            Rating = 3.8999999999999999
                        });
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.AirplaneEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaximumSeats")
                        .HasColumnType("int");

                    b.Property<int>("MaximumWeight")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Airplanes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("44dcfa17-b97d-4bd9-828b-ab498f565c9d"),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            MaximumSeats = 525,
                            MaximumWeight = 46750,
                            ModelName = "ATR EVO"
                        },
                        new
                        {
                            Id = new Guid("b642faa8-115d-4eeb-88e6-74e7c5eeca7a"),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            MaximumSeats = 320,
                            MaximumWeight = 32400,
                            ModelName = "CRIAC CR929"
                        },
                        new
                        {
                            Id = new Guid("50331fce-d5f5-494e-97ba-b89909601281"),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            MaximumSeats = 180,
                            MaximumWeight = 22600,
                            ModelName = "Comac C919"
                        },
                        new
                        {
                            Id = new Guid("7793ab1b-7600-4734-8139-64d7ed41d367"),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            MaximumSeats = 125,
                            MaximumWeight = 35600,
                            ModelName = "Airbus A220"
                        },
                        new
                        {
                            Id = new Guid("032df96e-9d2b-4321-a8f0-c5ab1d062b76"),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            MaximumSeats = 150,
                            MaximumWeight = 22600,
                            ModelName = "Comac C919"
                        },
                        new
                        {
                            Id = new Guid("21e5d331-780f-439c-9060-96bf96d31e27"),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            MaximumSeats = 300,
                            MaximumWeight = 31000,
                            ModelName = "Airbus A350"
                        },
                        new
                        {
                            Id = new Guid("6f39bb24-d2ef-4383-872d-abb02751ca7f"),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            MaximumSeats = 525,
                            MaximumWeight = 46750,
                            ModelName = "ATR EVO"
                        });
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.BoardingPassEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BookingExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Prise")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isExpired")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("UserId");

                    b.ToTable("BoardingPasses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5b646986-d9f8-4cee-b080-c22cfa9115e9"),
                            BookingExpireDate = new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = "05-12-23",
                            CreatedTime = "12:17:3",
                            FlightId = new Guid("48c30f5d-cfef-428b-bfdf-a7650e7cec6f"),
                            Prise = 778m,
                            UserId = new Guid("5071c449-d4ed-4b35-9aff-8bbbda78be2b"),
                            isExpired = false
                        },
                        new
                        {
                            Id = new Guid("bada3bb6-0621-4f09-8ada-07bfcc0b4de3"),
                            BookingExpireDate = new DateTime(2023, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = "02-06-23",
                            CreatedTime = "08:43:17",
                            FlightId = new Guid("46797e84-5438-41e4-b36f-e68744fbf290"),
                            Prise = 1661m,
                            UserId = new Guid("5071c449-d4ed-4b35-9aff-8bbbda78be2b"),
                            isExpired = false
                        },
                        new
                        {
                            Id = new Guid("c2ccfc2c-76c5-454c-9ad7-a51127023ca2"),
                            BookingExpireDate = new DateTime(2023, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = "07-24-23",
                            CreatedTime = "19:36:16",
                            FlightId = new Guid("96209a9d-78f8-4d7f-9adb-3f8b4c747066"),
                            Prise = 1672m,
                            UserId = new Guid("6d0b4263-c3ab-4c84-8f42-7abda64789b9"),
                            isExpired = false
                        },
                        new
                        {
                            Id = new Guid("5ace6361-4908-4dff-8d6e-7201ae071ae3"),
                            BookingExpireDate = new DateTime(2023, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = "03-16-23",
                            CreatedTime = "15:22:48",
                            FlightId = new Guid("d687cb42-a4b4-4df9-ba34-634bd8c87983"),
                            Prise = 1401m,
                            UserId = new Guid("ed7a6b39-79e8-43a4-b7cf-96d0d16df0d3"),
                            isExpired = false
                        });
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.FlightEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AirlineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AirplaneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Arrival")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ArrivingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Departurer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId");

                    b.HasIndex("AirplaneId");

                    b.ToTable("Flights");

                    b.HasData(
                        new
                        {
                            Id = new Guid("48c30f5d-cfef-428b-bfdf-a7650e7cec6f"),
                            AirlineId = new Guid("3c8c69b4-c5f7-420c-ad9a-f35cab7bd0cf"),
                            AirplaneId = new Guid("44dcfa17-b97d-4bd9-828b-ab498f565c9d"),
                            Arrival = "Boston",
                            ArrivingDate = new DateTime(2023, 2, 19, 18, 20, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            DepartureDate = new DateTime(2023, 2, 17, 22, 15, 0, 0, DateTimeKind.Unspecified),
                            Departurer = "Minsk"
                        },
                        new
                        {
                            Id = new Guid("46797e84-5438-41e4-b36f-e68744fbf290"),
                            AirlineId = new Guid("3c8c69b4-c5f7-420c-ad9a-f35cab7bd0cf"),
                            AirplaneId = new Guid("b642faa8-115d-4eeb-88e6-74e7c5eeca7a"),
                            Arrival = "Paris",
                            ArrivingDate = new DateTime(2023, 5, 13, 18, 55, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            DepartureDate = new DateTime(2023, 5, 13, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Departurer = "Pekin"
                        },
                        new
                        {
                            Id = new Guid("96209a9d-78f8-4d7f-9adb-3f8b4c747066"),
                            AirlineId = new Guid("96e1738a-568d-4a23-be7d-f2f3382f1cf9"),
                            AirplaneId = new Guid("50331fce-d5f5-494e-97ba-b89909601281"),
                            Arrival = "Mykonos",
                            ArrivingDate = new DateTime(2023, 3, 23, 11, 15, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            DepartureDate = new DateTime(2023, 3, 20, 20, 55, 0, 0, DateTimeKind.Unspecified),
                            Departurer = "Singapore"
                        },
                        new
                        {
                            Id = new Guid("d687cb42-a4b4-4df9-ba34-634bd8c87983"),
                            AirlineId = new Guid("b01e92b9-2c8b-4437-b290-eae73e1017f0"),
                            AirplaneId = new Guid("7793ab1b-7600-4734-8139-64d7ed41d367"),
                            Arrival = "yaunde",
                            ArrivingDate = new DateTime(2023, 7, 6, 3, 50, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            DepartureDate = new DateTime(2023, 7, 3, 6, 50, 0, 0, DateTimeKind.Unspecified),
                            Departurer = "frankfurt"
                        });
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.PersonInformationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Citizenship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirePasportDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("PersonInformations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("257fa5d1-d5b0-4faf-af1a-0185b3cfefab"),
                            BirthDate = new DateTime(1983, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Citizenship = "Belarus",
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            ExpirePasportDate = new DateTime(2027, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = "М",
                            IdentificationNumber = "2763984J836PB3",
                            Name = "Александр",
                            Surname = "Иванов",
                            UserId = new Guid("5071c449-d4ed-4b35-9aff-8bbbda78be2b")
                        },
                        new
                        {
                            Id = new Guid("05b231a2-c31d-4dfc-955a-6097102b1456"),
                            BirthDate = new DateTime(1993, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Citizenship = "America",
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            ExpirePasportDate = new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = "М",
                            IdentificationNumber = "2763984J836PB3",
                            Name = "Владислав",
                            Surname = "Лазарев",
                            UserId = new Guid("6d0b4263-c3ab-4c84-8f42-7abda64789b9")
                        },
                        new
                        {
                            Id = new Guid("e72b6d0c-a0ba-471a-bf43-cabca8ebbaf6"),
                            BirthDate = new DateTime(1998, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Citizenship = "Poland",
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17",
                            ExpirePasportDate = new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = "Ж",
                            IdentificationNumber = "2763984J836PB3",
                            Name = "Мария",
                            Surname = "Лебедева",
                            UserId = new Guid("ed7a6b39-79e8-43a4-b7cf-96d0d16df0d3")
                        });
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.UsersEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedTime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5071c449-d4ed-4b35-9aff-8bbbda78be2b"),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17"
                        },
                        new
                        {
                            Id = new Guid("6d0b4263-c3ab-4c84-8f42-7abda64789b9"),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17"
                        },
                        new
                        {
                            Id = new Guid("ed7a6b39-79e8-43a4-b7cf-96d0d16df0d3"),
                            CreatedDate = "01-25-23",
                            CreatedTime = "12:4:17"
                        });
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.BoardingPassEntity", b =>
                {
                    b.HasOne("FlightBooking.Domain.Entities.FlightEntity", "Flight")
                        .WithMany("BoardingPasses")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightBooking.Domain.Entities.UsersEntity", "User")
                        .WithMany("BoardingPasses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.FlightEntity", b =>
                {
                    b.HasOne("FlightBooking.Domain.Entities.AirlineEntity", "Airlines")
                        .WithMany("Flights")
                        .HasForeignKey("AirlineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlightBooking.Domain.Entities.AirplaneEntity", "Airplane")
                        .WithMany("Flights")
                        .HasForeignKey("AirplaneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airlines");

                    b.Navigation("Airplane");
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.PersonInformationEntity", b =>
                {
                    b.HasOne("FlightBooking.Domain.Entities.UsersEntity", "User")
                        .WithOne("PersonInformation")
                        .HasForeignKey("FlightBooking.Domain.Entities.PersonInformationEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.AirlineEntity", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.AirplaneEntity", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.FlightEntity", b =>
                {
                    b.Navigation("BoardingPasses");
                });

            modelBuilder.Entity("FlightBooking.Domain.Entities.UsersEntity", b =>
                {
                    b.Navigation("BoardingPasses");

                    b.Navigation("PersonInformation");
                });
#pragma warning restore 612, 618
        }
    }
}