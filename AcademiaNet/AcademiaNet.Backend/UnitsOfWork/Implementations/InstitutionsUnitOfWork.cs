using AcademiaNet.Backend.Repositories.Implementations;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;
using System.Diagnostics.Metrics;

namespace AcademiaNet.Backend.UnitsOfWork.Implementations;

//public class InstitutionsUnitOfWork
//{
//}
public class InstitutionsUnitOfWork : GenericUnitOfWork<Institution>, IInstitutionsUnitOfWork
{
    private readonly IInstitutionsRepository _institutionsRepository;

    public InstitutionsUnitOfWork(IGenericRepository<Institution> repository, IInstitutionsRepository institutionsRepository) : base(repository)
    {
        _institutionsRepository = institutionsRepository;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public override async Task<ActionResponse<IEnumerable<Institution>>> GetAsync() => await _institutionsRepository.GetAsync();

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task<ActionResponse<Institution>> GetAsync(int id) => await _institutionsRepository.GetAsync(id);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Institution>> GetComboAsync() => await _institutionsRepository.GetComboAsync();
}