namespace InventoryAPI.Common;

public record BookRequest(string Name, string AuthorName, int PublishYear, int Amount, decimal Price);