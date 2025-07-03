using FastighetProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Repositories.Announcement;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly FastighetProjektDbContext _context;
    
    public AnnouncementRepository(FastighetProjektDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Models.Models.Announcement.Announcement>> GetAllAsync()
    {
        return await _context.Announcements.ToListAsync();
    }
    
    public async Task<Models.Models.Announcement.Announcement?> GetByIdAsync(int id)
    {
        return await _context.Announcements
            .FirstOrDefaultAsync(a => a.AnnouncementId == id);
    }
    
    public async Task AddAsync(Models.Models.Announcement.Announcement announcement)
    {
        await _context.Announcements.AddAsync(announcement);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Models.Models.Announcement.Announcement announcement)
    {
        _context.Entry(announcement).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var announcement = await _context.Announcements.FindAsync(id);
        if (announcement != null)
        {
            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
        }
    }
}