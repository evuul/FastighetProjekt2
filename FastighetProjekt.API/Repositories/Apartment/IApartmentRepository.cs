namespace FastighetProjekt.Repositories.Apartment;

public interface IApartmentRepository
{
    Task<List<Models.Models.Apartment.Apartment>> GetAllAsync();
    Task<Models.Models.Apartment.Apartment?> GetByIdAsync(int id);
    Task<Models.Models.Apartment.Apartment?> CreateAsync(Models.Models.Apartment.Apartment apartment);
    Task<Models.Models.Apartment.Apartment?> UpdateAsync(Models.Models.Apartment.Apartment apartment);
    Task<bool> DeleteAsync(int id);
}