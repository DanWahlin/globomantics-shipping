namespace Globomantics.Shipping.Shipments;

/// <summary>
/// How fast a service is, in Globomantics Shipping's terms. Providers name their services differently,
/// so each provider service is mapped to one of these.
/// </summary>
public enum ServiceLevel
{
    Economy,
    Standard,
    Express,
    SameDay,
}

public static class ServiceLevelExtensions
{
    public static string DisplayName(this ServiceLevel level) => level switch
    {
        ServiceLevel.SameDay => "Same day",
        _ => level.ToString(),
    };
}
