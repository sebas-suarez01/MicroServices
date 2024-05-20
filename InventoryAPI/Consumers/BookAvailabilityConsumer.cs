using Contracts;
using InventoryAPI.Database;
using InventoryAPI.Entities;
using MassTransit;

namespace InventoryAPI.Consumers;

public class BookAvailabilityConsumer : IConsumer<GetBookAvailabilityRequest>
{
    private readonly InventoryDbContext _context;

    public BookAvailabilityConsumer(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<GetBookAvailabilityRequest> context)
    {
        var book = await _context.Set<BookModel>().FindAsync(context.Message.BookId);

        if (book is null)
        {
            await context.RespondAsync(new BookNotFound(context.Message.BookId));
            return;
        }

        if (book.Amount - context.Message.Amount < 0)
        {
            await context.RespondAsync(new GetBookAvailabilityResponse(false, book.Name, book.Price));
        }

        await context.RespondAsync(new GetBookAvailabilityResponse(true, book.Name, book.Price));
    }
}