namespace SkillUpREST.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Entity;
using SkillUpREST.Services.Exceptions;
using SkillUpREST.Services.Interfaces;
using System.Collections;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    private object UserToRepresentableUser(User user) => new
    {
        Id = user.Id,
        Name = user.Name,
        Age = default(object),
        Gender = default(object),
        Blocked = user.Blocked
    };

    [HttpGet(Name = "GetUsers")]
    public IEnumerable Get()
    {
        return _userService.GetAll().Select(UserToRepresentableUser);
    }

    [HttpGet("{id}", Name = "GetById")]
    public IActionResult Get(Guid id)
    {
        var user = _userService.Get(user => user.Id == id);

        return user is null ? NotFound() : Ok(UserToRepresentableUser(user));
    }

    [HttpPost(Name = "CreateUser")]
    public IActionResult Post([FromBody] CreateUserDto dto)
    {
        try
        {
            var user = _userService.CreateUser(dto);

            if (user is null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("GetById", new { id = user.Id }, user);
        }
        catch (CreateUserDtoException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}", Name = "UpdateUser")]
    public IActionResult Put(Guid id, [FromBody] UpdateUserDto dto)
    {
        var user = _userService.UpdateUser(id, dto);

        if (user is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteUserById")]
    public IActionResult Delete(Guid id)
    {
        var deletedUser = _userService.DeleteUser(new DeleteUserDto(id));

        if (deletedUser is null)
        {
            return NoContent();
        }

        return Ok(UserToRepresentableUser(deletedUser));
    }
}
