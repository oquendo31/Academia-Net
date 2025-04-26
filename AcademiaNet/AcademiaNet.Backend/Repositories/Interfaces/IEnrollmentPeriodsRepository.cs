using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.Repositories.Interfaces;

public interface IEnrollmentPeriodsRepository
{
    /// <summary>
    /// Obtiene todos los periodos de inscripción de una institución para combos/dropdowns.
    /// </summary>
    /// <param name="institutionID"></param>
    /// <returns></returns>
    Task<IEnumerable<EnrollmentPeriod>> GetComboAsync(int institutionID);

    /// <summary>
    /// Agrega un nuevo periodo de inscripción.
    /// </summary>
    /// <param name="enrollmentPeriodDTO"></param>
    /// <returns></returns>
    Task<ActionResponse<EnrollmentPeriod>> AddAsync(EnrollmentPeriodDTO enrollmentPeriodDTO);

    /// <summary>
    /// Actualiza un periodo de inscripción existente.
    /// </summary>
    /// <param name="enrollmentPeriodDTO"></param>
    /// <returns></returns>
    Task<ActionResponse<EnrollmentPeriod>> UpdateAsync(EnrollmentPeriodDTO enrollmentPeriodDTO);

    /// <summary>
    /// Obtiene un periodo de inscripción por ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ActionResponse<EnrollmentPeriod>> GetAsync(int id);

    /// <summary>
    /// Obtiene todos los periodos de inscripción.
    /// </summary>
    /// <returns></returns>
    Task<ActionResponse<IEnumerable<EnrollmentPeriod>>> GetAsync();

    /// <summary>
    /// Obtiene los periodos de inscripción de forma paginada.
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    Task<ActionResponse<IEnumerable<EnrollmentPeriod>>> GetAsync(PaginationDTO pagination);

    /// <summary>
    /// Obtiene el total de registros de periodos de inscripción (usando filtros de paginación si los hay).
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
}