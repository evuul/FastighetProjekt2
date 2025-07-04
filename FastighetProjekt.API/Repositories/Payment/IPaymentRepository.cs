namespace FastighetProjekt.Repositories.Payment;

public interface IPaymentRepository
{
    Task<IEnumerable<Models.Models.Payment.Payment>> GetAllAsync();
    Task<Models.Models.Payment.Payment?> GetByIdAsync(int id);
    Task<Models.Models.Payment.Payment> CreateAsync(Models.Models.Payment.Payment payment);
    Task<Models.Models.Payment.Payment?> UpdateAsync(Models.Models.Payment.Payment payment);
    Task<bool> DeleteAsync(int id);
}