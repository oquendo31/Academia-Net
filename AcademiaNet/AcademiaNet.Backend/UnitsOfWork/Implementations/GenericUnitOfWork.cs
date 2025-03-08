using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.UnitsOfWork.Implementations;

public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public GenericUnitOfWork(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public virtual async Task<ActionResponse<T>> AddAsync(T model) => await _repository.AddAsync(model);

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<ActionResponse<T>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<ActionResponse<T>> GetAsync(int id) => await _repository.GetAsync(id);

    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public virtual async Task<ActionResponse<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);

    /// <summary>
    ///
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination) => await _repository.GetAsync(pagination);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync() => await _repository.GetTotalRecordsAsync();
}