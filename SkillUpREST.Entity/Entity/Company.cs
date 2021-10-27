namespace SkillUpREST.Entity;


using System.Collections.Generic;


public class Company : EntityBase
{
    public string Name { get; }

    public IEnumerable<User> Employees { get; }
}
