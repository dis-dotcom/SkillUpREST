namespace SkillUpREST.Entity;


public abstract class EntityBase : IEntity
{
    public Guid Id { get; init; }
    public DateTimeOffset Created { get; init; }
}
