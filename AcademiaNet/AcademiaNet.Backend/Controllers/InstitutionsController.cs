using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademiaNet.Backend.Data;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using System.Diagnostics.Metrics;
using AcademiaNet.Backend.UnitsOfWork.Implementations;

namespace AcademiaNet.Backend.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class InstitutionsController : GenericController<Institution>
//{
//    private readonly IInstitutionsUnitOfWork _institutionsUnitOfWork;

//    public InstitutionsController(IGenericUnitOfWork<Institution> unitOfWork, IInstitutionsUnitOfWork institutionsUnitOfWork) : base(unitOfWork)
//    {
//        _institutionsUnitOfWork = institutionsUnitOfWork;
//    }

[ApiController]
[Route("api/[controller]")]
public class InstitutionsController : GenericController<Institution>
{
    private readonly IInstitutionsUnitOfWork _institutionsUnitOfWork;

    public InstitutionsController(IGenericUnitOfWork<Institution> unit, IInstitutionsUnitOfWork institutionsUnitOfWork) : base(unit)
    {
        _institutionsUnitOfWork = institutionsUnitOfWork;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _institutionsUnitOfWork.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _institutionsUnitOfWork.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet("combo")]
    public async Task<IActionResult> GetComboAsync()
    {
        return Ok(await _institutionsUnitOfWork.GetComboAsync());
    }
}