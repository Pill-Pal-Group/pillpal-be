using PillPal.Core.Identity;

namespace PillPal.Application.Common.Interfaces.Data;

public interface IIdentityService
{
    /// <summary>
    /// Get the username of the user with the given ID
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>Username or null if not found</returns>
    Task<string?> GetUserNameAsync(string userId);

    /// <summary>
    /// Check if user is in roles
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<bool> IsInRoleAsync(string userId, string role);

    /// <summary>
    /// Create a user with the given username and password
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns>
    /// <para>bool: True if the user was created successfully, false otherwise.</para>
    /// <para>string: The user's ID if the user was created successfully, null otherwise.</para>
    /// </returns>
    Task<(bool, string UserId)> CreateUserAsync(string userName, string password);

    /// <summary>
    /// Create new role, in case the role already exists, return the role ID
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns>
    /// <para>bool: True if the role was created successfully, false otherwise.</para>
    /// <para>string: New role ID if create successfully, or returns the role id if already exists</para>
    /// </returns>
    Task<(bool, string roleId)> CreateRoleAsync(string roleName);

    Task<(ApplicationUser, string role)> LoginAsync(string email, string password);
    Task<(ApplicationUser, string role)> GetUserByEmailAsync(string email);
}
