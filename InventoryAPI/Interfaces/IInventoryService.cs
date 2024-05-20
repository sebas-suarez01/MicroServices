using InventoryAPI.Common;
using InventoryAPI.Entities;

namespace InventoryAPI.Interfaces;

public interface IInventoryService
{
    Task<Guid> AddBook(BookRequest bookRequest);
    Task<List<BookModel>> GetAll();
    Task<Guid> UpdateBookAmount(Guid bookId, int amount);
}