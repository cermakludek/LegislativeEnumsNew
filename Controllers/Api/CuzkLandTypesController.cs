using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/land-types")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkLandTypesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkLandTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LandType>>> GetAll()
    {
        var items = await _context.CuzkLandTypes
            .OrderBy(l => l.SortOrder)
            .ThenBy(l => l.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LandType>> GetById(long id)
    {
        var item = await _context.CuzkLandTypes.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<LandType>> GetByCode(string code)
    {
        var item = await _context.CuzkLandTypes
            .FirstOrDefaultAsync(l => l.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
