using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace PillPal.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("/auth")]
        public dynamic Get(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            return new
            {
                name = jsonToken.Claims.Where(x => x.Type == "name").FirstOrDefault().Value,
                picture = jsonToken.Claims.Where(x => x.Type == "picture").FirstOrDefault().Value,
                email_verified = jsonToken.Claims.Where(x => x.Type == "email_verified").FirstOrDefault().Value,
                email = jsonToken.Claims.Where(x => x.Type == "email").FirstOrDefault().Value,

            };
        }
    }
}
