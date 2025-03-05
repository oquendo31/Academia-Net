using AcademiaNet.Backend.Repositories.Implementations;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.DTOs;
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
    private readonly ILocationRepository _locationRepository; // Nuevo

    public InstitutionsUnitOfWork(IGenericRepository<Institution> repository, IInstitutionsRepository institutionsRepository, ILocationRepository Locationrepository) : base(repository)
    {
        _institutionsRepository = institutionsRepository;
        _locationRepository = Locationrepository;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="institutionDTO"></param>
    /// <returns></returns>
    public async Task<ActionResponse<Institution>> AddAsync(InstitutionDTO institutionDTO) => await _institutionsRepository.AddAsync(institutionDTO);

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

    /// <summary>
    /// Trae la lista de las ubicaciones
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IEnumerable<Location>> GetComboLocationsAsync()
    {
        var response = await _locationRepository.GetComboLocationsAsync();

        if (response.WasSuccess)
        {
            return response.Result ?? Enumerable.Empty<Location>();  // Devuelve la lista o una lista vacía
        }
        else
        {
            // Manejo de error: puedes registrar un log o lanzar una excepción según lo requiera tu lógica
            throw new Exception($"Error al obtener lista de ubicaciones: {response.Message}");
        }
    }
}