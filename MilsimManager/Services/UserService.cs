using Microsoft.EntityFrameworkCore;
using MilsimManager.Models;

namespace MilsimManager.Services;

public class UserService(Context db) : IUserService {
    public async Task<User?> GetByIdAsync(int id) {
        return await db.Users
            .Include(u => u.Rank)
            .Include(u => u.Unit)
            .Include(u => u.LeaveOfAbsences)
            .Include(u => u.StatusLogs)
            .Include(u => u.RankLogs)
            .Include(u => u.UnitAssignmentLogs)
            .Include(u => u.UserAwards).ThenInclude(ua => ua.Award)
            .Include(u => u.UserCertifications).ThenInclude(uc => uc.Certification)
            .Include(u => u.UserAttendances).ThenInclude(ua => ua.Event)
            .FirstOrDefaultAsync(u => u.Id == id);
    }


    public async Task<List<User>> GetAllAsync(string? search = null) {
        var q = db.Users
            .AsNoTracking()
            .Include(u => u.Rank)
            .Include(u => u.Unit)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search)) {
            search = search.Trim().ToLower();
            q = q.Where(u => u.Name.ToLower().Contains(search));
        }

        return await q
            .OrderBy(u => u.Name)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<bool> UserExists(int id) {
        return await db.Users.AnyAsync(u => u.Id == id);
    }
    public async Task<User> AssignUserAsync(int userId, int? unitId, string? unitRole) {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) throw new ArgumentException("User not found", nameof(userId));

        if (unitId is not null) {
            var unitExists = await db.Units.AnyAsync(u => u.Id == unitId.Value);
            if (!unitExists) throw new ArgumentException("Unit not found", nameof(unitId));
            user.UnitId = unitId.Value;
        } else {
            user.UnitId = null;
        }

        user.UnitRole = string.IsNullOrWhiteSpace(unitRole) ? null : unitRole.Trim();

        await db.SaveChangesAsync();
        return user;
    }

}