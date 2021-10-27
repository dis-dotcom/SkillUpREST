using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository;

namespace SkillUpREST.Repositories.Interfaces
{
    public interface IRepositoryOnDrive<T> : IRepository<T> where T: IEntity
    {
        string Location { get; }
    }
}
