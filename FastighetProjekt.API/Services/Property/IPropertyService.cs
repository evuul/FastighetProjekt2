namespace FastighetProjekt.Services.Property;

public interface IPropertyService
{
    Task<IEnumerable<Models.Models.Property.Property>> GetAllAsync();
    Task<Models.Models.Property.Property?> GetByIdAsync(int id);
    Task<Models.Models.Property.Property> CreateAsync(Models.Models.Property.Property property);
    Task<Models.Models.Property.Property?> UpdateAsync(Models.Models.Property.Property property);
    Task<bool> DeleteAsync(int id);
}