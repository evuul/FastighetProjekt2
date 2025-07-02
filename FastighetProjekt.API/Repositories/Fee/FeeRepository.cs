using FastighetProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Repositories.Fee;

public class FeeRepository : IFeeRepository
{
    private readonly FastighetProjektDbContext _context;

    public FeeRepository(FastighetProjektDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Models.Models.Fee.Fee>> GetAllFeesAsync()
    {
        return await _context.Fees
            .Include(f => f.Apartment)
            .ToListAsync();
    }

    public async Task<Models.Models.Fee.Fee?> GetFeeByIdAsync(int id)
    {
        return await _context.Fees
            .Include(f => f.Apartment)
            .FirstOrDefaultAsync(f => f.FeeId == id);
    }

    public async Task AddFeeAsync(Models.Models.Fee.Fee fee)
    {
        await _context.Fees.AddAsync(fee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFeeAsync(Models.Models.Fee.Fee fee)
    {
        _context.Entry(fee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFeeAsync(int id)
    {
        var fee = await _context.Fees.FindAsync(id);
        if (fee != null)
        {
            _context.Fees.Remove(fee);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> FeeExistsAsync(int id)
    {
        return await _context.Fees.AnyAsync(f => f.FeeId == id);
    }
}