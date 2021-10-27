namespace SkillUpREST.Entity;

public partial class User : EntityBase
{
    public string Name { get; }

    public Company Company { get; }
}
