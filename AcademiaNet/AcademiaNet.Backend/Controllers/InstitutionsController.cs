using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademiaNet.Backend.Data;
using AcademiaNet.Shared.Entites;

namespace AcademiaNet.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstitutionsController : ControllerBase
{
    private readonly DataContext _context;

    public InstitutionsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _context.Institutions.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var country = await _context.Institutions.FirstOrDefaultAsync(c => c.InstitutionID == id);
        if (country == null)
        {
            return NotFound();
        }

        return Ok(country);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Institution country)
    {
        _context.Add(country);
        await _context.SaveChangesAsync();
        return Ok(country);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var country = await _context.Institutions.FirstOrDefaultAsync(c => c.InstitutionID == id);
        if (country == null)
        {
            return NotFound();
        }

        _context.Remove(country);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(Institution country)
    {
        _context.Update(country);
        await _context.SaveChangesAsync();
        return Ok(country);
    }
}