namespace GameStore.Data.Models;

public class Cart
{
    public List<CartItem> CartItems { get; set; } = new();

    /// <summary>
    /// Add the <paramref name="game"/> to <see cref="CartItems"/>. If the <paramref name="game"/> doesn't exists, it is added to <see cref="CartItems"/>.
    /// If the <paramref name="game"/> exists, the <paramref name="quantity"/> is added to the item.
    /// </summary>
    /// <param name="game"><see cref="Game"/> to add to the <see cref="CartItems"/></param>
    /// <param name="quantity">The quantity to add</param>
    public virtual void AddItem(Game game, int quantity)
    {
        var cartItem = CartItems.FirstOrDefault(c => c.Game.Id == game.Id);
        if (cartItem == null)
        {
            CartItems.Add(new CartItem
            {
                Game = game, Quantity = quantity, /* Id = CartItems.Count + 1*/
            });
        }
        else
        {
            cartItem.Quantity += quantity;
        }
    }

    /// <summary>
    /// Removes the <paramref name="game"/> from the <see cref="CartItems"/>
    /// </summary>
    /// <param name="game">The <see cref="Game"/> to remove</param>
    public virtual void RemoveItem(Game game)
    {
        CartItems.RemoveAll(ci => ci.Game.Id == game.Id);
    }

    /// <summary>
    /// Remove all items from <see cref="CartItems"/>
    /// </summary>
    public virtual void Clear() => CartItems.Clear();

    /// <summary>
    /// Calculates the total value of the items in the <see cref="CartItems"/>
    /// </summary>
    /// <returns>The total value of <see cref="CartItems"/></returns>
    public double ComputeTotalValue() => CartItems.Sum(i => i.Game.Price * i.Quantity);

    /// <summary>
    /// Represents an item in a Cart
    /// </summary>
    public class CartItem
    {
        // /// <summary>
        // /// Cart Item Id
        // /// </summary>
        // public int Id { get; set; }

        /// <summary>
        /// The <see cref="Game"/> item
        /// </summary>
        public required Game Game { get; set; }

        /// <summary>
        /// The quantity of the item
        /// </summary>
        public int Quantity { get; set; }
    }
}