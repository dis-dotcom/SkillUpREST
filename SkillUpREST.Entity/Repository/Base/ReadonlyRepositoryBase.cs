namespace SkillUpREST.Entity.Repository;


using System;
using System.Collections.Generic;


public class ReadonlyRepositoryBase<T> : IReadonlyRepository<T> where T: IEntity
{
    public virtual T Find(params Predicate<T>[] requirements) => throw new NotImplementedException();
    public virtual IEnumerable<T> FindMany(params Predicate<T>[] requirements) => throw new NotImplementedException();
}
