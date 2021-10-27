namespace SkillUpREST.Entity;


using System.Collections.Generic;


public class Company : EntityBase
{
    public Company(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public IEnumerable<User> Employees { get; } = Array.Empty<User>();
}
