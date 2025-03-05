using AcademiaNet.Shared.Entites;
using AcademiaNet.Shared.Responses;

namespace AcademiaNet.Backend.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<ActionResponse<IEnumerable<Category>>> GetComboCategoriesAsync();
}