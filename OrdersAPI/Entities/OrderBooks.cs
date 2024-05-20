using System.Text.Json.Serialization;

namespace OrdersAPI.Entities;

public class OrderBooks
{
    public Guid OrderId { get; set; }
    [JsonIgnore]
    public OrderModel Order { get; set; }
    public Guid BookId { get; set; }
    public BookModel Book { get; set; }
    public int Amount { get; set; }

}