using FastighetProjekt.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Controllers.Apartment;

[ApiController]
[Route("api/[controller]")]
public class ApartmentController : ControllerBase
{
    private readonly FastighetProjektDbContext _context;

    public ApartmentController(FastighetProjektDbContext context)
    {
        _context = context;
    }

    // GET: api/Apartment
    [HttpGet]
    public async Task<IActionResult> GetApartments()
    {
        var apartments = await _context.Apartments
            .Include(a => a.Property)
            .Select(a => new
            {
                a.ApartmentId,
                a.ApartmentNumber,
                a.Floor,
                a.Rooms,
                a.Area,
                a.Rent,
                a.PropertyId,
                PropertyName = a.Property.Name
            })
            .ToListAsync();

        return Ok(apartments);
    }

    // GET: api/Apartment/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetApartment(int id)
    {
        var apartment = await _context.Apartments
            .Include(a => a.Property)
            .FirstOrDefaultAsync(a => a.ApartmentId == id);

        if (apartment == null)
            return NotFound();

        return Ok(new
        {
            apartment.ApartmentId,
            apartment.ApartmentNumber,
            apartment.Floor,
            apartment.Rooms,
            apartment.Area,
            apartment.Rent,
            apartment.PropertyId,
            PropertyName = apartment.Property.Name
        });
    }

    // POST: api/Apartment
    [HttpPost]
    public async Task<IActionResult> CreateApartment([FromBody] Models.Models.Apartment.Apartment apartment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Apartments.Add(apartment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetApartment), new { id = apartment.ApartmentId }, apartment);
    }

    // PUT: api/Apartment/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApartment(int id, [FromBody] Models.Models.Apartment.Apartment apartment)
    {
        if (id != apartment.ApartmentId)
            return BadRequest();

        _context.Entry(apartment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Apartments.Any(a => a.ApartmentId == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Apartment/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApartment(int id)
    {
        var apartment = await _context.Apartments.FindAsync(id);
        if (apartment == null)
            return NotFound();

        _context.Apartments.Remove(apartment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}