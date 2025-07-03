namespace FastighetProjekt.Repositories.Announcement;

public interface IAnnouncementRepository
{
    Task<List<Models.Models.Announcement.Announcement>> GetAllAsync();
    Task<Models.Models.Announcement.Announcement?> GetByIdAsync(int id);
    Task AddAsync(Models.Models.Announcement.Announcement announcement);
    Task UpdateAsync(Models.Models.Announcement.Announcement announcement);
    Task DeleteAsync(int id);
}