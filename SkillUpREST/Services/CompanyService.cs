using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using System.Collections.Generic;

namespace SkillUpREST.Services
{
    public interface ICompanyService
    {
        IEnumerable<T> GetCompanies<T>(Func<CompanyEntity, T> convert);
    }

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IEnumerable<T> GetCompanies<T>(Func<CompanyEntity, T> convert)
        {
            return _companyRepository.FindMany().Select(convert);
        }
    }
}
