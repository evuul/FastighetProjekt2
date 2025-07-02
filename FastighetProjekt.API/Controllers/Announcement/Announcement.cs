using FastighetProjekt.Data;
using FastighetProjekt.Models.Models.Announcement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Controllers.Announcement;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly FastighetProjektDbContext _context;

    public AnnouncementController(FastighetProjektDbContext context)
    {
        _context = context;
    }

    // GET: api/Announcement
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var announcements = await _context.Announcements
            .OrderByDescending(a => a.PublishedDate)
            .ToListAsync();

        return Ok(announcements);
    }

    // GET: api/Announcement/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        if (announcement == null)
            return NotFound();

        return Ok(announcement);
    }

    // POST: api/Announcement
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Models.Models.Announcement.Announcement announcement)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = announcement.AnnouncementId }, announcement);
    }

    // PUT: api/Announcement/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Models.Models.Announcement.Announcement announcement)
    {
        if (id != announcement.AnnouncementId)
            return BadRequest();

        _context.Entry(announcement).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Announcements.Any(a => a.AnnouncementId == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/Announcement/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        if (announcement == null)
            return NotFound();

        _context.Announcements.Remove(announcement);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}