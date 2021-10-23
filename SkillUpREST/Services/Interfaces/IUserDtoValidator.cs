namespace SkillUpREST.Services.Interfaces;

public interface IUserDtoValidator
{
    bool IsValidCreateUserDto(CreateUserDto dto);
}
