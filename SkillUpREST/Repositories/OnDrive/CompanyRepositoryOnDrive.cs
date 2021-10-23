namespace SkillUpREST.Repositories.OnDrive
{
    using SkillUpREST.Repositories.Interfaces;

    public class CompanyRepositoryOnDrive : ICompanyRepository
    {
        private string _repositoryPath;

        public CompanyRepositoryOnDrive(string repositoryPath)
        {
            _repositoryPath = repositoryPath;
        }

        public string Root => _repositoryPath;
    }
}
