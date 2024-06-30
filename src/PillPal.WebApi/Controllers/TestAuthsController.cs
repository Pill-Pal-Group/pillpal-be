namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestAuthsController : ControllerBase
{
    /// <summary>
    /// Allow only admin role
    /// </summary>
    /// <remarks>Requires admin policy</remarks>
    [HttpGet("admin-only")]
    [Authorize(Policy.Admin)]
    public string TestAdmin()
    {
        return "admin verified";
    }

    /// <summary>
    /// Allow only customer role
    /// </summary>
    /// <remarks>Requires customer policy</remarks>
    [HttpGet("customer-only")]
    [Authorize(Policy.Customer)]
    public string TestCustomer()
    {
        return "customer verified";
    }

    /// <summary>
    /// Allow only manager role
    /// </summary>
    /// <remarks>Requires manager policy</remarks>
    [HttpGet("manager-only")]
    [Authorize(Policy.Manager)]
    public string TestManager()
    {
        return "manager verified";
    }

    /// <summary>
    /// Allow both admin and manager role
    /// </summary>
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    [HttpGet("both-admin-manager")]
    [Authorize(Policy.Administrative)]
    public string TestAdminMng()
    {
        return "both admin and manager allow";
    }

    /// <summary>
    /// Allow all roles admin, manager and customer
    /// </summary>
    /// <remarks>Requires authentication</remarks>
    [HttpGet("all-three")]
    [Authorize]
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
