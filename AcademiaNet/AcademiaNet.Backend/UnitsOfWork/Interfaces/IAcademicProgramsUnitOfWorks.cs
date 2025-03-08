using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.UnitsOfWork.Interfaces;

public interface IAcademicProgramsUnitOfWorks
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="institutionID"></param>
    /// <returns></returns>
    Task<IEnumerable<AcademicProgram>> GetComboAsync(int institutionID);

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

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
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