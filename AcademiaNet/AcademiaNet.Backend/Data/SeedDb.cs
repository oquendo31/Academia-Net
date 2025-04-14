using AcademiaNet.Backend.Helpers;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AcademiaNet.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IFileStorage _fileStorage;
    private readonly IUsersUnitOfWork _usersUnitOfWork;

    public SeedDb(DataContext context, IFileStorage fileStorage, IUsersUnitOfWork usersUnitOfWork)
    {
        _context = context;
        _fileStorage = fileStorage;
        _usersUnitOfWork = usersUnitOfWork;
    }

    /// <summary>
    /// Seed Async
    /// </summary>
    /// <returns></returns>
    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCategoriesAsync();
        await CheckLocationsAsync();
        await CheckInstitutionsAsync();
        await CheckAcademicProgramsAsync();
        await CheckInstitutionsImagesAsync();
        await CheckRolesAsync();
        await CheckUserAsync("Juan", "Oquendo", "felipeoquendo@hotmail.com", "322 509 9025", UserType.Admin);
    }

    /// <summary>
    /// Check Locations Async
    /// </summary>
    /// <returns></returns>
    private async Task CheckLocationsAsync()
    {
        if (!_context.Locations.Any())
        {
            var locationsSQLScript = File.ReadAllText("Data\\Locations.sql");
            await _context.Database.ExecuteSqlRawAsync(locationsSQLScript);
        }
    }

    /// <summary>
    /// Check Institutions Images Async
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Check Institutions Async
    /// </summary>
    /// <returns></returns>
    private async Task CheckInstitutionsAsync()
    {
        if (!_context.Institutions.Any())
        {
            var institutionsSQLScript = File.ReadAllText("Data\\Institutions.sql");
            await _context.Database.ExecuteSqlRawAsync(institutionsSQLScript);
        }
    }

    /// <summary>
    /// Check Academic Programs Async
    /// </summary>
    /// <returns></returns>
    private async Task CheckAcademicProgramsAsync()
    {
        if (!_context.AcademicPrograms.Any())
        {
            var academicProgramsSQLScript = File.ReadAllText("Data\\AcademicPrograms.sql");
            await _context.Database.ExecuteSqlRawAsync(academicProgramsSQLScript);
        }
    }

    /// <summary>
    /// Check Categories Async
    /// </summary>
    /// <returns></returns>
    private async Task CheckCategoriesAsync()
    {
        if (!_context.Categories.Any())
        {
            var categoriesSQLScript = File.ReadAllText("Data\\Categories.sql");
            await _context.Database.ExecuteSqlRawAsync(categoriesSQLScript);
        }
    }

    /// <summary>
    /// Check Roles Async
    /// </summary>
    /// <returns></returns>
    private async Task CheckRolesAsync()
    {
        await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
        await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
    }

    /// <summary>
    /// Check User Async
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="phone"></param>
    /// <param name="userType"></param>
    /// <returns></returns>
    private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phone, UserType userType)
    {
        var user = await _usersUnitOfWork.GetUserAsync(email);
        if (user == null)
        {
            var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Name == "Universidad Nacional de Colombia");
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Institution = institution!,
                UserType = userType,
            };

            await _usersUnitOfWork.AddUserAsync(user, "123456");
            await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
    }
}