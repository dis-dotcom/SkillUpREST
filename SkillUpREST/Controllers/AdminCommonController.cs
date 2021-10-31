namespace SkillUpREST.Controllers;

using Microsoft.AspNetCore.Mvc;
using SkillUpREST.Entity;
using SkillUpREST.Entity.Repository.Interfaces;
using SkillUpREST.Models;
using System.Collections.Generic;
using System.Linq;


[Route("api/admin")]
[ApiController]
public class AdminCommonController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ICompanyService _companyService;

    public AdminCommonController(IUserRepository userRepository, ICompanyService companyService)
    {
        _userRepository = userRepository;
        _companyService = companyService;
    }

    [HttpGet("/company-list")]
    public IEnumerable<Company> GetList() => _companyService.GetCompanies();

    [HttpGet("/company/{id}")]
    public IActionResult Get(Guid id)
    {
        var company = _companyService.GetCompanyById(id);

        return company is null
            ? NotFound()
            : Ok(company);
    }

    [HttpPost("/company")]
    public object Post([FromQuery] string name)
    {
        var company = _companyService.Create(name);

        return company;
    }

    [HttpGet("/company/{id}/employees")]
    public object GetUsersFromCompany(Guid id)
    {
        var company = _companyService.Repository.Find(company => company.Id == id);

        return company is null
            ? NotFound()
            : _userRepository.FindMany(user => user.CompanyId == id)
                             .Select(user => user.ToUserInfo());
    }

    // TODO: Bind user to company
    [HttpPost("/company/{companyId}/employees/{userId}")]
    public IActionResult BindUserToCompany(Guid userId, Guid companyId)
    {
        var user = _userRepository.Find(user => user.Id == userId);
        var company = _companyService.Repository.Find(company => company.Id == companyId);

        if (user is null)
        {
            return NotFound();
        }

        if (company is null)
        {
            return NotFound();
        }

        user.CompanyId = companyId;

        // TODO: TRANSACTION!!!
        _userRepository.DeleteById(userId);
        _userRepository.Insert(user);

        return Ok();
    }
}

public static class CompanyExt
{
    public static object ToCompanyInfo(this CompanyEntity company)
    {
        return company is null ? null : new
        {
            Id = company.Id,
            Name = company.Name ?? string.Empty
        };
    }
}

public static class UserExt
{
    public static object ToUserInfo(this UserEntity user)
    {
        return user is null ? null : new
        {
            Id = user.Id,
            Name = user.Name
        };
    }
}
