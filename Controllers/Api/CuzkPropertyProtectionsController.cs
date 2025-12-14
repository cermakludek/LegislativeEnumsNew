using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/property-protections")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkPropertyProtectionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkPropertyProtectionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyProtection>>> GetAll()
    {
        var items = await _context.CuzkPropertyProtections
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyProtection>> GetById(long id)
    {
        var item = await _context.CuzkPropertyProtections.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<PropertyProtection>> GetByCode(string code)
    {
        var item = await _context.CuzkPropertyProtections
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
