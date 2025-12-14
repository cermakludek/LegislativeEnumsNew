using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/building-uses")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkBuildingUsesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkBuildingUsesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BuildingUse>>> GetAll()
    {
        var items = await _context.CuzkBuildingUses
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BuildingUse>> GetById(long id)
    {
        var item = await _context.CuzkBuildingUses.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<BuildingUse>> GetByCode(string code)
    {
        var item = await _context.CuzkBuildingUses
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
