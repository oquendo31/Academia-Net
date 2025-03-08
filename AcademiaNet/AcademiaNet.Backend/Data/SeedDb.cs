using AcademiaNet.Backend.Helpers;
using AcademiaNet.Shared.Entites;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AcademiaNet.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IFileStorage _fileStorage;

    public SeedDb(DataContext context, IFileStorage fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCategoriesAsync();
        await CheckLocationsAsync();
        await CheckInstitutionsAsync();
        await CheckAcademicProgramsAsync();
        await CheckInstitutionsImagesAsync();
    }

    private async Task CheckLocationsAsync()
    {
        if (!_context.Locations.Any())
        {
            var locationsSQLScript = File.ReadAllText("Data\\Locations.sql");
            await _context.Database.ExecuteSqlRawAsync(locationsSQLScript);
        }
    }

    private async Task CheckInstitutionsImagesAsync()
    {
        var institutions = _context.Institutions.ToList();

        foreach (var institution in institutions)
        {
            if (string.IsNullOrEmpty(institution.Photo))
            {
                var imagePath = string.Empty;
                var fileExtensions = new[] { ".png", ".jpeg", ".jpg" };

                string? filePath = null;

                foreach (var extension in fileExtensions)
                {
                    var tempPath = Path.Combine(Environment.CurrentDirectory, "Images", "Flags", $"{institution.Name}{extension}");

                    if (File.Exists(tempPath))
                    {
                        filePath = tempPath;
                        break; // Apenas encuentre una imagen, sal del bucle
                    }
                }

                if (filePath != null)
                {
                    // Si encontró alguna imagen, la lee
                    var fileBytes = await File.ReadAllBytesAsync(filePath);

                    // Detecta la extensión real (sin el punto)
                    var fileExtension = Path.GetExtension(filePath).TrimStart('.');

                    // Guarda el archivo en el sistema de almacenamiento
                    imagePath = await _fileStorage.SaveFileAsync(fileBytes, fileExtension, "institutions");

                    // Actualiza el campo Photo
                    institution.Photo = imagePath;
                }
                else
                {
                    // Si no encontró ninguna, asigna imagen por defecto
                    institution.Photo = "/images/NoImage.png";
                }
            }
        }

        await _context.SaveChangesAsync();
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

    private async Task CheckCategoriesAsync()
    {
        if (!_context.Categories.Any())
        {
            var categoriesSQLScript = File.ReadAllText("Data\\Categories.sql");
            await _context.Database.ExecuteSqlRawAsync(categoriesSQLScript);
        }
    }
}