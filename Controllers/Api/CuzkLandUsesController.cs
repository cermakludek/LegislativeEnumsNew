using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/land-uses")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkLandUsesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkLandUsesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LandUse>>> GetAll()
    {
        var items = await _context.CuzkLandUses
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LandUse>> GetById(long id)
    {
        var item = await _context.CuzkLandUses.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<LandUse>> GetByCode(string code)
    {
        var item = await _context.CuzkLandUses
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
