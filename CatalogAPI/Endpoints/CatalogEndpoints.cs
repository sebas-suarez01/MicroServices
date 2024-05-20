using CatalogAPI.Interfaces;

namespace CatalogAPI.Endpoints;

public static class CatalogEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("catalog", GetAll)
            .WithName(nameof(GetAll));
    }
    public static async Task<IResult> GetAll(ICatalogService service)
    {
        var catalog = await service.GetAll();
        return Results.Ok(catalog);
    }
}