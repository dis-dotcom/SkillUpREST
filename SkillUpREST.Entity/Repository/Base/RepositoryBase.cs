namespace SkillUpREST.Entity.Repository;

using SkillUpREST.Repositories.Interfaces;
using System;
using System.Collections.Generic;

public class RepositoryBase<T> : ReadonlyRepositoryBase<T>, IRepository<T> where T: IEntity
{
    public virtual void Delete(T entity) { }
    public virtual void DeleteById(Guid id) { }
    public virtual void DeleteMany(IEnumerable<T> entity) { }
    public virtual void Insert(T entity) { }
    public virtual void InsertMany(IEnumerable<T> items) { }
}

public class RepositoryBaseOnDrive<T> : RepositoryBase<T>, IRepositoryOnDrive<T> where T: IEntity
{
    public RepositoryBaseOnDrive(string repositoryPath)
    {
        Location = repositoryPath;
    }

    public virtual string Location { get; protected set; }
}
