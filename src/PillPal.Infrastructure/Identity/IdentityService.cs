﻿using PillPal.Application.Features.Accounts;

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

    public async Task AssignManagerAsync(AssignManagerRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, request.Password!);

        if (!result.Succeeded)
        {
            throw new ConflictException(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, Role.Manager);
    }

    public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var result = await _userManager.ChangePasswordAsync(user!, currentPassword, newPassword);

        if (!result.Succeeded)
        {
            throw new ConflictException(result.Errors);
        }
    }

    public async Task CreatePasswordAsync(string userId, string password)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (await _userManager.HasPasswordAsync(user!))
        {
            throw new BadRequestException("Password already created");
        }

        var result = await _userManager.AddPasswordAsync(user!, password);

        if (!result.Succeeded)
        {
            throw new ConflictException(result.Errors);
        }
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

    public async Task<string> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            throw new ConflictException(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, Role.Customer);

        return user.Id.ToString();
    }

    public async Task<ApplicationUser> GetAccountAsync(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString())
            ?? throw new NotFoundException(nameof(ApplicationUser), id);
    }

    public async Task<IEnumerable<ApplicationUser>> GetAccountsAsync(string role)
    {
        return await _userManager.GetUsersInRoleAsync(role);
    }

    public async Task<(ApplicationUser user, string role, bool newUser)> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        bool newUser = false;

        if (user == null)
        {
            await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = email,
                Email = email
            });

            user = await _userManager.FindByEmailAsync(email);

            await _userManager.AddToRoleAsync(user!, Role.Customer);

            newUser = true;
        }

        // check if user is locked
        await CheckAccountLockoutAsync(user!);

        var role = await _userManager.GetRolesAsync(user!)
            .ContinueWith(task => task.Result.FirstOrDefault());

        return (user!, role!, newUser);
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

    public async Task LockAccountAsync(Guid userId, DateTimeOffset lockoutEnd)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (await _userManager.IsLockedOutAsync(user!))
        {
            throw new BadRequestException("Account is already locked");
        }

        await _userManager.SetLockoutEndDateAsync(user!, lockoutEnd);
    }

    public async Task<(ApplicationUser user, string role)> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email)
            ?? throw new UnauthorizedAccessException($"User with identifier '{email}' not found");

        // check if user is locked
        await CheckAccountLockoutAsync(user);

        var result = await _userManager.CheckPasswordAsync(user, password);

        if (!result)
        {
            // if user lockout is enabled
            // increment access failed count
            if (await _userManager.GetLockoutEnabledAsync(user))
            {
                // on failed login attempt, increment access failed count
                await _userManager.AccessFailedAsync(user);

                throw new UnauthorizedAccessException("Invalid password");
            }

            throw new UnauthorizedAccessException("Invalid password");
        }

        // reset access failed count
        await _userManager.ResetAccessFailedCountAsync(user);

        var role = await _userManager.GetRolesAsync(user)
            .ContinueWith(task => task.Result.FirstOrDefault());

        return (user!, role!);
    }

    private async Task CheckAccountLockoutAsync(ApplicationUser user)
    {
        if (await _userManager.IsLockedOutAsync(user))
        {
            var lockedOutEnd = await _userManager.GetLockoutEndDateAsync(user);

            throw new UnauthorizedAccessException($"User is locked out until {lockedOutEnd.Value.LocalDateTime}");
        }
    }

    public async Task UnlockAccountAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (!await _userManager.IsLockedOutAsync(user!))
        {
            throw new BadRequestException("Account is not locked");
        }

        await _userManager.SetLockoutEndDateAsync(user!, null);
    }

    public async Task UpdateManagerInformationAsync(string userId, UpdateManagerInformationDto request)
    {
        var user = await _userManager.FindByIdAsync(userId);

        user!.PhoneNumber = request.PhoneNumber;

        await _userManager.UpdateAsync(user);
    }
}
