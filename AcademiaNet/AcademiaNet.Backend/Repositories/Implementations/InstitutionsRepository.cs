using AcademiaNet.Backend.Data;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AcademiaNet.Backend.Repositories.Implementations;

//public class InstitutionsRepository
//{
//}

public class InstitutionsRepository : GenericRepository<Institution>, IInstitutionsRepository
{
    private readonly DataContext _context;

    public InstitutionsRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public override async Task<ActionResponse<IEnumerable<Institution>>> GetAsync()
    {
        var institutions = await _context.Institutions // Esto se refiere a un inner join me trae ambas tablas
            .Include(x => x.AcademicPrograms)
            .ToListAsync();
        return new ActionResponse<IEnumerable<Institution>>
        {
            WasSuccess = true,
            Result = institutions
        };
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task<ActionResponse<Institution>> GetAsync(int id)
    {
        var institution = await _context.Institutions
             .Include(x => x.AcademicPrograms)
             .FirstOrDefaultAsync(x => x.InstitutionID == id);

        if (institution == null)
        {
            return new ActionResponse<Institution>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<Institution>
        {
            WasSuccess = true,
            Result = institution
        };
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Institution>> GetComboAsync()
    {
        return await _context.Institutions
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}