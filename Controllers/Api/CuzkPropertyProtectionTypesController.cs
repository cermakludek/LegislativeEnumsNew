using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/property-protection-types")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkPropertyProtectionTypesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkPropertyProtectionTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyProtectionType>>> GetAll()
    {
        var items = await _context.CuzkPropertyProtectionTypes
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyProtectionType>> GetById(long id)
    {
        var item = await _context.CuzkPropertyProtectionTypes.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<PropertyProtectionType>> GetByCode(string code)
    {
        var item = await _context.CuzkPropertyProtectionTypes
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
