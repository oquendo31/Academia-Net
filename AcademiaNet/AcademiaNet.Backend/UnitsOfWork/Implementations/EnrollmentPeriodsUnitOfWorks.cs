using AcademiaNet.Backend.Repositories.Implementations;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.UnitsOfWork.Implementations;

public class EnrollmentPeriodsUnitOfWorks : GenericUnitOfWork<EnrollmentPeriod>, IEnrollmentPeriodsUnitOfWorks
{
    private readonly IEnrollmentPeriodsRepository _enrollmentPeriodsRepository;

    public EnrollmentPeriodsUnitOfWorks(IGenericRepository<EnrollmentPeriod> repository, IEnrollmentPeriodsRepository EnrollmentPeriodsRepository) : base(repository)
    {
        _enrollmentPeriodsRepository = EnrollmentPeriodsRepository;
    }

    /// <summary>
    /// Agrega un nuevo periodo de inscripción utilizando los datos proporcionados en el DTO.
    /// </summary>
    /// <param name="enrollmentPeriodDTO">El DTO que contiene la información del nuevo periodo.</param>
    /// <returns>Una respuesta que indica si la operación fue exitosa e incluye el periodo agregado.</returns>
    public async Task<ActionResponse<EnrollmentPeriod>> AddAsync(EnrollmentPeriodDTO enrollmentPeriodDTO) => await _enrollmentPeriodsRepository.AddAsync(enrollmentPeriodDTO);

    /// <summary>
    /// Obtiene una lista de periodos de inscripción asociados a una institución específica.
    /// </summary>
    /// <param name="institutionID">El ID de la institución.</param>
    /// <returns>Una lista de periodos de inscripción correspondientes a la institución.</returns>
    public async Task<IEnumerable<EnrollmentPeriod>> GetComboAsync(int institutionID) => await _enrollmentPeriodsRepository.GetComboAsync(institutionID);

    /// <summary>
    /// Actualiza un periodo de inscripción existente con los datos proporcionados en el DTO.
    /// </summary>
    /// <param name="enrollmentPeriodDTO">El DTO con la información actualizada del periodo.</param>
    /// <returns>Una respuesta con el resultado de la operación y el objeto actualizado si fue exitosa.</returns>
    public async Task<ActionResponse<EnrollmentPeriod>> UpdateAsync(EnrollmentPeriodDTO enrollmentPeriodDTO) => await _enrollmentPeriodsRepository.UpdateAsync(enrollmentPeriodDTO);

    /// <summary>
    /// Obtiene un periodo de inscripción por su ID.
    /// </summary>
    /// <param name="id">El ID del periodo de inscripción.</param>
    /// <returns>Una respuesta con el periodo correspondiente si existe.</returns>
    public override async Task<ActionResponse<EnrollmentPeriod>> GetAsync(int id) => await _enrollmentPeriodsRepository.GetAsync(id);

    /// <summary>
    /// Obtiene todos los periodos de inscripción sin filtros ni paginación.
    /// </summary>
    /// <returns>Una respuesta con la lista completa de periodos de inscripción.</returns>
    public override async Task<ActionResponse<IEnumerable<EnrollmentPeriod>>> GetAsync() => await _enrollmentPeriodsRepository.GetAsync();

    /// <summary>
    /// Obtiene una lista paginada de periodos de inscripción, aplicando filtros si se especifican.
    /// </summary>
    /// <param name="pagination">Parámetros de paginación y filtro.</param>
    /// <returns>Una respuesta con los periodos filtrados y paginados.</returns>
    public override async Task<ActionResponse<IEnumerable<EnrollmentPeriod>>> GetAsync(PaginationDTO pagination) => await _enrollmentPeriodsRepository.GetAsync(pagination);

    /// <summary>
    /// Obtiene el número total de registros de periodos de inscripción que cumplen con los criterios de filtrado.
    /// </summary>
    /// <param name="pagination">Parámetros de filtrado.</param>
    /// <returns>Una respuesta con el total de registros encontrados.</returns>
    public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _enrollmentPeriodsRepository.GetTotalRecordsAsync(pagination);
}