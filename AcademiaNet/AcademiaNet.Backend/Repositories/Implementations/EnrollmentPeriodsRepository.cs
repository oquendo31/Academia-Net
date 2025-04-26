using AcademiaNet.Backend.Data;
using AcademiaNet.Backend.Helpers;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace AcademiaNet.Backend.Repositories.Implementations;

public class EnrollmentPeriodsRepository : GenericRepository<EnrollmentPeriod>, IEnrollmentPeriodsRepository
{
    private readonly DataContext _context;

    public EnrollmentPeriodsRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los periodos de inscripción, incluyendo la institución asociada, ordenados por nombre del periodo.
    /// </summary>
    /// <returns>Una respuesta que contiene la lista de periodos de inscripción.</returns>

    public override async Task<ActionResponse<IEnumerable<EnrollmentPeriod>>> GetAsync()
    {
        var enrollmentPeriods = await _context.EnrollmentPeriods
            .Include(x => x.Institution)  // Relación con Institution
            .OrderBy(x => x.PeriodName)
            .ToListAsync();

        return new ActionResponse<IEnumerable<EnrollmentPeriod>>
        {
            WasSuccess = true,
            Result = enrollmentPeriods
        };
    }

    /// <summary>
    /// Obtiene un periodo de inscripción por su identificador, incluyendo la institución asociada.
    /// </summary>
    /// <param name="id">Identificador del periodo de inscripción.</param>
    /// <returns>Una respuesta que contiene el periodo de inscripción si se encuentra; de lo contrario, un mensaje de error.</returns>

