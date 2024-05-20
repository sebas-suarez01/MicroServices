using CatalogAPI.Database;
using CatalogAPI.Entities;
using Contracts;
using MassTransit;

namespace CatalogAPI.Consumers;

public class BookCreatedConsumer : IConsumer<BookCreatedEvent>
{
    private readonly CatalogDbContext _context;

    public BookCreatedConsumer(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<BookCreatedEvent> context)
    {
        var book = new BookModel()
        {
            Id = context.Message.BookId,
            AuthorName = context.Message.AuthorName,
            Name = context.Message.Name,
            OnStock = true,
            Price = context.Message.Price,
            PublishedYear = context.Message.Year,
            Amount = context.Message.Amount
        };
        
        await _context.Set<BookModel>().AddAsync(book);

        await _context.SaveChangesAsync();
    }
}