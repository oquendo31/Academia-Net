using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.Repositories.Interfaces;

public interface IExamsRepository
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Exam>> GetComboAsync();

    /// <summary>
    ///
    /// </summary>
    /// <param name="examenDTO"></param>
    /// <returns></returns>
    Task<ActionResponse<Exam>> AddAsync(ExamenDTO examenDTO);

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ActionResponse<Exam>> GetAsync(int id);
}