namespace SkillUpREST.Repositories.OnDrive
{
    using SkillUpREST.Entity;
    using SkillUpREST.Repositories.Interfaces;
    using System.IO;
    using System.Collections.Generic;

    public class CompanyRepositoryOnDrive : ICompanyRepository
    {
        private string _repositoryPath;

        public CompanyRepositoryOnDrive(string repositoryPath)
        {
            _repositoryPath = repositoryPath;
        }

        public string Root => _repositoryPath;

        public void Delete(Guid id)
        {
            var filePath = FilePath(id);

            if (Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void Delete(Company company)
        {
            if (company is null)
            {
                return;
            }

            Delete(company.Id);
        }

        public IEnumerable<Company> Find(params Predicate<Company>[] requirements)
        {
            throw new NotImplementedException();
        }

        public void Insert(Guid id, Company company)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Company company)
        {
            throw new NotImplementedException();
        }

        private string FilePath(Guid id) => Path.Combine(Root, id.ToString());
        private bool Exists(string filePath) => File.Exists(filePath);
        private bool Exists(Guid id) => File.Exists(FilePath(id));
    }
}
