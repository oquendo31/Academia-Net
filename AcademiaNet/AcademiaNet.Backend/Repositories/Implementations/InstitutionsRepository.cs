using AcademiaNet.Backend.Data;
using AcademiaNet.Backend.Helpers;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Shared.DTOs;
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
    private readonly IFileStorage _fileStorage;

    public InstitutionsRepository(DataContext context, IFileStorage fileStorage) : base(context)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    /// <summary>
    /// Guarda la institucion con la foto
    /// </summary>
    /// <param name="institutionDTO"></param>
    /// <returns></returns>
    public async Task<ActionResponse<Institution>> AddAsync(InstitutionDTO institutionDTO)
    {
        var institution = new Institution
        {
            Name = institutionDTO.Name.Trim(),
            LocationID = institutionDTO.LocationID,
            Description = institutionDTO.Description
        };

        if (!string.IsNullOrEmpty(institutionDTO.Photo))
        {
            var imageBase64 = Convert.FromBase64String(institutionDTO.Photo!);
            institution.Photo = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "institutions");
        }

        _context.Add(institution);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Institution>
            {
                WasSuccess = true,
                Result = institution
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Institution>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<Institution>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
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