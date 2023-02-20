namespace GameStore.Data.Models;

public class Order
{
    // TODO: Create Order model, See if this is the correct way to store in database.
    public int Id { get; set; }
    public List<Cart.CartItem> Items { get; set; } = new();
}