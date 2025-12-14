using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/simplified-parcel-sources")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkSimplifiedParcelSourcesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkSimplifiedParcelSourcesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SimplifiedParcelSource>>> GetAll()
    {
        var items = await _context.CuzkSimplifiedParcelSources
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SimplifiedParcelSource>> GetById(long id)
    {
        var item = await _context.CuzkSimplifiedParcelSources.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<SimplifiedParcelSource>> GetByCode(string code)
    {
        var item = await _context.CuzkSimplifiedParcelSources
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
