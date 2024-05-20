using OrdersAPI.Common;
using OrdersAPI.Interfaces;

namespace OrdersAPI.Endpoints;

public static class OrderEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("orders", Create)
            .WithName(nameof(Create));
        app.MapGet("orders", GetAll)
            .WithName(nameof(GetAll));
    }
    public static async Task<IResult> GetAll(IOrderService service)
    {
        var orders = await service.GetAll();
        return Results.Ok(orders);
    }
    public static async Task<IResult> Create(OrderRequest request, IOrderService service)
    {
        var orderId = await service.AddOrder(request);

        return orderId == Guid.Empty ? Results.Conflict(orderId) : Results.Ok(orderId);
    }
}