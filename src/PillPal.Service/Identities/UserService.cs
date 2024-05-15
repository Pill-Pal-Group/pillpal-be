
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PillPal.Core.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace PillPal.Service.Identities;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAll()
    {
        var list = await _userManager.Users.ToListAsync();
        return list;
    }

    public async Task<dynamic> LoginAsync(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        string email = jsonToken?.Claims.First(claim => claim.Type == "email").Value;

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return new
            {
                message = "User not found"
            };
        }

        return user;
    }

    public async Task<dynamic> LoginAsync(dynamic request)
    {
        var user = await _userManager.FindByEmailAsync(request.email);

        if (user == null)
        {
            return new
            {
                message = "User not found"
            };
        }

        var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.password);

        if (result == PasswordVerificationResult.Failed)
        {
            return new
            {
                message = "Invalid password"
            };
        }

        return true;
    }

    public async Task<dynamic> RegisterAsync(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

        string email = jsonToken?.Claims.First(claim => claim.Type == "email").Value;

        string username = jsonToken?.Claims.First(claim => claim.Type == "name").Value;

        var existedUser = await _userManager.FindByEmailAsync(email);

        if (existedUser != null)
        {
            return new
            {
                message = "User email already exists"
            };
        }


        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = email,
            UserName = email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            return new
            {
                message = "User created successfully"
            };
        }

        return new
        {
            message = "User creation failed"
        };
    }

    public async Task<dynamic> RegisterAsync(dynamic request)
    {
        var existedUser = await _userManager.FindByEmailAsync(request.email);

        if (existedUser != null)
        {
            return new
            {
                message = "User email already exists"
            };
        }

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = request.email,
            UserName = request.email,
            EmailConfirmed = true,
        };

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.password);

        var result = await _userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            return true;
        }

        return new
        {
            message = "User creation failed"
        };
    }
}
