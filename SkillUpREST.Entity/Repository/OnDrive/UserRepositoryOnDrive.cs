namespace SkillUpREST.Entity.Repository;

public class UserRepositoryOnDrive : RepositoryBaseOnDrive<User>
{
    public UserRepositoryOnDrive(string repositoryPath) : base(repositoryPath)
    {

    }
}
