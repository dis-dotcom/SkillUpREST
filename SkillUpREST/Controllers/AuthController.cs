using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Models;
using System.Collections.Generic;

#nullable enable

namespace SkillUpREST.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("sign-in")]
        public IActionResult SignIn([FromBody] IDictionary<string, string> credentials)
        {
            if (credentials == null || credentials.Any(kv => kv.Key is null || kv.Value is null))
            {
                return Unauthorized();
            }

            Response.Headers["Accept"] = "application/json";
            Response.Headers["Token"] = TokenManager.GenerateToken<string>();

            return Accepted();
        }
    }
}
