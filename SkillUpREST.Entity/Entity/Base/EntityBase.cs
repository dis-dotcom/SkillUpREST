namespace SkillUpREST.Entity;


public abstract class EntityBase : IEntity
{
    public EntityBase()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; protected set; }
}
