namespace SkillUpREST.Controllers;


using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Entity;


[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("signin")]
    public IActionResult SignIn([FromBody] UserIdentity credentials)
    {
        if (credentials.IsValid())
        {
            Response.Headers["Accept"] = "application/json";
            Response.Headers["Token"] = Token.New().Value;

            return Accepted();
        }

        return Unauthorized();
    }
}

public class UserIdentity
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public static class UserIdentityExt
{
    public static bool IsValid(this UserIdentity identity) => identity is not null &&
                                                              !string.IsNullOrWhiteSpace(identity.Password) &&
                                                              !string.IsNullOrWhiteSpace(identity.UserName);
}
