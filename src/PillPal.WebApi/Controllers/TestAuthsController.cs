using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestAuthsController : ControllerBase
{
    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")]
    public string TestAdmin()
    {
        return "admin verified";
    }

    [HttpGet("customer-only")]
    [Authorize(Roles = "Customer")]
    public string TestCustomer()
    {
        return "customer verified";
    }

    [HttpGet("both-admin-customer")]
    [Authorize(Roles = "Admin,Customer")]
    public string TestBoth()
    {
        return "both admin and customer allow";
    }
}
