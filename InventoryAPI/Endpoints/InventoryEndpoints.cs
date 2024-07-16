using InventoryAPI.Common;
using InventoryAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Endpoints;

public static class InventoryEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("inventory", Create)
            .WithName(nameof(Create));
        app.MapGet("inventory", GetAll)
            .WithName(nameof(GetAll));
    }

    public static async Task<IResult> Create([FromBody]BookRequest request, IInventoryService service)
    {
        var bookId = await service.AddBook(request);
        return Results.Ok(bookId);
    }
    
    [Authorize]
    public static async Task<IResult> GetAll(IInventoryService service)
    {
        var books = await service.GetAll();
        return Results.Ok(books);
    }
}