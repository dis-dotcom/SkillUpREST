namespace SkillUpREST.Repositories.Interfaces;


using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository;


public interface IRepositoryOnDrive<T> : IRepository<T> where T : IEntity
{
    string Location { get; }
}

