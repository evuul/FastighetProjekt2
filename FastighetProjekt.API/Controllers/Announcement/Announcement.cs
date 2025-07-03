using FastighetProjekt.Models.Models.Announcement;
using FastighetProjekt.Repositories.Announcement;
using Microsoft.AspNetCore.Mvc;

namespace FastighetProjekt.Controllers.Announcement;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementRepository _announcementRepository;

    public AnnouncementController(IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    // GET: api/Announcement
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var announcements = await _announcementRepository.GetAllAsync();
        return Ok(announcements);
    }

    // GET: api/Announcement/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var announcement = await _announcementRepository.GetByIdAsync(id);
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

        await _announcementRepository.AddAsync(announcement);
        return CreatedAtAction(nameof(GetById), new { id = announcement.AnnouncementId }, announcement);
    }

    // PUT: api/Announcement/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Models.Models.Announcement.Announcement updatedAnnouncement)
    {
        if (id != updatedAnnouncement.AnnouncementId)
            return BadRequest("ID mismatch");

        await _announcementRepository.UpdateAsync(updatedAnnouncement);
        return NoContent();
    }

    // DELETE: api/Announcement/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _announcementRepository.DeleteAsync(id);
        return NoContent();
    }
}