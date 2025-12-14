using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/soil-ecological-units")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkSoilEcologicalUnitsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkSoilEcologicalUnitsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SoilEcologicalUnit>>> GetAll()
    {
        var items = await _context.CuzkSoilEcologicalUnits
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SoilEcologicalUnit>> GetById(long id)
    {
        var item = await _context.CuzkSoilEcologicalUnits.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<SoilEcologicalUnit>> GetByCode(string code)
    {
        var item = await _context.CuzkSoilEcologicalUnits
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
