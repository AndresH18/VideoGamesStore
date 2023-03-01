using GameStore.Data.Identity;
using GameStore.Data.Models;
using GameStore.Data.ViewModels;

namespace GameStore.Repository;

public interface IAdminRepository
{
    /// <summary>
    /// Returns a page of games
    /// </summary>
    /// <param name="pageIndex">The non zero number of the page</param>
    public Task<ListViewModel<Game>> GetProducts(int pageIndex);

    /// <summary>
    /// Returns a page of users
    /// </summary>
    /// <param name="pageIndex">The non zero number of the page</param>
    public Task<ListViewModel<ApplicationUser>> GetUsers(int pageIndex);
}