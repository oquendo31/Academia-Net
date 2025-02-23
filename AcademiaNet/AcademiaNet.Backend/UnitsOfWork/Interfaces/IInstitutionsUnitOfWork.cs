using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;
using System.Diagnostics.Metrics;

namespace AcademiaNet.Backend.UnitsOfWork.Interfaces;

public interface IInstitutionsUnitOfWork
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ActionResponse<Institution>> GetAsync(int id);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<ActionResponse<IEnumerable<Institution>>> GetAsync();

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Institution>> GetComboAsync();
}