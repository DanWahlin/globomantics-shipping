using System.Globalization;

namespace Globomantics.Shipping;

public static class Formatting
{
    public static string TimeAgo(DateTimeOffset when, DateTimeOffset now)
    {
        var elapsed = now - when;

        return elapsed switch
        {
            { TotalMinutes: < 1 } => "just now",
            { TotalHours: < 1 } => $"{(int)elapsed.TotalMinutes} min ago",
            { TotalHours: < 24 } => $"{(int)elapsed.TotalHours} h ago",
            { TotalDays: < 2 } => "yesterday",
            _ => $"{(int)elapsed.TotalDays} days ago",
        };
    }

    public static string Timestamp(DateTimeOffset when) =>
        when.ToLocalTime().ToString("d MMM, HH:mm", CultureInfo.InvariantCulture);

    public static string Weight(decimal kg) =>
        $"{kg.ToString("0.0", CultureInfo.InvariantCulture)} kg";

    public static string TransitTime(int days) => days switch
    {
        0 => "Same day",
        1 => "1 day",
        _ => $"{days} days",
    };
}
