using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using AcademiaNet.Shared.Entites;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaNet.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AcademicProgramsController : GenericController<AcademicProgram>
{
    public AcademicProgramsController(IGenericUnitOfWork<AcademicProgram> unit) : base(unit)
    {
    }
}