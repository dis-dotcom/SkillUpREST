using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace SkillUpREST.Controllers;


[Route("api/signin")]
[ApiController]
public class SignInController : ControllerBase
{
    [HttpGet]
    public object SignIn()
    {
        return default;
    }
}
