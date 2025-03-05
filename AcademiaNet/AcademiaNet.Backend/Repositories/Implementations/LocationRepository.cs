using AcademiaNet.Backend.Data;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace AcademiaNet.Backend.Repositories.Implementations;

public class LocationRepository : ILocationRepository
{
    private readonly DataContext _context;

    public LocationRepository(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Trae la lista de las ubicaciones
    /// </summary>
    /// <returns></returns>
    public async Task<ActionResponse<IEnumerable<Location>>> GetComboLocationsAsync()
    {
        var locations = await _context.Locations
            .OrderBy(c => c.Name)
            .ToListAsync();

        return new ActionResponse<IEnumerable<Location>>
        {
            WasSuccess = true,
            Result = locations
        };
    }
}