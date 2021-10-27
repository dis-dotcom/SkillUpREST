namespace SkillUpREST.Entity;


public class User : EntityBase
{
    public string Name { get; }
    public Company Company { get; }

    public User(Guid id, string name, Company company = null)
    {
        Id = id;
        Name = name;
        Company = company;
    }
}
