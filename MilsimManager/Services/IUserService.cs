using MilsimManager.Models;

namespace MilsimManager.Services;

public interface IUserService {
    Task<User?> GetByIdAsync(int id);
    Task<List<User>> GetAllAsync(string? search = null);
    Task<bool> UserExists(int id);
    Task<User> AssignUserAsync(int userId, int? unitId, string? unitRole);

}