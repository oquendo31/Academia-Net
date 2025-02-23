using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademiaNet.Backend.Data;
using AcademiaNet.Shared.Entites;
using AcademiaNet.Backend.UnitsOfWork.Interfaces;
using System.Diagnostics.Metrics;

namespace AcademiaNet.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstitutionsController : GenericController<Institution>
{
    public InstitutionsController(IGenericUnitOfWork<Institution> unit) : base(unit)
    {
    }
}