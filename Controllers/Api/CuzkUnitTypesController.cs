using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/unit-types")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkUnitTypesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkUnitTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnitType>>> GetAll()
    {
        var items = await _context.CuzkUnitTypes
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitType>> GetById(long id)
    {
        var item = await _context.CuzkUnitTypes.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<UnitType>> GetByCode(string code)
    {
        var item = await _context.CuzkUnitTypes
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
