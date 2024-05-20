using InventoryAPI.Primitives;

namespace InventoryAPI.Entities;

public class BookModel : IAuditable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AuthorName { get; set; }
    public int PublishedYear { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUTc { get; set; }
}