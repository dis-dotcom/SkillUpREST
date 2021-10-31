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
    // consider not to inject repositories
    // create separated layer of services, where all BLL will be encapsulated
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    
    // WARNING: consider not to write your own DB context (repository), it is useless codebase
    public AdminCommonController(ICompanyRepository companyRepository, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _companyRepository = companyRepository;
    }

    [HttpGet("/company-list")]
    public IEnumerable<object> GetList()
    {
        // move such selections to the services layer
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
        Guid? ownerId = null;

        var company = new CompanyEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            OwnerId = ownerId
        };

        _companyRepository.Insert(company);

        return company.ToCompanyInfo();
    }

    [HttpGet("/company/{id}/employees")]
    public object GetUsersFromCompany(Guid id)
    {
        var company = _companyRepository.Find(company => company.Id == id);

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
        var company = _companyRepository.Find(company => company.Id == companyId);

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
