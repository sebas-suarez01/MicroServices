namespace OrdersAPI.Interfaces;

public interface IBookInventoryService
{
    Task<(bool IsAvailable, string Name, decimal Price)> BookAvailability(Guid id, int amount);
}