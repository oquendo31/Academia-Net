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
    /// Update Async Institution
    /// </summary>
    /// <param name="institutionDTO"></param>
    /// <returns></returns>
    public async Task<ActionResponse<Institution>> UpdateAsync(InstitutionDTO institutionDTO)
    {
        var currentnstitution = await _context.Institutions.FindAsync(institutionDTO.InstitutionID);
        if (currentnstitution == null)
        {
            return new ActionResponse<Institution>
            {
                WasSuccess = false,
                Message = "ERR005"
            };
        }

        if (!string.IsNullOrEmpty(institutionDTO.Photo))
        {
            var imageBase64 = Convert.FromBase64String(institutionDTO.Photo!);
            currentnstitution.Photo = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "institutions");
        }

        currentnstitution.Name = institutionDTO.Name;
        currentnstitution.LocationID = institutionDTO.LocationID;
        currentnstitution.Description = institutionDTO.Description;

        _context.Update(currentnstitution);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Institution>
            {
                WasSuccess = true,
                Result = currentnstitution
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
            .Include(x => x.Location)     // Relación con Location
            .OrderBy(x => x.Name)
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

    /// <summary>
    ///
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    public override async Task<ActionResponse<IEnumerable<Institution>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Institutions
            .Include(x => x.AcademicPrograms)
            .Include(x => x.Location)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<Institution>>
        {
            WasSuccess = true,
            Result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    public async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Institutions.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }
}