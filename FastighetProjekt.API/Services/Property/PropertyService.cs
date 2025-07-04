using FastighetProjekt.Models.Models.Property;
using FastighetProjekt.Repositories.Property;

namespace FastighetProjekt.Services.Property
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<IEnumerable<Models.Models.Property.Property>> GetAllAsync()
        {
            return await _propertyRepository.GetAllAsync();
        }

        public async Task<Models.Models.Property.Property?> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _propertyRepository.GetByIdAsync(id);
        }

        public async Task<Models.Models.Property.Property> CreateAsync(Models.Models.Property.Property property)
        {
            if (string.IsNullOrWhiteSpace(property.Name) ||
                string.IsNullOrWhiteSpace(property.Adress))
            {
                throw new ArgumentException("Name and Address are required");
            }

            return await _propertyRepository.CreateAsync(property);
        }

        public async Task<Models.Models.Property.Property?> UpdateAsync(Models.Models.Property.Property property)
        {
            var existing = await _propertyRepository.GetByIdAsync(property.PropertyId);
            if (existing == null)
                return null;

            return await _propertyRepository.UpdateAsync(property);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _propertyRepository.DeleteAsync(id);
        }
    }
}