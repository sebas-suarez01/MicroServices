using CatalogAPI.Endpoints;
using CatalogAPI.Extensions;
using CatalogAPI.Interfaces;
using CatalogAPI.Services;
using CatalogAPI.Settings;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbConfiguration(builder.Configuration);
builder.Services.AddScoped<ICatalogService, CatalogService>();

builder.Services.AddMessaging(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapEndpoints();
app.Run();