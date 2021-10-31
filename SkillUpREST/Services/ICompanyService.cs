using SkillUpREST.Entity.Repository.Interfaces;
using System.Collections.Generic;

namespace SkillUpREST.Models
{
    public interface ICompanyService
    {
        ICompanyRepository Repository { get; }
        Company? GetCompanyById(Guid id);
        IEnumerable<Company> GetCompanies();
        Company Create(string name);
    }
}
