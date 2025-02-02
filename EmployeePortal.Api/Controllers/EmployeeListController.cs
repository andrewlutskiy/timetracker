using AutoMapper;
using EmployeePortal.Api.Domain.Employees;
using EmployeePortal.Api.Domain.Employees.Services;
using EmployeePortal.Api.Models;
using EmployeePortal.Api.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AuthPolicy")]
public class EmployeeListController : Controller
{
    private readonly IEmployeeSearchService _employeeSearchService;
    private readonly IMapper _mapper;

    public EmployeeListController(IEmployeeSearchService employeeSearchService, IMapper mapper)
    {
        _employeeSearchService = employeeSearchService;
        _mapper = mapper;
    }

    [HttpGet("current-user")]
    public async Task<string> GetCurrentUser()
    {
        return await Task.FromResult<string>(this.User.Identity.Name);
    }

    [HttpPost("filter")]
    public async Task<PagedResultModel<EmployeeModel>> GetPageAsync([FromBody]FilterParams filter)
    {
        var gridResponse = await _employeeSearchService.SearchAsync(filter);
        var companyResponseModels = gridResponse.Data.Select(item => _mapper.Map<Employee, EmployeeModel>(item)).ToList();
        return new PagedResultModel<EmployeeModel>
        {
            Data = companyResponseModels,
            TotalRecords = gridResponse.TotalCount
        };
    }
}