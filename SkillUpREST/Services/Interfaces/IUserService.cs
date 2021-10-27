using SkillUpREST.Entity;
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
        User Get(params Predicate<User>[] requirements);

        IEnumerable<User> GetAll(params Predicate<User>[] requirements);
    }

    public record CreateUserDto(string Name);
    public record UpdateUserDto(string Name);
    public record DeleteUserDto(Guid Id);
}
