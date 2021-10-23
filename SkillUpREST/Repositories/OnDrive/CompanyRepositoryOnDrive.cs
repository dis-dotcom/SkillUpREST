namespace SkillUpREST.Repositories.OnDrive;

using SkillUpREST.Entity;
using SkillUpREST.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;

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
        foreach (var filePath in Directory.GetFiles(Root))
        {
            var json = File.ReadAllText(filePath);
            var company = Company.FromJson(json);

            if (requirements.All(requirement => requirement(company)))
            {
                yield return company;
            }
        }
    }
    public void Insert(Guid id, Company company)
    {
        File.WriteAllText(FilePath(id), company.ToJson());
    }
    public void Update(Guid id, Company company)
    {
        Delete(id);
        Insert(id, company);
    }
    private string FilePath(Guid id) => Path.Combine(Root, id.ToString());
    private bool Exists(string filePath) => File.Exists(filePath);
}
