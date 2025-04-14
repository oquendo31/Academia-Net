using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using Microsoft.AspNetCore.Identity;

namespace AcademiaNet.Backend.UnitsOfWork.Interfaces;

public interface IUsersUnitOfWork
{
    /// <summary>
    /// Get User Async
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<User> GetUserAsync(string email);

    /// <summary>
    /// Add User Async
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<IdentityResult> AddUserAsync(User user, string password);

    /// <summary>
    /// Check Role Async
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    Task CheckRoleAsync(string roleName);

    /// <summary>
    /// Add User To Role Async
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    Task AddUserToRoleAsync(User user, string roleName);

    /// <summary>
    /// Is User In Role Async
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    Task<bool> IsUserInRoleAsync(User user, string roleName);

    /// <summary>
    /// Login Async
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<SignInResult> LoginAsync(LoginDTO model);

    /// <summary>
    /// Logout Async
    /// </summary>
    /// <returns></returns>
    Task LogoutAsync();
}