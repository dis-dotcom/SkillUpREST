namespace SkillUpREST.Entity.Repository;


using SkillUpREST.Entity.Repository.Interfaces;


internal class UserRepositoryOnDrive : RepositoryBaseOnDrive<User>, IUserRepository
{
    public UserRepositoryOnDrive(string repositoryPath) : base(repositoryPath)
    {

    }
}
