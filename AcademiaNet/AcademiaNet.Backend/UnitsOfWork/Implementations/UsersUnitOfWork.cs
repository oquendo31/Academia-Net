using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using Microsoft.AspNetCore.Identity;

namespace AcademiaNet.Backend.UnitsOfWork.Implementations;

public class UsersUnitOfWork : IUsersUnitOfWork
{
    private readonly IUsersRepository _usersRepository;

    public UsersUnitOfWork(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    /// <summary>
    /// Add User Async
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<IdentityResult> AddUserAsync(User user, string password) => await _usersRepository.AddUserAsync(user, password);

    /// <summary>
    /// Add User To Role Async
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public async Task AddUserToRoleAsync(User user, string roleName) => await _usersRepository.AddUserToRoleAsync(user, roleName);

    /// <summary>
    /// Check Role Async
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public async Task CheckRoleAsync(string roleName) => await _usersRepository.CheckRoleAsync(roleName);

    /// <summary>
    /// Get User Async
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<User> GetUserAsync(string email) => await _usersRepository.GetUserAsync(email);

    /// <summary>
    /// Is User In Role Async
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public async Task<bool> IsUserInRoleAsync(User user, string roleName) => await _usersRepository.IsUserInRoleAsync(user, roleName);

    /// <summary>
    /// Login Async
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<SignInResult> LoginAsync(LoginDTO model) => await _usersRepository.LoginAsync(model);

    /// <summary>
    /// Logout Async
    /// </summary>
    /// <returns></returns>
    public async Task LogoutAsync() => await _usersRepository.LogoutAsync();

    /// <summary>
    /// Get User Async
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<User> GetUserAsync(Guid userId) => await _usersRepository.GetUserAsync(userId);

    /// <summary>
    /// Generate Email Confirmation Token Async
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<string> GenerateEmailConfirmationTokenAsync(User user) => await _usersRepository.GenerateEmailConfirmationTokenAsync(user);

    /// <summary>
    /// Confirm Email Async
    /// </summary>
    /// <param name="user"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<IdentityResult> ConfirmEmailAsync(User user, string token) => await _usersRepository.ConfirmEmailAsync(user, token);
}