using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.Repositories.Interfaces;

/// <summary>
///
/// </summary>
public interface ILocationRepository
{
    Task<ActionResponse<IEnumerable<Location>>> GetComboLocationsAsync();
}