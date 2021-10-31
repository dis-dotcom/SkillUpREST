using SkillUpREST.Entity.Repository.Interfaces;
using System.Collections.Generic;

namespace SkillUpREST.Services
{
    public interface ICompanyService
    {
        ICompanyRepository Repository { get; }
        Company? GetCompanyById(Guid id);
        IEnumerable<Company> GetCompanies();
        Company Create(string name);
    }
}
