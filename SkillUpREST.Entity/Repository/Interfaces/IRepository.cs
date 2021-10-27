namespace SkillUpREST.Entity.Repository;

using System;
using System.Collections.Generic;

public interface IRepository<T> : IReadonlyRepository<T> where T : IEntity
{
    void Insert(T entity);
    void InsertMany(IEnumerable<T> items);
    void Delete(T entity);
    void DeleteById(Guid id);
    void DeleteMany(IEnumerable<T> entity);
}
