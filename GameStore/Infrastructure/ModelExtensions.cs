using GameStore.Data.Models;
using GameStore.Data.ViewModels;

namespace GameStore.Infrastructure;

public static class ModelExtensions
{
    public static void Update(this Game game, GameViewModel model)
    {
        game.Name = model.Name;
        game.Price = model.Price;
    }
}