namespace SkillUpREST.Repositories.Interfaces;

using SkillUpREST.Entity;
using System.Collections.Generic;

public interface ICompanyRepository
{
    string Root { get; }

    IEnumerable<Company> Find(params Predicate<Company>[] requirements);
    void Update(Guid id, Company company);
    void Insert(Guid id, Company company);
    void Delete(Guid id);
    void Delete(Company company);
}
