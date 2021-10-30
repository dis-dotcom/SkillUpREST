namespace SkillUpREST.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using SkillUpREST.Services.Exceptions;
using SkillUpREST.Services.Interfaces;
using System.Collections.Generic;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public UserController(IUserService userService, IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    [HttpGet(Name = "GetUsers")]
    public IEnumerable<object> Get()
    {
        return _userRepository.FindMany()
                              .Select(user => user.ToRepresentableUser());
    }

    [HttpGet("{id}", Name = "GetById")]
    public IActionResult Get(Guid id)
    {
        var user = _userRepository.Find(user => user.Id == id)
                                  .ToRepresentableUser();

        return user is null
            ? NotFound()
            : Ok(user);
    }

    // TODO: For refactoring
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

    // TODO: For refactoring
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
        var userForDelete = _userRepository.Find(user => user.Id == id);

        if (userForDelete is null)
        {
            return Ok();
        }

        // TODO: TRANSACTION!!!
        _userRepository.DeleteById(id);

        return Ok(userForDelete.ToDeletedUser());
    }
}

internal static class UserEntityExt
{
    public static object ToRepresentableUser(this UserEntity user)
    {
        return user is null ? null : new
        {
            Id = user.Id,
            Name = user.Name,
            Age = default(int?),
            Gender = default(object)
        };
    }

    public static object ToDeletedUser(this UserEntity user)
    {
        return user is null ? null : new
        {
            Id = user.Id,
            Name = user.Name
        };
    }
}
