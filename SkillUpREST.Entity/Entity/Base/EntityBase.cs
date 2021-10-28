namespace SkillUpREST.Entity;


public abstract class EntityBase : IEntity
{
    public EntityBase()
    {
        Id = Guid.NewGuid();
    }

    public EntityBase(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; protected set; }
}
