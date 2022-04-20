namespace SkillUpREST.Controllers;


using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Entity;
using System.Collections.Generic;


[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("signin")]
    public IActionResult SignIn([FromBody] IDictionary<string, string> credentials)
    {
        if (credentials == null || credentials.Any(kv => kv.Key is null || kv.Value is null))
        {
            return Unauthorized();
        }

        Response.Headers["Accept"] = "application/json";
        Response.Headers["Token"] = Token.New().Value;

        return Accepted();
    }
}
