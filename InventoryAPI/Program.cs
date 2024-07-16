using InventoryAPI.Endpoints;
using InventoryAPI.Extensions;
using InventoryAPI.Interfaces;
using InventoryAPI.Services;
using InventoryAPI.Settings;
using JwtAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCustomJwtAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbConfiguration(builder.Configuration);
builder.Services.AddMessaging(builder.Configuration);
builder.Services.AddScoped<IInventoryService, InventoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();
app.Run();