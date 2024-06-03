using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;

namespace PillPal.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<(bool, string roleId)> CreateRoleAsync(string roleName)
    {
        var roleExist = await _roleManager.FindByNameAsync(roleName);

        if (roleExist != null)
        {
            return (false, roleExist.Id.ToString());
        }

        var role = new IdentityRole<Guid>(roleName);

        var result = await _roleManager.CreateAsync(role);

        return (result.Succeeded, role.Id.ToString());
    }

    public async Task<(bool, string UserId)> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName
        };

        await _userManager.AddPasswordAsync(user, password);

        await _userManager.AddToRoleAsync(user, Role.Customer);

        var result = await _userManager.CreateAsync(user, password);

        return (result.Succeeded, user.Id.ToString());
    }

    public async Task<(ApplicationUser, string role)> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = email,
                Email = email
            });

            user = await _userManager.FindByEmailAsync(email);

            await _userManager.AddToRoleAsync(user, Role.Customer);
        }

        var role = await _userManager.GetRolesAsync(user)
            .ContinueWith(task => task.Result.FirstOrDefault());

        return (user, role);
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<(ApplicationUser, string role)> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), email);
        }

        var result = await _userManager.CheckPasswordAsync(user, password);

        if (!result)
        {
            throw new Exception("Invalid password");
        }

        var role = await _userManager.GetRolesAsync(user)
            .ContinueWith(task => task.Result.FirstOrDefault());

        return (user, role);
    }
}
