using AcademiaNet.Shared.DTOs;
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

    /// <summary>
    ///
    /// </summary>
    /// <param name="institutionDTO"></param>
    /// <returns></returns>
    Task<ActionResponse<Institution>> AddAsync(InstitutionDTO institutionDTO);

    /// <summary>
    /// Trae la lista de las ubicaciones
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Location>> GetComboLocationsAsync();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="institutionDTO"></param>
    /// <returns></returns>
    Task<ActionResponse<Institution>> UpdateAsync(InstitutionDTO institutionDTO);
}