namespace SkillUpREST.Entity.Repository;


using SkillUpREST.Entity.Repository.Interfaces;


internal class CompanyRepositoryOnDrive : RepositoryBaseOnDrive<CompanyEntity>, ICompanyRepository
{
    public CompanyRepositoryOnDrive(string repositoryPath) : base(repositoryPath)
    {

    }
}
