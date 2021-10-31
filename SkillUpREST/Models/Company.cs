namespace SkillUpREST.Models;

using SkillUpREST.Entity;

public struct Company
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    public static Company From(CompanyEntity entity)
    {
        return new Company
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}

