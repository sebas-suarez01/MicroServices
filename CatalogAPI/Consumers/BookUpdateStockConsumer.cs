using CatalogAPI.Database;
using CatalogAPI.Entities;
using Contracts;
using MassTransit;

namespace CatalogAPI.Consumers;

public class BookUpdateStockConsumer : IConsumer<BookStockUpdateEvent>
{
    private readonly CatalogDbContext _context;
    private readonly ILogger<BookUpdateStockConsumer> _logger;

    public BookUpdateStockConsumer(CatalogDbContext context, ILogger<BookUpdateStockConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BookStockUpdateEvent> context)
    {
        var book = await _context.Set<BookModel>().FindAsync(context.Message.BookId);

        if (book is null)
        {
            _logger.LogError($"Book Id {context.Message.BookId} not exits");
            return;
        }
        
        book.Amount = context.Message.Amount;
        book.OnStock = book.Amount > 0;

        await _context.SaveChangesAsync();

    }
}