namespace SkillUpREST.Models.Exceptions;


public abstract class UserDtoException : Exception
{
    protected UserDtoException(string message) : base(message) { }
}

public class CreateUserDtoException : UserDtoException
{
    public CreateUserDtoException(string message) : base(message) { }
}
