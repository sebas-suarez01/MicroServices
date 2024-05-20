using Contracts;
using InventoryAPI.Common;
using InventoryAPI.Database;
using InventoryAPI.Entities;
using InventoryAPI.Interfaces;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Services;

public class InventoryService(InventoryDbContext context, IPublishEndpoint publishEndpoint) : IInventoryService
{
    private readonly InventoryDbContext _context = context;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;


    public async Task<Guid> AddBook(BookRequest bookRequest)
    {
        var bookId = Guid.NewGuid();

        var book = new BookModel()
        {
            Id = bookId,
            Amount = bookRequest.Amount,
            AuthorName = bookRequest.AuthorName,
            CreatedOnUtc = DateTime.UtcNow,
            Name = bookRequest.Name,
            Price = bookRequest.Price,
            PublishedYear = bookRequest.PublishYear
        };

        await _context.Set<BookModel>().AddAsync(book);

        await _context.SaveChangesAsync();

        await _publishEndpoint.Publish(new BookCreatedEvent(bookId, book.Name, book.AuthorName, book.PublishedYear,
            book.Price, book.Amount));

        return bookId;
    }

    public async Task<List<BookModel>> GetAll()
    {
        return await _context.Set<BookModel>().ToListAsync();
    }

    public async Task<Guid> UpdateBookAmount(Guid bookId, int amount)
    {
        var book = await _context.Set<BookModel>().FindAsync(bookId);
        
        book.Amount += amount;

        await _context.SaveChangesAsync();

        return bookId;
    }
}