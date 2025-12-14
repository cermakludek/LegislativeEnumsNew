using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/unit-uses")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkUnitUsesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkUnitUsesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnitUse>>> GetAll()
    {
        var items = await _context.CuzkUnitUses
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitUse>> GetById(long id)
    {
        var item = await _context.CuzkUnitUses.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<UnitUse>> GetByCode(string code)
    {
        var item = await _context.CuzkUnitUses
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
