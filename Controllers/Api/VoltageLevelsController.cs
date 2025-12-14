using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/voltage-levels")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class VoltageLevelsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VoltageLevelsController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all voltage levels
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VoltageLevel>>> GetAll()
    {
        var items = await _context.VoltageLevels
            .OrderBy(v => v.SortOrder)
            .ToListAsync();
        return Ok(items);
    }

    /// <summary>
    /// Get voltage level by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<VoltageLevel>> GetById(long id)
    {
        var item = await _context.VoltageLevels.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// Get voltage level by code
    /// </summary>
    [HttpGet("code/{code}")]
    public async Task<ActionResult<VoltageLevel>> GetByCode(string code)
    {
        var item = await _context.VoltageLevels
            .FirstOrDefaultAsync(v => v.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
