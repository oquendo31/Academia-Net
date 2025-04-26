using AcademiaNet.Backend.UnitsOfWork.Implementations;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaNet.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentPeriodsController : GenericController<EnrollmentPeriod>
{
    private readonly IEnrollmentPeriodsUnitOfWorks _enrollmentPeriodsUnitOfWorks;

    public EnrollmentPeriodsController(IGenericUnitOfWork<EnrollmentPeriod> unitOfWork, IEnrollmentPeriodsUnitOfWorks EnrollmentPeriodsUnitOfWorks) : base(unitOfWork)
    {
        _enrollmentPeriodsUnitOfWorks = EnrollmentPeriodsUnitOfWorks;
    }

    /// <summary>
    /// Obtiene todos los periodos de inscripción disponibles desde la unidad de trabajo.
    /// </summary>
    /// <returns>Un resultado HTTP 200 con la lista de periodos si fue exitoso, o 400 en caso contrario.</returns>
    public override async Task<IActionResult> GetAsync()
    {
        var response = await _enrollmentPeriodsUnitOfWorks.GetAsync();
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    /// <summary>
    /// Obtiene un periodo de inscripción específico por su ID desde la unidad de trabajo.
    /// </summary>
    /// <param name="id">ID del periodo de inscripción a consultar.</param>
    /// <returns>Un resultado HTTP 200 con el periodo si fue encontrado, o 404 si no existe.</returns>
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id)
    {
        var response = await _enrollmentPeriodsUnitOfWorks.GetAsync(id);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return NotFound(response.Message);
    }

    /// <summary>
    /// Obtiene una lista de periodos de inscripción filtrados por el ID de una institución, ordenados por nombre.
    /// </summary>
    /// <param name="institutionID">ID de la institución para la cual se consultan los periodos.</param>
    /// <returns>Un resultado HTTP 200 con la lista de periodos correspondientes.</returns>
    [HttpGet("combo/{institutionID:int}")]
    public async Task<IActionResult> GetComboAsync(int institutionID)
    {
        return Ok(await _enrollmentPeriodsUnitOfWorks.GetComboAsync(institutionID));
    }

    /// <summary>
    /// Crea un nuevo periodo de inscripción con los datos proporcionados.
    /// </summary>
    /// <param name="enrollmentPeriodDTO">DTO con la información del periodo de inscripción a crear.</param>
    /// <returns>Resultado HTTP 200 si la creación fue exitosa; de lo contrario, HTTP 400 con mensaje de error.</returns>
    [HttpPost("full")]
    public async Task<IActionResult> PostAsync(EnrollmentPeriodDTO enrollmentPeriodDTO)
    {
        var action = await _enrollmentPeriodsUnitOfWorks.AddAsync(enrollmentPeriodDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    /// <summary>
    /// Actualiza un periodo de inscripción existente con la información proporcionada.
    /// </summary>
    /// <param name="enrollmentPeriodDTO">DTO con los datos actualizados del periodo de inscripción.</param>
    /// <returns>Un resultado HTTP 200 con el periodo actualizado si es exitoso; de lo contrario, un HTTP 400 con el mensaje de error.</returns>
    [HttpPut("full")]
    public async Task<IActionResult> PutAsync(EnrollmentPeriodDTO enrollmentPeriodDTO)
    {
        var action = await _enrollmentPeriodsUnitOfWorks.UpdateAsync(enrollmentPeriodDTO);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }

    /// <summary>
    /// Obtiene una lista paginada de periodos de inscripción, aplicando filtros si se especifican.
    /// </summary>
    /// <param name="pagination">Parámetros de paginación y filtro.</param>
    /// <returns>Resultado HTTP 200 con la lista paginada si es exitoso; de lo contrario, HTTP 400.</returns>
    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
    {
        var response = await _enrollmentPeriodsUnitOfWorks.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    /// <summary>
    /// Obtiene el número total de registros de periodos de inscripción, aplicando los filtros de búsqueda especificados.
    /// </summary>
    /// <param name="pagination">Parámetros de paginación que incluyen filtros opcionales.</param>
    /// <returns>Resultado HTTP 200 con el número total de registros si es exitoso; de lo contrario, HTTP 400.</returns>
    [HttpGet("totalRecordsPaginated")]
    public async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _enrollmentPeriodsUnitOfWorks.GetTotalRecordsAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }
}