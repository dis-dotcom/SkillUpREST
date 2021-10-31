namespace SkillUpREST.Services.Interfaces;

// consider to use fluent validations, do not create your own
public interface IUserValidator
{
    bool IsValidCreateUserDto(CreateUserDto dto);
}
