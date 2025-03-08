using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.Repositories.Interfaces;

public interface IAcademicprogramsRepository
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="institutionID"></param>
    /// <returns></returns>
    Task<IEnumerable<AcademicProgram>> GetComboAsync(int institutionID);

    /// <summary>
    ///
    /// </summary>
    /// <param name="academicprogramDTO"></param>
    /// <returns></returns>
    Task<ActionResponse<AcademicProgram>> AddAsync(AcademicProgramDTO academicprogramDTO);

    /// <summary>
    ///
    /// </summary>
    /// <param name="academicprogramDTO"></param>
    /// <returns></returns>
    Task<ActionResponse<AcademicProgram>> UpdateAsync(AcademicProgramDTO academicprogramDTO);

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ActionResponse<AcademicProgram>> GetAsync(int id);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<ActionResponse<IEnumerable<AcademicProgram>>> GetAsync();

    // Nuevo método para obtener categorías (NO ES LO IDEAL, PERO SE PUEDE HACER TEMPORALMENTE)
    Task<IEnumerable<Category>> GetComboCategoriesAsync();

    /// <summary>
    ///
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    Task<ActionResponse<IEnumerable<AcademicProgram>>> GetAsync(PaginationDTO pagination);

    /// <summary>
    ///
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);
}