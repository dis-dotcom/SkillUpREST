namespace SkillUpREST.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        T GetById(Guid id);
        IEnumerable<T> Get();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Guid id);
    }
}
