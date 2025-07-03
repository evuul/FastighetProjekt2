using FastighetProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Repositories.Apartment;

public class ApartmentRepository : IApartmentRepository
{
    private readonly FastighetProjektDbContext _context;

    public ApartmentRepository(FastighetProjektDbContext context)
    {
        _context = context;
    }

    public async Task<List<Models.Models.Apartment.Apartment>> GetAllAsync()
    {
        return await _context.Apartments.ToListAsync();
    }

    public async Task<Models.Models.Apartment.Apartment?> GetByIdAsync(int id)
    {
        return await _context.Apartments.FindAsync(id);
    }

    public async Task<Models.Models.Apartment.Apartment?> CreateAsync(Models.Models.Apartment.Apartment apartment)
    {
        _context.Apartments.Add(apartment);
        await _context.SaveChangesAsync();
        return apartment;
    }

    public async Task<Models.Models.Apartment.Apartment?> UpdateAsync(Models.Models.Apartment.Apartment apartment)
    {
        _context.Apartments.Update(apartment);
        await _context.SaveChangesAsync();
        return apartment;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var apartment = await GetByIdAsync(id);
        if (apartment == null) return false;

        _context.Apartments.Remove(apartment);
        await _context.SaveChangesAsync();
        return true;
    }
    
}