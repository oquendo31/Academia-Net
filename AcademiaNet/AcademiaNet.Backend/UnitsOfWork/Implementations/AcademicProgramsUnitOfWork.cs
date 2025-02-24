using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.UnitsOfWork.Implementations;

public class AcademicProgramsUnitOfWork : GenericUnitOfWork<AcademicProgram>, IAcademicProgramsUnitOfWorks
{
    private readonly IAcademicprogramsRepository _academicprogramsRepository;

    public AcademicProgramsUnitOfWork(IGenericRepository<AcademicProgram> repository, IAcademicprogramsRepository academicprogramsRepository) : base(repository)
    {
        _academicprogramsRepository = academicprogramsRepository;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="academicProgramDTO"></param>
    /// <returns></returns>
    public async Task<ActionResponse<AcademicProgram>> AddAsync(AcademicProgramDTO academicProgramDTO) => await _academicprogramsRepository.AddAsync(academicProgramDTO);

    /// <summary>
    ///
    /// </summary>
    /// <param name="countryId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<AcademicProgram>> GetComboAsync(int countryId) => await _academicprogramsRepository.GetComboAsync(countryId);

    /// <summary>
    ///
    /// </summary>
    /// <param name="academicProgramDTO"></param>
    /// <returns></returns>
    public async Task<ActionResponse<AcademicProgram>> UpdateAsync(AcademicProgramDTO academicProgramDTO) => await _academicprogramsRepository.UpdateAsync(academicProgramDTO);

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task<ActionResponse<AcademicProgram>> GetAsync(int id) => await _academicprogramsRepository.GetAsync(id);

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public override async Task<ActionResponse<IEnumerable<AcademicProgram>>> GetAsync() => await _academicprogramsRepository.GetAsync();
}