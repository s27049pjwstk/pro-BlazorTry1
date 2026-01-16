namespace MilsimManager.Extensions;

public static class DateTimeExtensions {
    public static string TimeAgo(this DateTime date) {
        var span = DateTime.Now - date;
        if (span.TotalHours < 1)
            return $"{(int)span.TotalMinutes} min ago";
        switch (span.TotalDays) {
            case < 1: return $"{(int)span.TotalHours} h ago";
            case < 2: return $"{(int)span.TotalDays} day ago";
            case < 60: return $"{(int)span.TotalDays} days ago";
            case < 365: return $"{(int)(span.TotalDays / 30)} months ago";
            default: return $"{(int)(span.TotalDays / 365)} years ago";
        }
    }
    private static long ToUnixTimestampSeconds(this DateTime date) => new DateTimeOffset(date).ToUnixTimeSeconds();
    public static string ToDiscordTimestamp(this DateTime date) => $"<t:{date.ToUnixTimestampSeconds().ToString()}>";
}