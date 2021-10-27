namespace SkillUpREST.Entity;


using System;


public abstract class EntityBase : IEntity
{
    public Guid Id { get; protected set; }
}
