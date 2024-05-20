namespace InventoryAPI.Primitives;

public interface IAuditable
{
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUTc { get; set; }
}