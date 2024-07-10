using PillPal.Application.Features.Accounts;
using PillPal.Core.Identity;

namespace PillPal.Application.Common.Interfaces.Data;

public interface IIdentityService
{
    /// <summary>
    /// Get the username of the user with the given ID
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <returns>Username or null if not found</returns>
    Task<string?> GetUserNameAsync(string userId);

    /// <summary>
    /// Check if user is in roles
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <param name="role">The role to check</param>
    /// <returns>True if the user is in the role, false otherwise</returns>
    Task<bool> IsInRoleAsync(string userId, string role);

    /// <summary>
    /// Create a user with the given username and password
    /// and assign the user to the role "Customer"
    /// </summary>
    /// <param name="userName">Username of the user</param>
    /// <param name="password">Password of the user</param>
    /// <returns>
    /// The user's ID if the user.
    /// </returns>
    /// <remarks>
    /// Be noted that the ID still created even if the user was not created successfully
    /// </remarks>
    /// <exception cref="ConflictException">Thrown when the user already exists</exception>
    Task<string> CreateUserAsync(string userName, string password);

    /// <summary>
    /// Create new role, in case the role already exists, return the role ID
    /// </summary>
    /// <param name="roleName">The name of the role</param>
    /// <returns>
    /// A tuple containing:
    /// <para>Item1 (bool): True if the role was created successfully, false otherwise.</para>
    /// <para>Item2 (string): New role ID if create successfully, or returns the role id if already exists</para>
    /// </returns>
    Task<(bool, string roleId)> CreateRoleAsync(string roleName);

    /// <summary>
    /// Login with the given email and password
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="password">The password of the user</param>
    /// <returns>
    /// A tuple containing:
    /// <para>Item1 (ApplicationUser): The user with the given email</para>
    /// <para>Item2 (string): The role of the user</para>
    /// </returns>
    /// <exception cref="UnauthorizedAccessException">Thrown when the email not found or password is incorrect</exception>
    Task<(ApplicationUser, string role)> LoginAsync(string email, string password);

    /// <summary>
    /// Get the user with the given email, in case not found, create a new user with default role "Customer"
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <returns>
    /// A tuple containing:
    /// <para>Item1 (ApplicationUser): The user with the given email</para>
    /// <para>Item2 (string): The role of the user</para>
    /// <para>Item3 (bool): True if the user is a newly created user, false otherwise</para>
    /// </returns>
    Task<(ApplicationUser, string role, bool newUser)> GetUserByEmailAsync(string email);

    /// <summary>
    /// Change the password of the user
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <param name="currentPassword">The current password of the user</param>
    /// <param name="newPassword">The new password of the user</param>
    /// <exception cref="ConflictException">Thrown if operation fail</exception>
    Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);

    /// <summary>
    /// Create a password for the user
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <param name="password">The password of the user</param>
    /// <exception cref="ConflictException">Thrown if operation failed</exception>
    /// <exception cref="BadRequestException">Thrown if the password already created</exception>
    Task CreatePasswordAsync(string userId, string password);

    /// <summary>
    /// Get the user with the given ID
    /// </summary>
    /// <param name="id">The ID of the user</param>
    /// <returns>The user with the given ID</returns>
    /// <exception cref="NotFoundException">Thrown if the user not found</exception>
    Task<ApplicationUser> GetAccountAsync(Guid id);

    /// <summary>
    /// Get all users with the given role
    /// </summary>
    /// <param name="role">The role of the users</param>
    /// <returns>The users with the given role</returns>
    Task<IEnumerable<ApplicationUser>> GetAccountsAsync(string role);

    /// <summary>
    /// Lock the account of the user with the given ID and lockout end time
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <param name="lockoutEnd">The end time of the lockout</param>
    /// <exception cref="BadRequestException">Thrown if the account is already locked</exception>
    Task LockAccountAsync(Guid userId, DateTimeOffset lockoutEnd);

    /// <summary>
    /// Unlock the account of the user with the given ID
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <exception cref="BadRequestException">Thrown if the account is already unlocked or not locked</exception>
    Task UnlockAccountAsync(Guid userId);

    /// <summary>
    /// Assign a manager to the customer with email and password
    /// </summary>
    /// <param name="request">The request object containing the email and password of the manager</param>
    /// <exception cref="ConflictException">Thrown if the email is already exists</exception>
    Task AssignManagerAsync(AssignManagerRequest request);

    /// <summary>
    /// Update the information of the manager
    /// </summary>
    /// <remarks>
    /// This will not check for user id existence,
    /// as given that the user is needed to be logged in to update the information.
    /// Therefore, the user ID is always valid
    /// </remarks>
    /// <param name="userId">The ID of the manager</param>
    /// <param name="request">The request object containing the information of the manager</param>
    Task UpdateManagerInformationAsync(string userId, UpdateManagerInformationDto request);
}
