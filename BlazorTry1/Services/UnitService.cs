using BlazorTry1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorTry1.Services;

public class UnitService(Context db) {
    public async Task<Unit?> GetByIdAsync(int id) {
        return await db.Units.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Unit?> GetByIdWithMembersAsync(int id) {
        return await db.Units
            .Include(u => u.Users)
            .FirstOrDefaultAsync(u => u.Id == id);
    }


    public async Task<List<Unit>> GetAllAsync(string? search = null, CancellationToken cancellationToken = default) {
        var q = db.Units
            .AsNoTracking()
            .Include(u => u.Users)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search)) {
            search = search.Trim().ToLower();
            q = q.Where(u => u.Name.ToLower().Contains(search) ||
                             u.Abbreviation != null && u.Abbreviation.ToLower().Contains(search));
        }

        return await q
            .OrderBy(u => u.Name)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Unit> CreateAsync(string name, string? abbreviation, string? description) {
        var unit = new Unit {
            Name = name.Trim(),
            Abbreviation = string.IsNullOrWhiteSpace(abbreviation) ? null : abbreviation.Trim(),
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim()
        };
        db.Units.Add(unit);
        await db.SaveChangesAsync();
        return unit;
    }

    public async Task<Unit> UpdateAsync(int id, string name, string? abbreviation, string? description) {
        var unit = await db.Units.FirstAsync(u => u.Id == id);
        unit.Name = name.Trim();
        unit.Abbreviation = string.IsNullOrWhiteSpace(abbreviation) ? null : abbreviation.Trim();
        unit.Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        await db.SaveChangesAsync();
        return unit;
    }

    public async Task<int> DeleteAsync(int id) {
        var deleted = await db.Units.Where(u => u.Id == id).ExecuteDeleteAsync();
        return deleted;
    }

    public async Task<bool> NameExistsAsync(string name, int? excludeId = null) {
        var q = db.Units.AsNoTracking().Where(u => u.Name == name);
        if (excludeId is not null) q = q.Where(u => u.Id != excludeId.Value);
        return await q.AnyAsync();
    }

    public async Task<bool> AbbreviationExistsAsync(string? abbreviation, int? excludeId = null) {
        if (string.IsNullOrWhiteSpace(abbreviation))
            return false;
        var abbr = abbreviation.Trim();
        var q = db.Units.AsNoTracking().Where(u => u.Abbreviation == abbr);
        if (excludeId is not null) q = q.Where(u => u.Id != excludeId.Value);
        return await q.AnyAsync();
    }
    public async Task<bool> UnitExists(int id) {
        return await db.Units.AnyAsync(u => u.Id == id);
    }
}