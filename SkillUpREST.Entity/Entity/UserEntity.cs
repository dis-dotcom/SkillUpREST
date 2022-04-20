namespace SkillUpREST.Entity;


public class UserEntity : EntityBase
{
    public string Name { get; set; }
    public Guid? CompanyId { get; set; }
    public bool Blocked { get; set; }
}
