namespace SkillUpREST.Services.Interfaces;

public interface IUserValidator
{
    bool IsValidCreateUserDto(CreateUserDto dto);
}
