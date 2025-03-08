using AcademiaNet.Backend.UnitsOfWork.Implementations;
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

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet("comboCategories")]
    public async Task<IActionResult> GetComboCategoriesAsync()
    {
        var categories = await _academicProgramsUnitOfWorks.GetComboCategoriesAsync();

        // Puedes devolverlo directamente
        return Ok(categories);

        // O si prefieres devolver solo un par clave/valor (id/nombre) para combos, puedes hacer un select:
        // var result = categories.Select(c => new { c.CategoryID, c.Name });
        // return Ok(result);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
    {
        var response = await _academicProgramsUnitOfWorks.GetAsync(pagination);
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
        var action = await _academicProgramsUnitOfWorks.GetTotalRecordsAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }
}