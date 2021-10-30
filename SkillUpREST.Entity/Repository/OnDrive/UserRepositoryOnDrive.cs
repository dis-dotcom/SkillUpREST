namespace SkillUpREST.Entity.Repository;


using SkillUpREST.Entity.Repository.Interfaces;


internal class UserRepositoryOnDrive : RepositoryBaseOnDrive<UserEntity>, IUserRepository
{
    public UserRepositoryOnDrive(string repositoryPath) : base(repositoryPath)
    {

    }
}
