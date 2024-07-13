﻿global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using PillPal.Application.Common.Exceptions;
global using PillPal.Application.Common.Interfaces.Auth;
global using PillPal.Application.Common.Interfaces.Cache;
global using PillPal.Application.Common.Interfaces.Data;
global using PillPal.Application.Common.Interfaces.File;
global using PillPal.Application.Common.Interfaces.Services;
global using PillPal.Core.Common;
global using PillPal.Core.Constant;
global using PillPal.Core.Identity;
global using PillPal.Core.Models;
global using System.Data;
global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using System.Text.Json;
