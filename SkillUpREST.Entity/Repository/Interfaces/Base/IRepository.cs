namespace SkillUpREST.Entity.Repository;


using System;
using System.Collections.Generic;


public interface IRepository<TEntity> : IReadonlyRepository<TEntity> where TEntity : IEntity
{
    void Insert(TEntity entity);
    void InsertMany(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteById(Guid id);
    void DeleteMany(IEnumerable<TEntity> entity);
}
