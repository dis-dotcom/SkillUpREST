using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using System.Collections.Generic;

namespace SkillUpREST.Models
{
    public class CompanyService : ICompanyService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }

        public ICompanyRepository Repository => _companyRepository;

        public Company Create(string name)
        {
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            var entity = new CompanyEntity
            {
                Id = company.Id,
                Name = company.Name,
                OwnerId = null
            };

            _companyRepository.Insert(entity);

            return company;
        }

        public IEnumerable<Company> GetCompanies()
        {
            return _companyRepository.FindMany().Select(Company.From);
        }

        public Company? GetCompanyById(Guid id)
        {
            var company = _companyRepository.Find(company => company.Id == id);

            if (company is null)
            {
                return null;
            }

            return Company.From(company);
        }
    }
}
