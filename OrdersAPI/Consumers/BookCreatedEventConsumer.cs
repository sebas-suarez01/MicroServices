using Contracts;
using MassTransit;
using OrdersAPI.Database;
using OrdersAPI.Entities;

namespace OrdersAPI.Consumers;

public class BookCreatedEventConsumer : IConsumer<BookCreatedEvent>
{
    private readonly OrderDbContext _context;

    public BookCreatedEventConsumer(OrderDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<BookCreatedEvent> context)
    {
        var book = new BookModel()
        {
            Id = context.Message.BookId,
            Name = context.Message.Name
        };

        await _context.Set<BookModel>().AddAsync(book);
        await _context.SaveChangesAsync();
    }
}