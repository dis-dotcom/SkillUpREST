namespace SkillUpREST.Services;

using SkillUpREST.Services.Interfaces;

public class UserDtoValidator : IUserValidator
{
    public bool IsValidCreateUserDto(CreateUserDto createUserDto)
    {
        var requirements = new Predicate<CreateUserDto>[]
        {
            dto => dto is not null,
            dto => dto.Name is not null,
            dto => dto.Name.Length >= 3
        };

        return requirements.All(r => r(createUserDto));
    }
}
