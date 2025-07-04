using FastighetProjekt.Data;
using FastighetProjekt.Models.Models.Property;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Repositories.Property
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly FastighetProjektDbContext _context;

        public PropertyRepository(FastighetProjektDbContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Models.Property.Property>> GetAllAsync()
        {
            return await _context.Properties
                .Include(p => p.Apartments)
                .ToListAsync();
        }

        public async Task<Models.Models.Property.Property?> GetByIdAsync(int id)
        {
            return await _context.Properties
                .Include(p => p.Apartments)
                .FirstOrDefaultAsync(p => p.PropertyId == id);
        }

        public async Task<Models.Models.Property.Property> CreateAsync(Models.Models.Property.Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<Models.Models.Property.Property?> UpdateAsync(Models.Models.Property.Property property)
        {
            var existing = await _context.Properties.FindAsync(property.PropertyId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(property);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return false;

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}