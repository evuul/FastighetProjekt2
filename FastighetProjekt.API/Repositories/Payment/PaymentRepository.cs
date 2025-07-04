using FastighetProjekt.Data;
using Microsoft.EntityFrameworkCore;
using FastighetProjekt.Models.Models.Payment;

namespace FastighetProjekt.Repositories.Payment
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly FastighetProjektDbContext _context;

        public PaymentRepository(FastighetProjektDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Models.Payment.Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Fee)
                .ToListAsync();
        }

        public async Task<Models.Models.Payment.Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.Fee)
                .FirstOrDefaultAsync(p => p.PaymentId == id);
        }

        public async Task<Models.Models.Payment.Payment> CreateAsync(Models.Models.Payment.Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Models.Models.Payment.Payment?> UpdateAsync(Models.Models.Payment.Payment payment)
        {
            var existing = await _context.Payments.FindAsync(payment.PaymentId);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(payment);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}