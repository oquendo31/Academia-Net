using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaNet.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AcademicProgramsController : GenericController<AcademicProgram>
{
    private readonly IAcademicProgramsUnitOfWorks _academicProgramsUnitOfWorks;

    public AcademicProgramsController(IGenericUnitOfWork<AcademicProgram> unitOfWork, IAcademicProgramsUnitOfWorks academicProgramsUnitOfWorks) : base(unitOfWork)
    {
        _academicProgramsUnitOfWorks = academicProgramsUnitOfWorks;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _academicProgramsUnitOfWorks.GetAsync();
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
        var response = await _academicProgramsUnitOfWorks.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="institutionID"></param>
    /// <returns></returns>
    [HttpGet("combo/{institutionID:int}")]
    public async Task<IActionResult> GetComboAsync(int institutionID)
    {
        return Ok(await _academicProgramsUnitOfWorks.GetComboAsync(institutionID));
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="academicProgramDTO"></param>
    /// <returns></returns>
    [HttpPost("full")]
    public async Task<IActionResult> PostAsync(AcademicProgramDTO academicProgramDTO)
    {
        var action = await _academicProgramsUnitOfWorks.AddAsync(academicProgramDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="academicProgramDTO"></param>
    /// <returns></returns>
    [HttpPut("full")]
    public async Task<IActionResult> PutAsync(AcademicProgramDTO academicProgramDTO)
    {
        var action = await _academicProgramsUnitOfWorks.UpdateAsync(academicProgramDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }
}