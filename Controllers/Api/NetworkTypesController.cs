using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/network-types")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class NetworkTypesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public NetworkTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NetworkType>>> GetAll()
    {
        var items = await _context.NetworkTypes
            .OrderBy(n => n.SortOrder)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NetworkType>> GetById(long id)
    {
        var item = await _context.NetworkTypes.FindAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<NetworkType>> GetByCode(string code)
    {
        var item = await _context.NetworkTypes
            .FirstOrDefaultAsync(n => n.Code == code);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}
