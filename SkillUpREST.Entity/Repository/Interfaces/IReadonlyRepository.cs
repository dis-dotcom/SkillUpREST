namespace SkillUpREST.Entity.Repository;

using System;
using System.Collections.Generic;

public interface IReadonlyRepository<T> where T: IEntity
{
    T Find(params Predicate<T>[] requirements);
    IEnumerable<T> FindMany(params Predicate<T>[] requirements);
}
