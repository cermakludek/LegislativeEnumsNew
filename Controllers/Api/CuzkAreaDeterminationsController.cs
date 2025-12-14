using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/area-determinations")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkAreaDeterminationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkAreaDeterminationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AreaDetermination>>> GetAll()
    {
        var items = await _context.CuzkAreaDeterminations
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AreaDetermination>> GetById(long id)
    {
        var item = await _context.CuzkAreaDeterminations.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<AreaDetermination>> GetByCode(string code)
    {
        var item = await _context.CuzkAreaDeterminations
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
