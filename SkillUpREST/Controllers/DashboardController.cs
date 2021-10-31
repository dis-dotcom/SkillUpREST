using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Services.Interfaces;
using static System.IO.File;

namespace SkillUpREST.Controllers;

[Route("/")]
[ApiController]
public class DashboardController : ControllerBase
{
    // move this variables to static class
    private string IndexPath => Path.Combine(App.Root, "index.html");
    private string StylesPath => Path.Combine(App.Root, "styles.css");
    private string SignInPath => Path.Combine(App.Root, "signin.html");
    private string JSApiPath => Path.Combine(App.Root, "js/Api.js");
    private string JSCorePath => Path.Combine(App.Root, "js/core.js");
    private string JSReportComponentPath => Path.Combine(App.Root, "js/usersComponent.js");
    private string JSNetPath => Path.Combine(App.Root, "js/Net.js");

    private IUserBlockService _userBlockService;

    public DashboardController(IUserBlockService userBlockService)
    {
        _userBlockService = userBlockService;
    }

    [HttpGet("/dashboard")]
    public ActionResult Index()
    {
        var content = ReadAllText(IndexPath);

        return Content(content, "text/html", Encoding.UTF8);
    }

    [HttpGet("/styles.css")]
    public ActionResult Styles()
    {
        var content = ReadAllText(StylesPath);

        return Content(content, "text/css", Encoding.UTF8);
    }

    [HttpPost("/dashboard/block-user/{id}")]
    public IActionResult BlockUser(Guid id)
    {
        _userBlockService.BlockUser(id);

        return Accepted();
    }

    [HttpPost("/dashboard/unblock-user/{id}")]
    public IActionResult UnblockUser(Guid id)
    {
        _userBlockService.UnblockUser(id);

        return Accepted();
    }

    [HttpGet("/signin")]
    public ActionResult SignIn()
    {
        var content = ReadAllText(SignInPath);

        return Content(content, "text/html", Encoding.UTF8);
    }

    [HttpGet("/js/Api.js")]
    public ActionResult GetJS()
    {
        var content = ReadAllText(JSApiPath);

        return Content(content, "application/json", Encoding.UTF8);
    }

    [HttpGet("/js/core.js")]
    public ActionResult GetCore()
    {
        var content = ReadAllText(JSCorePath);

        return Content(content, "application/json", Encoding.UTF8);
    }

    [HttpGet("/js/usersComponent.js")]
    public ActionResult GetReportComponent()
    {
        var content = ReadAllText(JSReportComponentPath);

        return Content(content, "application/json", Encoding.UTF8);
    }

    [HttpGet("/js/Net.js")]
    public ActionResult GetNet()
    {
        var content = ReadAllText(JSNetPath);

        return Content(content, "application/json", Encoding.UTF8);
    }
}
