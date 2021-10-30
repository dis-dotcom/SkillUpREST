global using System;
global using System.Linq;


namespace SkillUpREST.Entity;

public interface IEntity
{
    Guid Id { get; init; }
}
