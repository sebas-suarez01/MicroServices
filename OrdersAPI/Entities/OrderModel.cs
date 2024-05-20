namespace OrdersAPI.Entities;

public class OrderModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Date { get; set; }
    public List<OrderBooks> OrderBooks { get; set; }
}