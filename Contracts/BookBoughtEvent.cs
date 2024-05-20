namespace Contracts;

public record BookBoughtEvent(Guid BookId, int Amount, DateTime Date)
{
}