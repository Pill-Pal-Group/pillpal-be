using Microsoft.AspNetCore.Mvc;
using PillPal.Core.Constant;
using PillPal.WebApi.Service;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestAuthsController : ControllerBase
{
    [HttpGet("admin-only")]
    [AuthorizeRoles(Role.Admin)]
    public string TestAdmin()
    {
        return "admin verified";
    }

    [HttpGet("customer-only")]
    [AuthorizeRoles(Role.Customer)]
    public string TestCustomer()
    {
        return "customer verified";
    }

    [HttpGet("both-admin-customer")]
    [AuthorizeRoles(Role.Admin, Role.Customer)]
    public string TestBoth()
    {
        return "both admin and customer allow";
    }
}
