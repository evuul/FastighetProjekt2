namespace FastighetProjekt.Repositories.MaintenanceRequest;

public interface IMaintenanceRequestRepository
{
    Task<IEnumerable<Models.Models.MaintenanceRequest.MaintenanceRequest>> GetAllAsync();
    Task<Models.Models.MaintenanceRequest.MaintenanceRequest?> GetByIdAsync(int id);
    Task<Models.Models.MaintenanceRequest.MaintenanceRequest> CreateAsync(Models.Models.MaintenanceRequest.MaintenanceRequest maintenanceRequest);
    Task<Models.Models.MaintenanceRequest.MaintenanceRequest?> UpdateAsync(Models.Models.MaintenanceRequest.MaintenanceRequest maintenanceRequest);
    Task<bool> DeleteAsync(int id);
}