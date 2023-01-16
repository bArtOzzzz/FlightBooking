using FlightBooking.Application.Abstractions.IRepository;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Queries;
using FlightBooking.Infrastructure.Repository;
using FlightBooking.Application.Services;
using FlightBooking.Application.Mapper;
using Microsoft.EntityFrameworkCore;
using FlightBooking.Application.Dto;
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

builder.Services.AddScoped<IAirlineRepository, AirlineRepository>();
builder.Services.AddScoped<IAirlineService, AirlineService>();

builder.Services.AddScoped<IValidator<AirlineDto>, AirlineModelValidator>();

builder.Services.AddSwaggerGen();

// Add Fluent Validation
builder.Services.AddFluentValidationClientsideAdapters();

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
