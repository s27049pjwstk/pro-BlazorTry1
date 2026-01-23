using MilsimManager.Models;

namespace MilsimManager.Services;

public interface IUnitService {
    Task<Unit?> GetByIdAsync(int id);
    Task<Unit?> GetByIdWithMembersAsync(int id);
    Task<List<Unit>> GetAllAsync(string? search = null, CancellationToken cancellationToken = default);
    Task<Unit> CreateAsync(string name, string? abbreviation, string? description);
    Task<Unit> UpdateAsync(int id, uint version, string name, string? abbreviation, string? description);
    Task<int> DeleteAsync(int id);
    Task<bool> NameExistsAsync(string name, int? excludeId = null);
    Task<bool> AbbreviationExistsAsync(string? abbreviation, int? excludeId = null);
    Task<bool> UnitExists(int id);
}