using System.Text.Json.Serialization;
using GameStore.Data.Models;
using GameStore.Infrastructure;

namespace GameStore.Data;

/// <summary>
/// <see cref="Cart"/> that uses Session storage
/// </summary>
public class SessionCart : Cart
{
    /// <summary>
    /// The Session reference
    /// </summary>
    [JsonIgnore]
    public ISession? Session { get; private set; }

    public static Cart GetCart(IServiceProvider serviceProvider)
    {
        var session = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
        var cart = session?.GetObject<SessionCart>(nameof(Cart)) ?? new SessionCart();
        cart.Session = session;
        return cart;
    }

    
    public override void AddItem(Game game, int quantity)
    {
        base.AddItem(game, quantity);
        Session?.SetJson(nameof(Cart), this);
    }

    public override void RemoveItem(Game game)
    {
        base.RemoveItem(game);
        Session?.SetJson(nameof(Cart), this);
    }
    public override void Clear()
    {
        base.Clear();
        Session?.Remove(nameof(Cart));
    }
}