using FlightBooking.Application.Abstractions.IRepository;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Queries;
using FlightBooking.Infrastructure.Repository;
using FlightBooking.Application.Services;
using FlightBooking.Application.Mapper;
using FlightBooking.API.Models.Request;
using Microsoft.EntityFrameworkCore;
using FlightBooking.Infrastructure;
using FlightBooking.API.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Enable AutoMapper
builder.Services.AddAutoMapper(typeof(AirlineMapper).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add Fluent Validation
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddStashbox();

// DI for custom services
builder.Host.UseStashbox(container => // Optional configuration options.
{
    // This one enables the lifetime validation for production environments too.
    container.Configure(config => config.WithLifetimeValidation());

    container.RegisterScoped<IAirlineRepository, AirlineRepository>();
    container.RegisterScoped<IAirlineService, AirlineService>();
});

builder.Services.AddScoped<IValidator<AirlineCreateOrUpdate>, AirlineModelValidator>();

builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(GetAllAirlinesQuery).Assembly);
 
// Database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["DefaultConnectionToLocalDatabase"], b => b.MigrationsAssembly("FlightBooking.Infrastructure"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
