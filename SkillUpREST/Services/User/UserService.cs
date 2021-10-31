namespace SkillUpREST.Models;

using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using SkillUpREST.Models.Exceptions;
using SkillUpREST.Models.Interfaces;
using System.Collections.Generic;

public class UserService : IUserService
{
    private readonly IUserValidator _userDtoValidator;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IUserValidator userDtoValidator)
    {
        _userRepository = userRepository;
        _userDtoValidator = userDtoValidator;
    }

    public UserEntity CreateUser(CreateUserDto dto)
    {
        if (!_userDtoValidator.IsValidCreateUserDto(dto))
        {
            throw new CreateUserDtoException("Invalid values in object <CreateUserDto> dto");
        }

        var user = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        try
        {
            _userRepository.Insert(user);
        }
        catch
        {
            user = default;
        }

        return user;
    }
    public UserEntity UpdateUser(Guid id, UpdateUserDto dto)
    {
        if (Exists(id))
        {
            var user = _userRepository.Find(user => user.Id == id);
            
            var updatedUser = new UserEntity()
            {
                Id = id,
                Name = dto.Name
            };

            _userRepository.DeleteById(id);
            _userRepository.Insert(updatedUser);

            return updatedUser;
        }

        return null;
    }
    public UserEntity DeleteUser(DeleteUserDto dto)
    {
        if (Exists(dto.Id))
        {
            var user = GetById(dto.Id);

            _userRepository.DeleteById(dto.Id);

            return user;
        }

        return null;
    }
    public UserEntity Get(params Predicate<UserEntity>[] requirements) => _userRepository.Find(requirements);
    public IEnumerable<UserEntity> GetAll(params Predicate<UserEntity>[] requirements) => _userRepository.FindMany(requirements);

    private bool Exists(Guid id) => GetById(id) is not null;
    private UserEntity GetById(Guid id) => _userRepository.Find(user => user.Id == id);
}
