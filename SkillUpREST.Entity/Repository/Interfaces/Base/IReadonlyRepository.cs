namespace SkillUpREST.Entity.Repository;


using System;
using System.Collections.Generic;


public interface IReadonlyRepository<TEntity> where TEntity : IEntity
{
    TEntity Find(params Predicate<TEntity>[] requirements);
    IEnumerable<TEntity> FindMany(params Predicate<TEntity>[] requirements);
}
