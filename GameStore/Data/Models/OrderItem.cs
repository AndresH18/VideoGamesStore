using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Models;

public class OrderItem
{
    public OrderItem()
    {
    }

    public OrderItem(Cart.CartItem cartItem)
    {
        Name = cartItem.Game.Name;
        Price = cartItem.Game.Price;
        Quantity = cartItem.Quantity;
    }

    [Key] public ulong Id { get; set; }

    public int OrderId { get; set; }

    [Required] public string Name { get; set; } = default!;

    public double Price { get; set; }

    public int Quantity { get; set; }
    
    public Order? Order { get; set; }
}