    public override async Task<ActionResponse<EnrollmentPeriod>> GetAsync(int id)
    {
        var enrollmentPeriod = await _context.EnrollmentPeriods
                 .Include(x => x.Institution)
                 .FirstOrDefaultAsync(c => c.EnrollmentPeriodID == id);

        if (enrollmentPeriod == null)
        {
            return new ActionResponse<EnrollmentPeriod>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<EnrollmentPeriod>
        {
            WasSuccess = true,
            Result = enrollmentPeriod
        };
    }

    /// <summary>
    /// Agrega un nuevo periodo de inscripción utilizando los datos proporcionados en el DTO.
    /// Verifica que la institución asociada exista antes de realizar la inserción.
    /// </summary>
    /// <param name="enrollmentPeriodDTO">El objeto de transferencia de datos que contiene la información del periodo de inscripción a agregar.</param>
    /// <returns>Una respuesta que indica el éxito o fracaso de la operación, junto con el periodo de inscripción agregado si es exitoso.</returns>

    public async Task<ActionResponse<EnrollmentPeriod>> AddAsync(EnrollmentPeriodDTO enrollmentPeriodDTO)
    {
        // Accede a la propiedad InstitutionID a través de la instancia enrollmentPeriodDTO
        var institution = await _context.Institutions.FindAsync(enrollmentPeriodDTO.InstitutionID);

        if (institution == null)
        {
            return new ActionResponse<EnrollmentPeriod>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        var enrollmentPeriod = new EnrollmentPeriod
        {
            InstitutionID = enrollmentPeriodDTO.InstitutionID,
            PeriodName = enrollmentPeriodDTO.PeriodName.Trim(),
            StartDateEnrollment = enrollmentPeriodDTO.StartDateEnrollment,
            EndDateEnrollment = enrollmentPeriodDTO.EndDateEnrollment,
            StartDateExam = enrollmentPeriodDTO.StartDateExam,
            EndDateExam = enrollmentPeriodDTO.EndDateExam
        };

        _context.Add(enrollmentPeriod);

        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<EnrollmentPeriod>
            {
                WasSuccess = true,
                Result = enrollmentPeriod
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<EnrollmentPeriod>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<EnrollmentPeriod>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    /// <summary>
    /// Obtiene todos los periodos de inscripción asociados a una institución específica, ordenados por nombre del periodo.
    /// </summary>
    /// <param name="institutionID">El identificador de la institución cuyo periodo de inscripción se desea obtener.</param>
    /// <returns>Una lista de periodos de inscripción asociados a la institución, ordenados por nombre.</returns>

    public async Task<IEnumerable<EnrollmentPeriod>> GetComboAsync(int institutionID)
    {
        return await _context.EnrollmentPeriods
               .Where(x => x.InstitutionID == institutionID)
               .OrderBy(x => x.PeriodName)
               .ToListAsync();
    }

    /// <summary>
    /// Obtiene el total de registros de periodos de inscripción que cumplen con los criterios de filtrado proporcionados.
    /// </summary>
    /// <param name="pagination">El objeto de paginación que contiene los parámetros de filtrado.</param>
    /// <returns>Una respuesta que contiene el número total de registros de periodos de inscripción que cumplen con el filtro especificado.</returns>

    public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.EnrollmentPeriods.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Institution!.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }

    /// <summary>
    /// Actualiza un periodo de inscripción existente con los nuevos datos proporcionados en el DTO.
    /// Verifica que el periodo de inscripción y la institución existan antes de realizar la actualización.
    /// </summary>
    /// <param name="enrollmentPeriodDTO">El objeto de transferencia de datos que contiene la información actualizada del periodo de inscripción.</param>
    /// <returns>Una respuesta que indica el éxito o fracaso de la operación, junto con el periodo de inscripción actualizado si es exitoso.</returns>

    public async Task<ActionResponse<EnrollmentPeriod>> UpdateAsync(EnrollmentPeriodDTO enrollmentPeriodDTO)
    {
        var currentEnrollmentPeriod = await _context.EnrollmentPeriods.FindAsync(enrollmentPeriodDTO.EnrollmentPeriodID);
        if (currentEnrollmentPeriod == null)
        {
            return new ActionResponse<EnrollmentPeriod>
            {
                WasSuccess = false,
                Message = "ERR005"
            };
        }

        var institution = await _context.Institutions.FindAsync(enrollmentPeriodDTO.InstitutionID);
        if (institution == null)
        {
            return new ActionResponse<EnrollmentPeriod>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        currentEnrollmentPeriod.Institution = institution;
        currentEnrollmentPeriod.PeriodName = enrollmentPeriodDTO.PeriodName.Trim();
        currentEnrollmentPeriod.StartDateEnrollment = enrollmentPeriodDTO.StartDateEnrollment;
        currentEnrollmentPeriod.EndDateEnrollment = enrollmentPeriodDTO.EndDateEnrollment;
        currentEnrollmentPeriod.StartDateExam = enrollmentPeriodDTO.StartDateExam;
        currentEnrollmentPeriod.EndDateExam = enrollmentPeriodDTO.EndDateExam;

        _context.Update(currentEnrollmentPeriod);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<EnrollmentPeriod>
            {
                WasSuccess = true,
                Result = currentEnrollmentPeriod
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<EnrollmentPeriod>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<EnrollmentPeriod>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    /// <summary>
    /// Obtiene una lista de periodos de inscripción con paginación y filtrado por nombre del periodo.
    /// </summary>
    /// <param name="pagination">El objeto de paginación que contiene los parámetros de filtrado y paginación.</param>
    /// <returns>Una respuesta que contiene la lista de periodos de inscripción paginados según los parámetros proporcionados.</returns>

    public override async Task<ActionResponse<IEnumerable<EnrollmentPeriod>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.EnrollmentPeriods
            .Include(x => x.Institution)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            //queryable = queryable.Where(x => x.Institution!.Name.ToLower().Contains(pagination.Filter.ToLower()));
            queryable = queryable.Where(x => x.PeriodName.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<EnrollmentPeriod>>
        {
            WasSuccess = true,
            Result = await queryable
                .OrderBy(x => x.PeriodName)
                .Paginate(pagination)
                .ToListAsync()
        };
    }
}