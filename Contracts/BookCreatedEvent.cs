namespace Contracts;

public record BookCreatedEvent(Guid BookId, string Name, string AuthorName, int Year, decimal Price, int Amount);