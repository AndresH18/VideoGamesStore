using System.Security.Claims;
using GameStore.Data.Identity;
using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Repository;

public interface IAdminRepository
{
    /// <summary>
    /// Returns a page of games of a specific genre. If <paramref name="genreId"/> is 0, returns a games without being filtered by a category.
    /// </summary>
    /// <param name="pageNumber">The non zero number of the page</param>
    /// <param name="genreId">The id of the genre, if 0 then doesn't filter by category</param>
    public Task<GamesListViewModel> GetProducts(int pageNumber, int genreId);

    public Task<Game?> GetProduct(int gameId);

    public Task DeleteProduct(int gameId);

    public Task UpdateProduct(GameViewModel model);
    public Task CreateProduct(GameViewModel model);

    /// <summary>
    /// Returns a page of users
    /// </summary>
    /// <param name="pageIndex">The non zero number of the page</param>
    public Task<ListViewModel<ApplicationUser>> GetUsers(int pageIndex);

    /// <summary>
    /// Returns the <see cref="ApplicationUser"/> whose id matches <paramref name="userId"/>
    /// </summary>
    /// <param name="userId">The <see cref="ApplicationUser"/> ID </param>
    /// <returns></returns>
    public Task<ApplicationUser?> GetUser(Guid userId);

    public Task<IdentityResult> UpdateUser(UserViewModel userModel);
    public Task<IdentityResult> DeleteUser(Guid userId, ClaimsPrincipal currentUser);

    // public Task<IdentityResult> VerifyUser(Guid userid);
    /// <summary>
    /// Returns a page of <see cref="Order"/>. The page contains shipped, unshipped or both types of orders
    /// <param name="pageIndex">The non zero number of the page</param>
    /// <param name="filter">The category to filter the orders, should be either <b>shipped</b>, <b>unshipped</b>, or ""(empty).</param>
    /// <returns></returns>
    /// </summary>
    public Task<ListViewModel<Order>> GetOrders(int pageIndex, string filter);

    public Task Ship(int orderId);
}