﻿using PillPal.Core.Identity;

namespace PillPal.Service.Identities;

public interface IUserService
{
    Task<dynamic> RegisterAsync(string token);
    Task<dynamic> RegisterAsync(dynamic request);
    Task<dynamic> LoginAsync(string token);
    Task<IEnumerable<ApplicationUser>> GetAll();
}
