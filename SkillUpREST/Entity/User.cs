namespace SkillUpREST.Entity;

public class User : Entity
{
    public string Name { get; init; }

    public bool Hidden { get; set; }

    public bool Blocked { get; set; }
}
