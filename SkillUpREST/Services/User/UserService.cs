namespace SkillUpREST.Services;

using SkillUpREST.Entity;
using SkillUpREST.Repositories.Interfaces;
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

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Hidden = false
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
    public User UpdateUser(Guid id, UpdateUserDto dto)
    {
        if (Exists(id))
        {
            var user = _userRepository.GetById(id);
            var updatedUser = new User
            {
                Id = id,
                Name = dto.Name,
                Hidden = user.Hidden
            };

            _userRepository.Update(updatedUser);

            return updatedUser;
        }

        return null;
    }
    public User DeleteUser(DeleteUserDto dto)
    {
        if (Exists(dto.Id))
        {
            var user = _userRepository.GetById(dto.Id);

            _userRepository.Delete(dto.Id);

            return user;
        }

        return null;
    }

    public User Get(params Func<User, bool>[] requirements) => GetAll(requirements).FirstOrDefault();

    public IEnumerable<User> GetAll(params Func<User, bool>[] requirements) => _userRepository.Get().Where(u => requirements.All(r => r(u)));

    private bool Exists(Guid id) => _userRepository.GetById(id) is not null;
}
