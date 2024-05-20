using Contracts;
using InventoryAPI.Database;
using InventoryAPI.Entities;
using MassTransit;

namespace InventoryAPI.Consumers;

public class BookBoughtEventConsumer : IConsumer<BookBoughtEvent>
{
    private readonly InventoryDbContext _context;
    private readonly ILogger<BookBoughtEventConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public BookBoughtEventConsumer(InventoryDbContext context, ILogger<BookBoughtEventConsumer> logger, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<BookBoughtEvent> context)
    {
        var book = await _context.Set<BookModel>().FindAsync(context.Message.BookId);

        if (book is null)
        {
            _logger.LogError($"Book Id {context.Message.BookId} not exits");
            return;
        }
        
        book.Amount -= context.Message.Amount;
        await _context.SaveChangesAsync();
        
        await _publishEndpoint.Publish(new BookStockUpdateEvent(book.Id, book.Amount));
    }
    
}