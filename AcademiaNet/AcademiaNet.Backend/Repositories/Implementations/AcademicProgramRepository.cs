using AcademiaNet.Backend.Data;
using AcademiaNet.Backend.Helpers;
using AcademiaNet.Backend.Repositories.Interfaces;
using AcademiaNet.Shared.DTOs;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace AcademiaNet.Backend.Repositories.Implementations;

public class AcademicProgramRepository : GenericRepository<AcademicProgram>, IAcademicprogramsRepository
{
    private readonly DataContext _context;

    public AcademicProgramRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public override async Task<ActionResponse<IEnumerable<AcademicProgram>>> GetAsync()
    {
        var academicprograms = await _context.AcademicPrograms
            .Include(x => x.Institution)  // Relación con Institution
            .Include(x => x.Category)     // Relación con Category
            .OrderBy(x => x.Name)
            .ToListAsync();

        return new ActionResponse<IEnumerable<AcademicProgram>>
        {
            WasSuccess = true,
            Result = academicprograms
        };
    }

    /// <summary>
    /// prueba commit Juan felipe
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task<ActionResponse<AcademicProgram>> GetAsync(int id)
    {
        var academicprogram = await _context.AcademicPrograms
                 .Include(x => x.Institution)
                 .FirstOrDefaultAsync(c => c.AcademicProgramID == id);

        if (academicprogram == null)
        {
            return new ActionResponse<AcademicProgram>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<AcademicProgram>
        {
            WasSuccess = true,
            Result = academicprogram
        };
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="academicprogramDTO"></param>
    /// <returns></returns>
    public async Task<ActionResponse<AcademicProgram>> AddAsync(AcademicProgramDTO academicProgramDTO)
    {
        var institution = await _context.Institutions.FindAsync(academicProgramDTO.InstitutionID);
        if (institution == null)
        {
            return new ActionResponse<AcademicProgram>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        var academicprogram = new AcademicProgram
        {
            InstitutionID = academicProgramDTO.InstitutionID,
            Name = academicProgramDTO.Name.Trim(),
            CategoryID = academicProgramDTO.CategoryID
        };

        //if (!string.IsNullOrEmpty(teamDTO.Image))
        //{
        //    var imageBase64 = Convert.FromBase64String(teamDTO.Image!);
        //    team.Image = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "teams");
        //}

        _context.Add(academicprogram);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<AcademicProgram>
            {
                WasSuccess = true,
                Result = academicprogram
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<AcademicProgram>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<AcademicProgram>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="institutionID"></param>
    /// <returns></returns>
    public async Task<IEnumerable<AcademicProgram>> GetComboAsync(int institutionID)
    {
        return await _context.AcademicPrograms
               .Where(x => x.InstitutionID == institutionID)
               .OrderBy(x => x.Name)
               .ToListAsync();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="academicProgramDTO"></param>
    /// <returns></returns>
    public async Task<ActionResponse<AcademicProgram>> UpdateAsync(AcademicProgramDTO academicProgramDTO)
    {
        var currentAcademicProgram = await _context.AcademicPrograms.FindAsync(academicProgramDTO.AcademicProgramID);
        if (currentAcademicProgram == null)
        {
            return new ActionResponse<AcademicProgram>
            {
                WasSuccess = false,
                Message = "ERR005"
            };
        }

        var institution = await _context.Institutions.FindAsync(academicProgramDTO.InstitutionID);
        if (institution == null)
        {
            return new ActionResponse<AcademicProgram>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        //if (!string.IsNullOrEmpty(teamDTO.Image))
        //{
        //    var imageBase64 = Convert.FromBase64String(teamDTO.Image!);
        //    currentTeam.Image = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "teams");
        //}

        currentAcademicProgram.Institution = institution;
        currentAcademicProgram.Name = academicProgramDTO.Name;

        _context.Update(currentAcademicProgram);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<AcademicProgram>
            {
                WasSuccess = true,
                Result = currentAcademicProgram
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<AcademicProgram>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception exception)
        {
            return new ActionResponse<AcademicProgram>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }
    }

    public async Task<IEnumerable<Category>> GetComboCategoriesAsync()
    {
        return await _context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="pagination"></param>
    /// <returns></returns>
    public override async Task<ActionResponse<IEnumerable<AcademicProgram>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.AcademicPrograms
            .Include(x => x.Category)
            .Include(x => x.Institution)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            //queryable = queryable.Where(x => x.Institution!.Name.ToLower().Contains(pagination.Filter.ToLower()));
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<AcademicProgram>>
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
        var queryable = _context.AcademicPrograms.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Institution!.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }
}