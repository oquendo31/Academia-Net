using AcademiaNet.Backend.Repositories.Implementations;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.UnitsOfWork.Implementations;

public class AcademicProgramsUnitOfWork : GenericUnitOfWork<AcademicProgram>, IAcademicProgramsUnitOfWorks
{
    private readonly IAcademicprogramsRepository _academicprogramsRepository;
    private readonly ICategoryRepository _categoryRepository; // Nuevo

    public AcademicProgramsUnitOfWork(
     IGenericRepository<AcademicProgram> repository,
     IAcademicprogramsRepository academicprogramsRepository,
     ICategoryRepository categoryRepository)
     : base(repository)
    {
        _academicprogramsRepository = academicprogramsRepository;
        _categoryRepository = categoryRepository; // Asignar repositorio
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

    public async Task<IEnumerable<Category>> GetComboCategoriesAsync()
    {
        var response = await _categoryRepository.GetComboCategoriesAsync();

        if (response.WasSuccess)
        {
            return response.Result ?? Enumerable.Empty<Category>();  // Devuelve la lista o una lista vacía
        }
        else
        {
            // Manejo de error: puedes registrar un log o lanzar una excepción según lo requiera tu lógica
            throw new Exception($"Error al obtener categorías: {response.Message}");
        }
    }
}