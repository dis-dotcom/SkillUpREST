namespace SkillUpREST.Services;

using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using SkillUpREST.Repositories.Interfaces;
using SkillUpREST.Services.Interfaces;
using System;

public class UserBlockService : IUserBlockService
{
    private IUserRepository _userRepository;

    public UserBlockService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void BlockUser(User user)
    {
        // user.Blocked = true;

        // _userRepository.Update(user);
    }

    public void BlockUser(Guid id)
    {
        // var user = _userRepository.GetById(id);

        // BlockUser(user);
    }

    public void UnblockUser(User user)
    {
        // user.Blocked = false;

        // _userRepository.Update(user);
    }

    public void UnblockUser(Guid id)
    {
        // var user = _userRepository.GetById(id);

        // UnblockUser(user);
    }
}
