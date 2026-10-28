namespace Globomantics.Shipping.Shipments;

/// <summary>
/// The shipments Globomantics Shipping starts with. Times are relative to startup so the data always looks current.
/// The Havsgren Lines shipments were booked during the pilot, before Havsgren Lines was added to Globomantics Shipping.
/// </summary>
internal static class SampleShipments
{
    public static List<Shipment> Create(DateTimeOffset now) =>
    [
        Build(now, "GS-104201", "harbor-post", "HP-ECO", new("Emily Williams", "Exempelgatan 7", "582 22", "Linköping"), 2.4m,
            (98, "PRE_ADVICE", "Gothenburg"),
            (90, "ACCEPTED", "Gothenburg terminal"),
            (70, "IN_TRANSIT", "Jönköping depot"),
            (30, "ARRIVED_AT_DEPOT", "Linköping depot"),
            (8, "OUT_FOR_DELIVERY", "Linköping"),
            (4, "DELIVERED", "Linköping")),

        Build(now, "GS-104202", "harbor-post", "HP-PRI", new("James Brown", "Exempelvägen 31", "753 21", "Uppsala"), 1.1m,
            (50, "PRE_ADVICE", "Gothenburg"),
            (44, "ACCEPTED", "Gothenburg terminal"),
            (20, "IN_TRANSIT", "Örebro depot")),

        Build(now, "GS-104203", "harbor-post", "HP-ECO", new("Olivia Miller", "Exempelgatan 40", "211 43", "Malmö"), 5.6m,
            (120, "PRE_ADVICE", "Gothenburg"),
            (110, "ACCEPTED", "Gothenburg terminal"),
            (96, "IN_TRANSIT", "Halmstad depot"),
            (50, "ARRIVED_AT_DEPOT", "Malmö depot"),
            (27, "OUT_FOR_DELIVERY", "Malmö"),
            (22, "NOT_DELIVERED", "Malmö")),

        Build(now, "GS-104204", "harbor-post", "HP-PRI", new("Liam Anderson", "Exempeltorget 18", "852 30", "Sundsvall"), 3.0m,
            (75, "PRE_ADVICE", "Gothenburg"),
            (70, "ACCEPTED", "Gothenburg terminal"),
            (52, "IN_TRANSIT", "Gävle depot"),
            (40, "HELD", "Sundsvall depot")),

        Build(now, "GS-104205", "harbor-post", "HP-EXP", new("Sophia Lewis", "Exempelgatan 22", "413 04", "Gothenburg"), 1.2m,
            (3, "PRE_ADVICE", "Gothenburg"),
            (2.6, "ACCEPTED", "Gothenburg terminal"),
            (2.2, "IN_TRANSIT", "Gothenburg"),
            (1.6, "OUT_FOR_DELIVERY", "Gothenburg"),
            (1.1, "DELIVERED", "Gothenburg")),

        Build(now, "GS-104206", "harbor-post", "HP-PRI", new("Benjamin Thomas", "Exempelvägen 2", "411 25", "Gothenburg"), 4.2m,
            (6, "PRE_ADVICE", "Gothenburg"),
            (5, "ACCEPTED", "Gothenburg terminal"),
            (4.5, "IN_TRANSIT", "Gothenburg"),
            (0.5, "OUT_FOR_DELIVERY", "Gothenburg")),

        Build(now, "GS-104207", "harbor-post", "HP-PRI", new("Ava Hall", "Exempeltorget 6", "411 18", "Gothenburg"), 0.9m,
            (30, "PRE_ADVICE", "Gothenburg"),
            (29, "ACCEPTED", "Gothenburg terminal"),
            (27, "OUT_FOR_DELIVERY", "Gothenburg"),
            (26, "NOT_DELIVERED", "Gothenburg"),
            (6, "RETURNED", "Gothenburg terminal")),

        Build(now, "GS-104208", "havsgren-lines", "HVL-STD", new("Ethan Harris", "Exempelvägen 64", "113 59", "Stockholm"), 1.8m,
            (52, "MANIFESTED", "Gothenburg"),
            (47, "COLLECTED", "Gothenburg warehouse"),
            (40, "AT_HUB", "Havsgren Lines terminal Jönköping"),
            (30, "IN_LINEHAUL", "Jönköping to Stockholm"),
            (14, "AT_HUB", "Havsgren Lines terminal Stockholm"),
            (3, "WITH_COURIER", "Stockholm")),

        Build(now, "GS-104209", "havsgren-lines", "HVL-NXT", new("Grace Walker", "Exempelgatan 21", "116 46", "Stockholm"), 0.7m,
            (30, "MANIFESTED", "Gothenburg"),
            (26, "COLLECTED", "Gothenburg warehouse"),
            (20, "AT_HUB", "Havsgren Lines terminal Stockholm"),
            (8, "WITH_COURIER", "Stockholm"),
            (6, "DELIVERED_NEIGHBOUR", "Stockholm")),

        Build(now, "GS-104210", "havsgren-lines", "HVL-ECO", new("Lucas Hill", "Exempelvägen 3", "753 20", "Uppsala"), 6.5m,
            (70, "MANIFESTED", "Gothenburg"),
            (66, "COLLECTED", "Gothenburg warehouse"),
            (60, "AT_HUB", "Havsgren Lines terminal Jönköping"),
            (26, "WITH_COURIER", "Uppsala"),
            (24, "ATTEMPT_FAILED", "Uppsala"),
            (4, "IN_LOCKER", "Uppsala Central parcel locker")),

        Build(now, "GS-104211", "havsgren-lines", "HVL-STD", new("Harper Young", "Exempeltorget 8", "211 45", "Malmö"), 2.2m,
            (20, "MANIFESTED", "Gothenburg"),
            (15, "COLLECTED", "Gothenburg warehouse"),
            (5, "ADDRESS_QUERY", "Havsgren Lines terminal Malmö")),
    ];

    private static Shipment Build(
        DateTimeOffset now,
        string reference,
        string providerId,
        string serviceCode,
        Recipient recipient,
        decimal weightKg,
        params (double HoursAgo, string Code, string Location)[] events)
    {
        var shipment = new Shipment
        {
            Reference = reference,
            ProviderId = providerId,
            ServiceCode = serviceCode,
            Recipient = recipient,
            WeightKg = weightKg,
            BookedAt = now.AddHours(-events[0].HoursAgo),
        };

        foreach (var (hoursAgo, code, location) in events)
        {
            shipment.Events.Add(new TrackingEvent(now.AddHours(-hoursAgo), code, location));
        }

        return shipment;
    }
}
