namespace SkillUpREST.Controllers;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using System.Collections.Generic;

[Route("api/admin")]
[ApiController]
public class AdminCommonController : ControllerBase
{
    private ICompanyRepository _companyRepository;

    public AdminCommonController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    [HttpGet("/company-list")]
    public IEnumerable<object> GetList()
    {
        object ToCompanyInfo(Company company)
        {
            return new
            {
                Name = company.Name
            };
        }

        return _companyRepository.FindMany()
                                 .ToArray()
                                 .Select(ToCompanyInfo);
    }

    [HttpGet("/company/{id}")]
    public object Get(Guid id)
    {
        return _companyRepository.Find(company => company.Id == id)
                                 .ToCompanyInfo();
    }

    [HttpGet("/company")]
    public object GetByName([FromQuery]string name)
    {
        return _companyRepository.Find(company => company.Name == name)?
                                 .ToCompanyInfo();
    }

    [HttpPost]
    public Company Post([FromBody] CreateCompany dto)
    {
        var company = new Company(dto.Name);

        _companyRepository.Insert(company);

        return company;
    }

    public class CreateCompany
    {
        public string Name { get; set; }
    }
}

public static class CompanyExt
{
    public static object ToCompanyInfo(this Company company)
    {
        return new
        {
            Name = company.Name
        };
    }
}
