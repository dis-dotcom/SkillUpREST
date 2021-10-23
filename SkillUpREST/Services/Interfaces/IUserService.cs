using SkillUpREST.Entity;
using System;
using System.Collections.Generic;

namespace SkillUpREST.Services.Interfaces
{
    public interface IUserService
    {
        // Base
        User CreateUser(CreateUserDto dto);

        User UpdateUser(Guid id, UpdateUserDto dto);

        User DeleteUser(DeleteUserDto dto);

        // Special
        User Get(params Func<User, bool>[] requirements);

        IEnumerable<User> GetAll(params Func<User, bool>[] requirements);
    }

    public record CreateUserDto(string Name);
    public record UpdateUserDto(string Name);
    public record DeleteUserDto(Guid Id);
}
