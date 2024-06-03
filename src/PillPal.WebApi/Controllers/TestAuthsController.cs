using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillPal.Core.Constant;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestAuthsController : ControllerBase
{
    /// <summary>
    /// Allow only admin role
    /// </summary>
    [HttpGet("admin-only")]
    [AuthorizeRoles(Role.Admin)]
    public string TestAdmin()
    {
        return "admin verified";
    }

    /// <summary>
    /// Allow only customer role
    /// </summary>
    [HttpGet("customer-only")]
    [AuthorizeRoles(Role.Customer)]
    public string TestCustomer()
    {
        return "customer verified";
    }

    /// <summary>
    /// Allow only manager role
    /// </summary>
    [HttpGet("manager-only")]
    [AuthorizeRoles(Role.Manager)]
    public string TestManager()
    {
        return "manager verified";
    }

    /// <summary>
    /// Allow both admin and customer role
    /// </summary>
    [HttpGet("both-admin-customer")]
    [AuthorizeRoles(Role.Admin, Role.Customer)]
    public string TestAdminCus()
    {
        return "both admin and customer allow";
    }

    /// <summary>
    /// Allow both admin and manager role
    /// </summary>
    [HttpGet("both-admin-manager")]
    [AuthorizeRoles(Role.Admin, Role.Manager)]
    public string TestAdminMng()
    {
        return "both admin and manager allow";
    }

    /// <summary>
    /// Allow all roles admin, manager and customer
    /// </summary>
    [HttpGet("all-three")]
    [AuthorizeRoles(Role.Admin, Role.Manager, Role.Customer)]
    public string TestAllThreee()
    {
        return "all roles admin, manager and customer allow";
    }

    /// <summary>
    /// Allow annonymous user (non-login user)
    /// </summary>
    [HttpGet("annonymous")]
    [AllowAnonymous]
    public string TestBoth()
    {
        return "non-login user allow";
    }
}
