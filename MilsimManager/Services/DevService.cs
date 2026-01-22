using Microsoft.EntityFrameworkCore;
using MilsimManager.Models;

namespace MilsimManager.Services;

public class DevService(IDbContextFactory<Context> dbFactory) : IDevService {
    public async Task ResetAsync() {
        await using var db = await dbFactory.CreateDbContextAsync();

        await db.Database.EnsureDeletedAsync();
        await db.Database.MigrateAsync();

        await SeedExampleDataAsync(db);
    }

    private static async Task SeedExampleDataAsync(Context db) {
        if (await db.Users.AnyAsync()) return;

        var unitCmd = new Unit { Name = "Command", Abbreviation = "HQ", Description = "Command and admin." };
        var unitAlpha = new Unit { Name = "Alpha", Abbreviation = "A", Description = "Infantry squad." };
        db.Units.AddRange(unitCmd, unitAlpha);

        var rankPvt = new Rank { Name = "Private", Code = "PVT", SortOrder = 1 };
        var rankCpl = new Rank { Name = "Corporal", Code = "CPL", SortOrder = 2 };
        var rankSgt = new Rank { Name = "Sergeant", Code = "SGT", SortOrder = 3 };
        db.Ranks.AddRange(rankPvt, rankCpl, rankSgt);

        var event1 = new Event {
            Name = "Operation Frostbite", Description = "Night raid training.", Date = DateTime.UtcNow.Date.AddDays(7)
        };
        var event2 = new Event {
            Name = "Medical School Day 3", Description = null, Date = DateTime.UtcNow.Date.AddDays(14)
        };
        db.Events.AddRange(event1, event2);

        await db.SaveChangesAsync();

        var u1 = new User {
            Name = "J. Doe", Active = true, Rank = rankPvt, UnitId = unitCmd.Id, UnitRole = "Admin"
        };
        var u2 = new User {
            Name = "T. Able", Active = true, Rank = rankCpl, UnitId = unitAlpha.Id, UnitRole = "Squad Leader"
        };
        var u3 = new User {
            Name = "M. Marco", Active = true, Rank = rankPvt, UnitId = null, UnitRole = null
        };
        db.Users.AddRange(u1, u2, u3);

        await db.SaveChangesAsync();
    }
}