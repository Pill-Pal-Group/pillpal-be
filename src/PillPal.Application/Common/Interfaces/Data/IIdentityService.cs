namespace PillPal.Application.Common.Interfaces.Data;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<(bool, string UserId)> CreateUserAsync(string userName, string password);
}
