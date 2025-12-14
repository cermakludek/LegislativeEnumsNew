using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/building-right-purposes")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkBuildingRightPurposesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkBuildingRightPurposesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BuildingRightPurpose>>> GetAll()
    {
        var items = await _context.CuzkBuildingRightPurposes
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BuildingRightPurpose>> GetById(long id)
    {
        var item = await _context.CuzkBuildingRightPurposes.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<BuildingRightPurpose>> GetByCode(string code)
    {
        var item = await _context.CuzkBuildingRightPurposes
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
