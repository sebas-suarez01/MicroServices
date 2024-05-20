using OrdersAPI.Common;
using OrdersAPI.Entities;

namespace OrdersAPI.Interfaces;

public interface IOrderService
{
    Task<Guid> AddOrder(OrderRequest request);
    Task<List<OrderModel>> GetAll();
}