using AcademiaNet.Backend.Data;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AcademiaNet.Backend.Repositories.Implementations;

public class UsersRepository : IUsersRepository
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;

    public UsersRepository(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    /// <summary>
    /// Add User Async
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<IdentityResult> AddUserAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    /// <summary>
    ///  Add User To Role Async
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public async Task AddUserToRoleAsync(User user, string roleName)
    {
        await _userManager.AddToRoleAsync(user, roleName);
    }

    /// <summary>
    /// Check Role Async
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public async Task CheckRoleAsync(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleName
            });
        }
    }

    /// <summary>
    /// Get User Async
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<User> GetUserAsync(string email)
    {
        var user = await _context.Users
            .Include(u => u.Institution)
            .FirstOrDefaultAsync(x => x.Email == email);
        return user!;
    }

    /// <summary>
    /// Is User In Role As ync
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public async Task<bool> IsUserInRoleAsync(User user, string roleName)
    {
        return await _userManager.IsInRoleAsync(user, roleName);
    }

    /// <summary>
    /// Login Async
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<SignInResult> LoginAsync(LoginDTO model)
    {
        return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
    }

    /// <summary>
    /// Logout Async
    /// </summary>
    /// <returns></returns>
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    /// <summary>
    /// Get User Async
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<User> GetUserAsync(Guid userId)
    {
        var user = await _context.Users
            .Include(u => u.Institution)
            .FirstOrDefaultAsync(x => x.Id == userId.ToString());
        return user!;
    }

    /// <summary>
    /// Generate Email Confirmation Token Async
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    /// <summary>
    /// Confirm Email Async
    /// </summary>
    /// <param name="user"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
    {
        return await _userManager.ConfirmEmailAsync(user, token);
    }

    /// <summary>
    ///Change PasswordAsync
    /// </summary>
    /// <param name="user"></param>
    /// <param name="currentPassword"></param>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    /// <summary>
    ///Update UserAsync
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<IdentityResult> UpdateUserAsync(User user)
    {
        return await _userManager.UpdateAsync(user);
    }
}