using JwtAuthenticationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot();
builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();

app.UseHttpsRedirection();

// Ensure Ocelot middleware is added here
app.UseOcelot().Wait();

app.UseAuthentication();
app.UseAuthorization();

app.Run();