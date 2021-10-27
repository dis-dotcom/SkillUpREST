namespace SkillUpREST.Services;

using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using SkillUpREST.Services.Exceptions;
using SkillUpREST.Services.Interfaces;
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

    public User CreateUser(CreateUserDto dto)
    {
        if (!_userDtoValidator.IsValidCreateUserDto(dto))
        {
            throw new CreateUserDtoException("Invalid values in object <CreateUserDto> dto");
        }

        var user = new User(Guid.NewGuid(), dto.Name);

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
    public User UpdateUser(Guid id, UpdateUserDto dto)
    {
        if (Exists(id))
        {
            var user = _userRepository.Find(user => user.Id == id);
            var updatedUser = new User(id, dto.Name);

            _userRepository.DeleteById(id);
            _userRepository.Insert(updatedUser);

            return updatedUser;
        }

        return null;
    }
    public User DeleteUser(DeleteUserDto dto)
    {
        if (Exists(dto.Id))
        {
            var user = GetById(dto.Id);

            _userRepository.DeleteById(dto.Id);

            return user;
        }

        return null;
    }
    public User Get(params Predicate<User>[] requirements) => _userRepository.Find(requirements);
    public IEnumerable<User> GetAll(params Predicate<User>[] requirements) => _userRepository.FindMany(requirements);

    private bool Exists(Guid id) => GetById(id) is not null;
    private User GetById(Guid id) => _userRepository.Find(user => user.Id == id);
}
