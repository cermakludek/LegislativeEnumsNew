using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/cuzk/building-types")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class CuzkBuildingTypesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CuzkBuildingTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BuildingType>>> GetAll()
    {
        var items = await _context.CuzkBuildingTypes
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BuildingType>> GetById(long id)
    {
        var item = await _context.CuzkBuildingTypes.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<BuildingType>> GetByCode(string code)
    {
        var item = await _context.CuzkBuildingTypes
            .FirstOrDefaultAsync(b => b.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
