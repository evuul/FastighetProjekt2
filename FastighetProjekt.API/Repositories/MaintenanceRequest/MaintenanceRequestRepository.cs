using FastighetProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Repositories.MaintenanceRequest;

public class MaintenanceRequestRepository : IMaintenanceRequestRepository
{
    private readonly FastighetProjektDbContext _context;

    public MaintenanceRequestRepository(FastighetProjektDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Models.MaintenanceRequest.MaintenanceRequest>> GetAllAsync()
    {
        return await _context.MaintenanceRequests.ToListAsync();
    }

    public async Task<Models.Models.MaintenanceRequest.MaintenanceRequest?> GetByIdAsync(int id)
    {
        return await _context.MaintenanceRequests.FindAsync(id);
    }

    public async Task<Models.Models.MaintenanceRequest.MaintenanceRequest> CreateAsync(Models.Models.MaintenanceRequest.MaintenanceRequest maintenanceRequest)
    {
        _context.MaintenanceRequests.Add(maintenanceRequest);
        await _context.SaveChangesAsync();
        return maintenanceRequest;
    }

    public async Task<Models.Models.MaintenanceRequest.MaintenanceRequest?> UpdateAsync(Models.Models.MaintenanceRequest.MaintenanceRequest maintenanceRequest)
    {
        var existing = await GetByIdAsync(maintenanceRequest.MaintenanceRequestId);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(maintenanceRequest);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var maintenanceRequest = await GetByIdAsync(id);
        if (maintenanceRequest == null) return false;

        _context.MaintenanceRequests.Remove(maintenanceRequest);
        await _context.SaveChangesAsync();
        return true;
    }
    
}