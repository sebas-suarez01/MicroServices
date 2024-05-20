using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Common;
using OrdersAPI.Database;
using OrdersAPI.Entities;
using OrdersAPI.Interfaces;

namespace OrdersAPI.Services;

public class OrderService(OrderDbContext context, IBookInventoryService inventoryService, IPublishEndpoint publishEndpoint) : IOrderService
{
    private readonly OrderDbContext _context = context;
    private readonly IBookInventoryService _inventoryService = inventoryService;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task<Guid> AddOrder(OrderRequest request)
    {
        decimal totalPrice = 0;
        foreach (var book in request.Books)
        {
            var response = await _inventoryService.BookAvailability(book.BookId, book.Amount);

            if (!response.IsAvailable)
            {
                return Guid.Empty;
            }

            totalPrice += response.Price*book.Amount;
        }

        var orderId = Guid.NewGuid();

        var orderBooks = request.Books
            .Select(b =>
                new OrderBooks()
                {
                    BookId = b.BookId,
                    OrderId = orderId,
                    Amount = b.Amount
                })
            .ToList();

        var order = new OrderModel()
        {
            Date = DateTime.UtcNow,
            Id = orderId,
            UserId = Guid.NewGuid(),
            OrderBooks = orderBooks,
            TotalPrice = totalPrice

        };
        
        await _context.Set<OrderModel>().AddAsync(order);
        await _context.SaveChangesAsync();
        
        foreach (var book in request.Books)
        {
            await _publishEndpoint.Publish(new BookBoughtEvent(book.BookId, book.Amount, DateTime.UtcNow));
        }

        return order.Id;
    }

    public Task<List<OrderModel>> GetAll()
    {
        return _context.Set<OrderModel>().ToListAsync();
    }
}