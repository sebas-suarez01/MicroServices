namespace CatalogAPI.Entities;

public class BookModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AuthorName { get; set; }
    public int PublishedYear { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public bool OnStock { get; set; }
}