using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.UnitsOfWork.Implementations;

public class ExamsUnitOfWork : GenericUnitOfWork<Exam>, IExamsUnitOfWorks
{
    private readonly IExamsRepository _ExamsRepository;

    public ExamsUnitOfWork(IGenericRepository<Exam> repository, IExamsRepository ExamsRepository) : base(repository)
    {
        _ExamsRepository = ExamsRepository;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    ///
    public override async Task<ActionResponse<Exam>> GetAsync(int id) => await _ExamsRepository.GetAsync(id);

    public Task<IEnumerable<Exam>> GetComboAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="examenDTO"></param>
    /// <returns></returns>
    async Task<ActionResponse<Exam>> IExamsUnitOfWorks.AddAsync(ExamenDTO examenDTO)
    {
        var response = await _ExamsRepository.AddAsync(examenDTO);

        if (!response.WasSuccess)
        {
            return response;
        }
        //await SaveChangesAsync();
        return response;
    }
}