namespace SkillUpREST.Services.Interfaces;

using SkillUpREST.Entity;

public interface IUserBlockService
{
    void BlockUser(Guid id);
    void BlockUser(User user);

    void UnblockUser(Guid id);
    void UnblockUser(User user);
}
