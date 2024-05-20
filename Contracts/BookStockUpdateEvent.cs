namespace Contracts;

public record BookStockUpdateEvent(Guid BookId, int Amount);