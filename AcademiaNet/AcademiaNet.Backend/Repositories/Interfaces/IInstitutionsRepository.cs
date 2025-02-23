using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;
using System.Diagnostics.Metrics;

namespace AcademiaNet.Backend.Repositories.Interfaces;

public interface IInstitutionsRepository
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