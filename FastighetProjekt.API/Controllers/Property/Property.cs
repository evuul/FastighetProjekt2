using FastighetProjekt.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Controllers.Property;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly FastighetProjektDbContext _context;

    public PropertyController(FastighetProjektDbContext context)
    {
        _context = context;
    }

    // GET: api/Property
    [HttpGet]
    public async Task<IActionResult> GetProperties()
    {
        var properties = await _context.Properties
            .Select(p => new
            {
                p.PropertyId,
                p.Name,
                p.Adress,
                p.City,
                p.ZipCode,
                ApartmentCount = p.Apartments.Count
            })
            .ToListAsync();

        return Ok(properties);
    }

    // GET: api/Property/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProperty(int id)
    {
        var property = await _context.Properties
            .Include(p => p.Apartments)
            .FirstOrDefaultAsync(p => p.PropertyId == id);

        if (property == null)
            return NotFound();

        return Ok(new
        {
            property.PropertyId,
            property.Name,
            property.Adress,
            property.City,
            property.ZipCode,
            Apartments = property.Apartments.Select(a => new
            {
                a.ApartmentId,
                a.ApartmentNumber,
                a.Floor,
                a.Rooms,
                a.Area,
                a.Rent
            })
        });
    }

    // POST: api/Property
    [HttpPost]
    public async Task<IActionResult> CreateProperty([FromBody] Models.Models.Property.Property property)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Properties.Add(property);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProperty), new { id = property.PropertyId }, property);
    }

    // PUT: api/Property/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProperty(int id, [FromBody] Models.Models.Property.Property property)
    {
        if (id != property.PropertyId)
            return BadRequest();

        _context.Entry(property).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Properties.Any(p => p.PropertyId == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Property/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProperty(int id)
    {
        var property = await _context.Properties.FindAsync(id);
        if (property == null)
            return NotFound();

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}