using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademiaNet.Backend.Data;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using System.Diagnostics.Metrics;
using AcademiaNet.Backend.UnitsOfWork.Implementations;
using AcademiaNet.Shared.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace AcademiaNet.Backend.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

    /// <summary>
    ///
    /// </summary>
    /// <param name="institutionDTO"></param>
    /// <returns></returns>
    [HttpPost("full")]
    public async Task<IActionResult> PostAsync(InstitutionDTO institutionDTO)
    {
        var action = await _institutionsUnitOfWork.AddAsync(institutionDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    /// <summary>
    /// Trae la lista de las ubicaciones
    /// </summary>
    /// <returns></returns>
    [HttpGet("comboLocations")]
    public async Task<IActionResult> GetComboLocationsAsync()
    {
        return Ok(await _institutionsUnitOfWork.GetComboLocationsAsync());
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="academicProgramDTO"></param>
    /// <returns></returns>
    [HttpPut("full")]
    public async Task<IActionResult> PutAsync(InstitutionDTO institutionDTO)
    {
        var action = await _institutionsUnitOfWork.UpdateAsync(institutionDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
    {
        var response = await _institutionsUnitOfWork.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    [HttpGet("totalRecordsPaginated")]
    public async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _institutionsUnitOfWork.GetTotalRecordsAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }
}