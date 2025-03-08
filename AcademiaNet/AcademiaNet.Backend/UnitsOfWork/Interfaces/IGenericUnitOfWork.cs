using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.UnitsOfWork.Interfaces;

public interface IGenericUnitOfWork<T> where T : class
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<ActionResponse<IEnumerable<T>>> GetAsync();

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ActionResponse<T>> AddAsync(T model);

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ActionResponse<T>> UpdateAsync(T model);

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ActionResponse<T>> DeleteAsync(int id);

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ActionResponse<T>> GetAsync(int id);

    /// <summary>
    ///
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<ActionResponse<int>> GetTotalRecordsAsync();
}