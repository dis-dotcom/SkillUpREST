namespace SkillUpREST.Services.Exceptions;


public class UserDtoException : Exception
{
    public UserDtoException(string message) : base(message) { }
}

public class CreateUserDtoException : UserDtoException
{
    public CreateUserDtoException(string message) : base(message) { }
}
