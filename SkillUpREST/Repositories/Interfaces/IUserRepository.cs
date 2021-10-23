using System;
using System.Collections.Generic;
using SkillUpREST.Entity;

namespace SkillUpREST.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User Find(params Predicate<User>[] requirements);
        IEnumerable<User> FindAll(params Predicate<User>[] requirements);
    }
}

namespace SkillUpREST.Repositories.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {

    }

    public class UserNotFoundException : Exception
    {

    }
}
