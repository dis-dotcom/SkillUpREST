using SkillUpREST.Entity;
using System.Collections.Generic;

namespace SkillUpREST.Models.Interfaces
{
    public interface IUserService
    {
        // Base
        UserEntity CreateUser(CreateUserDto dto);

        UserEntity UpdateUser(Guid id, UpdateUserDto dto);

        UserEntity DeleteUser(DeleteUserDto dto);

        // Special
        UserEntity Get(params Predicate<UserEntity>[] requirements);

        IEnumerable<UserEntity> GetAll(params Predicate<UserEntity>[] requirements);
    }

    public record CreateUserDto(string Name);
    public record UpdateUserDto(string Name);
    public record DeleteUserDto(Guid Id);
}
