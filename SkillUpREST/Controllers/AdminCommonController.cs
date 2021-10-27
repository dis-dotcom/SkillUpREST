﻿namespace SkillUpREST.Controllers;

using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
        return _companyRepository.FindMany()
                                 .Select(company => company.ToCompanyInfo());
    }

    [HttpGet("/company/{id}")]
    public object Get(Guid id)
    {
        return _companyRepository.Find(company => company.Id == id)
                                 .ToCompanyInfo();
    }

    [HttpPost("/company")]
    public object Post([FromQuery] string name)
    {
        var company = new Company(name);

        _companyRepository.Insert(company);

        return company.ToCompanyInfo();
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
            Id = company.Id,
            Name = company.Name
        };
    }
}
