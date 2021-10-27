namespace SkillUpREST.Repositories.Interfaces;


using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository;


public interface IRepositoryOnDrive<TEntity> : IRepository<TEntity> where TEntity : IEntity
{
    string Location { get; }
}
