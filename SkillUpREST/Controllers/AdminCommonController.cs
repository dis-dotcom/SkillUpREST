namespace SkillUpREST.Controllers;


using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Entity;
using SkillUpREST.Repositories.Interfaces;
using System.Collections;


[Route("api/admin")]
[ApiController]
public class AdminCommonController : ControllerBase
{
    private ICompanyRepository _companyRepository;

    public AdminCommonController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    object ToCompanyInfo(Company company)
    {
        return new
        {
            Name = company.Name
        };
    }

    [HttpGet("/company-list")]
    public IEnumerable GetList()
    {
        return _companyRepository.Find()
                                 .Select(ToCompanyInfo);
    }

    [HttpGet("/company/{id}")]
    public object Get(Guid id)
    {
        return _companyRepository.Find(company => company.Id == id)
                                 .Select(ToCompanyInfo)
                                 .First();
    }

    public class DTO
    {
        public string Name { get; set; }
    }

    [HttpPost]
    public Company Post([FromBody] DTO dto)
    {
        var id = Guid.NewGuid();
        var json = dto.ToString();

        var company = new Company
        {
            Id = id,
            Name = dto.Name
        };

        _companyRepository.Insert(id, company);

        return company;
    }
}
