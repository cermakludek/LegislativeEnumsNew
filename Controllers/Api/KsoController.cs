using LegislativeEnumsNew.Data;
using LegislativeEnumsNew.Data.Entities.Kso;
using LegislativeEnumsNew.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Controllers.Api;

[ApiController]
[Route("api/kso")]
[Produces("application/json", "application/xml")]
[ApiFormatFilter]
public class KsoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public KsoController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAll([FromQuery] int? level = null)
    {
        var query = _context.BuildingClassifications.AsQueryable();

        if (level.HasValue)
        {
            query = query.Where(b => b.Level == level.Value);
        }

        var items = await query
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .Select(b => new
            {
                b.Id,
                b.Code,
                b.NameCs,
                b.NameEn,
                b.DescriptionCs,
                b.DescriptionEn,
                b.Level,
                b.ParentId,
                b.ValidFrom,
                b.ValidTo,
                b.SortOrder,
                b.CreatedAt,
                b.UpdatedAt
            })
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetById(long id)
    {
        var item = await _context.BuildingClassifications
            .Where(b => b.Id == id)
            .Select(b => new
            {
                b.Id,
                b.Code,
                b.NameCs,
                b.NameEn,
                b.DescriptionCs,
                b.DescriptionEn,
                b.Level,
                b.ParentId,
                b.ValidFrom,
                b.ValidTo,
                b.SortOrder,
                b.CreatedAt,
                b.UpdatedAt,
                Children = b.Children.Select(c => new { c.Id, c.Code, c.NameCs, c.NameEn, c.Level })
            })
            .FirstOrDefaultAsync();
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<object>> GetByCode(string code)
    {
        var item = await _context.BuildingClassifications
            .Where(b => b.Code == code)
            .Select(b => new
            {
                b.Id,
                b.Code,
                b.NameCs,
                b.NameEn,
                b.DescriptionCs,
                b.DescriptionEn,
                b.Level,
                b.ParentId,
                b.ValidFrom,
                b.ValidTo,
                b.SortOrder,
                b.CreatedAt,
                b.UpdatedAt,
                Children = b.Children.Select(c => new { c.Id, c.Code, c.NameCs, c.NameEn, c.Level })
            })
            .FirstOrDefaultAsync();
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpGet("tree")]
    public async Task<ActionResult<IEnumerable<object>>> GetTree()
    {
        var items = await _context.BuildingClassifications
            .Where(b => b.ParentId == null)
            .OrderBy(b => b.SortOrder)
            .ThenBy(b => b.Code)
            .Select(b => new
            {
                b.Id,
                b.Code,
                b.NameCs,
                b.NameEn,
                b.Level,
                Children = b.Children.OrderBy(c => c.SortOrder).ThenBy(c => c.Code).Select(c => new
                {
                    c.Id,
                    c.Code,
                    c.NameCs,
                    c.NameEn,
                    c.Level,
                    Children = c.Children.OrderBy(d => d.SortOrder).ThenBy(d => d.Code).Select(d => new
                    {
                        d.Id,
                        d.Code,
                        d.NameCs,
                        d.NameEn,
                        d.Level,
                        Children = d.Children.OrderBy(e => e.SortOrder).ThenBy(e => e.Code).Select(e => new
                        {
                            e.Id,
                            e.Code,
                            e.NameCs,
                            e.NameEn,
                            e.Level
                        })
                    })
                })
            })
            .ToListAsync();
        return Ok(items);
    }
}
