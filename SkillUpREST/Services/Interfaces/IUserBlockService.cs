namespace SkillUpREST.Models.Interfaces;

using SkillUpREST.Entity;

public interface IUserBlockService
{
    void BlockUser(Guid id);
    void BlockUser(UserEntity user);

    void UnblockUser(Guid id);
    void UnblockUser(UserEntity user);
}
