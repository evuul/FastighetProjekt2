namespace FastighetProjekt.Repositories.Fee;

public interface IFeeRepository
{
    Task<IEnumerable<Models.Models.Fee.Fee>> GetAllFeesAsync();
    Task<Models.Models.Fee.Fee?> GetFeeByIdAsync(int id);
    Task AddFeeAsync(Models.Models.Fee.Fee fee);
    Task UpdateFeeAsync(Models.Models.Fee.Fee fee);
    Task DeleteFeeAsync(int id);
    Task<bool> FeeExistsAsync(int id);
}