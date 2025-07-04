namespace FastighetProjekt.Repositories.User;

public interface IUserRepository
{
    Task<IEnumerable<Models.Models.User.User>> GetAllUsersAsync();
    Task<Models.Models.User.User?> GetUserByIdAsync(int id);
    Task<Models.Models.User.User> CreateUserAsync(Models.Models.User.User user);
    Task<Models.Models.User.User> UpdateUserAsync(Models.Models.User.User user);
    Task<bool> DeleteUserAsync(int id);
}