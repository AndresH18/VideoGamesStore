using GameStore.Data.Models;
using GameStore.Repository;

namespace GameStore.Services;

public class OrderService
{
    private readonly IGamesRepository _repo;

    public OrderService(IGamesRepository repo)
    {
        _repo = repo;
    }

    public Task<Game?> GetGame(int gameId)
    {
        return _repo.GetGame(gameId);
    }

    public async Task<Order> SaveOrder(Order order, Cart cart)
    {
        order.OrderItems = cart.CartItems.Select(g => new OrderItem(g)).ToList();
        await _repo.SaveOrder(order);
        return order;
    }
}