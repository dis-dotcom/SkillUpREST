namespace SkillUpREST.Controllers;

using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;


[Route("api/admin")]
[ApiController]
public class AdminCommonController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;

    public AdminCommonController(ICompanyRepository companyRepository, IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
                                 .ToCompanyInfo() ?? NotFound();
    }

    [HttpPost("/company")]
    public object Post([FromQuery] string name)
    {
        var company = new Company(Guid.NewGuid(), name);

        _companyRepository.Insert(company);

        return company.ToCompanyInfo();
    }

    [HttpGet("/company/{id}/employees")]
    public object GetUsersFromCompany(Guid id)
    {
        var company = _companyRepository.Find(company => company.Id == id);

        return company is null ? NotFound() : company.Employees.Select(user => user.ToUserInfo());
    }

    // TODO: Bind user to company
    [HttpPost("/company/{companyId}/employees/{userId}")]
    public IActionResult BindUserToCompany(Guid companyId, Guid userId)
    {
        var user = _userRepository.Find(user => user.Id == userId);
        var company = _companyRepository.Find(company => company.Id == companyId);

        return Ok();
    }
}

public static class CompanyExt
{
    public static object ToCompanyInfo(this Company company)
    {
        return company is null ? null : new
        {
            Id = company.Id,
            Name = company.Name
        };
    }
}

public static class UserExt
{
    public static object ToUserInfo(this User user)
    {
        return user is null ? null : new
        {
            Id = user.Id,
            Name = user.Name
        };
    }
}
