namespace SkillUpREST.Entity.Repository;
using System;
using System.Collections.Generic;


public class RepositoryBase<T> : ReadonlyRepositoryBase<T>, IRepository<T> where T : IEntity
{
    public virtual void Delete(T entity) => throw new NotImplementedException();
    public virtual void DeleteById(Guid id) => throw new NotImplementedException();
    public virtual void DeleteMany(IEnumerable<T> entity) => throw new NotImplementedException();
    public virtual void Insert(T entity) => throw new NotImplementedException();
    public virtual void InsertMany(IEnumerable<T> items) => throw new NotImplementedException();
}
