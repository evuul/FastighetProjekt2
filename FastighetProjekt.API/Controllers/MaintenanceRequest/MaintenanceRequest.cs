using FastighetProjekt.Data;
using FastighetProjekt.Models.Models.MaintenanceRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Controllers.MaintenanceRequest;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceRequestController : ControllerBase
{
    private readonly FastighetProjektDbContext _context;

    public MaintenanceRequestController(FastighetProjektDbContext context)
    {
        _context = context;
    }

    // GET: api/MaintenanceRequest
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var requests = await _context.MaintenanceRequests
            .Include(m => m.Apartment)
            .Select(m => new 
            {
                m.MaintenanceRequestId,
                m.ApartmentId,
                ApartmentNumber = m.Apartment.ApartmentNumber,
                m.Title,
                m.Description,
                m.CreatedDate,
                m.Status,
                m.AttachmentUrl
            })
            .ToListAsync();

        return Ok(requests);
    }

    // GET: api/MaintenanceRequest/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var request = await _context.MaintenanceRequests
            .Include(m => m.Apartment)
            .FirstOrDefaultAsync(m => m.MaintenanceRequestId == id);

        if (request == null) return NotFound();

        return Ok(new 
        {
            request.MaintenanceRequestId,
            request.ApartmentId,
            ApartmentNumber = request.Apartment.ApartmentNumber,
            request.Title,
            request.Description,
            request.CreatedDate,
            request.Status,
            request.AttachmentUrl
        });
    }

    // POST: api/MaintenanceRequest
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Models.Models.MaintenanceRequest.MaintenanceRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _context.MaintenanceRequests.Add(request);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = request.MaintenanceRequestId }, request);
    }

    // PUT: api/MaintenanceRequest/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Models.Models.MaintenanceRequest.MaintenanceRequest request)
    {
        if (id != request.MaintenanceRequestId)
            return BadRequest();

        _context.Entry(request).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.MaintenanceRequests.Any(m => m.MaintenanceRequestId == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/MaintenanceRequest/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var request = await _context.MaintenanceRequests.FindAsync(id);
        if (request == null) return NotFound();

        _context.MaintenanceRequests.Remove(request);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}