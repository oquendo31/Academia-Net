using Microsoft.EntityFrameworkCore;

namespace AcademiaNet.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckInstitutionsAsync();
        await CheckAcademicProgramsAsync();
    }

    private async Task CheckInstitutionsAsync()
    {
        if (!_context.Institutions.Any())
        {
            var institutionsSQLScript = File.ReadAllText("Data\\Institutions.sql");
            await _context.Database.ExecuteSqlRawAsync(institutionsSQLScript);
        }
    }

    private async Task CheckAcademicProgramsAsync()
    {
        if (!_context.AcademicPrograms.Any())
        {
            var academicProgramsSQLScript = File.ReadAllText("Data\\AcademicPrograms.sql");
            await _context.Database.ExecuteSqlRawAsync(academicProgramsSQLScript);
        }
    }
}
