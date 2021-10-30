namespace SkillUpREST.Services;

using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using SkillUpREST.Services.Interfaces;
using System;

// TODO: Required method Update for Repository
public class UserBlockService : IUserBlockService
{
    private readonly IUserRepository _userRepository;

    public UserBlockService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void BlockUser(UserEntity user)
    {
        // user.Blocked = true;

        // _userRepository.Update(user);
    }

    public void BlockUser(Guid id)
    {
        var user = _userRepository.Find(user => user.Id == id);

        if (user is not null)
        {
            user.Blocked = true;
            _userRepository.DeleteById(id);
            _userRepository.Insert(user);
        }
    }

    public void UnblockUser(UserEntity user)
    {
        // user.Blocked = false;

        // _userRepository.Update(user);
    }

    public void UnblockUser(Guid id)
    {
        var user = _userRepository.Find(user => user.Id == id);

        if (user is not null)
        {
            user.Blocked = false;
            _userRepository.DeleteById(id);
            _userRepository.Insert(user);
        }
    }
}
