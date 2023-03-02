using GameStore.Data.Identity;
using GameStore.Data.Models;
using GameStore.Data.ViewModels;
using Microsoft.AspNetCore.Identity;

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

    /// <summary>
    /// Returns the <see cref="ApplicationUser"/> whose id matches <paramref name="userId"/>
    /// </summary>
    /// <param name="userId">The <see cref="ApplicationUser"/> ID </param>
    /// <returns></returns>
    public Task<ApplicationUser?> GetUser(Guid userId);

    public Task<IdentityResult> UpdateUser(UserViewModel userModel);
}