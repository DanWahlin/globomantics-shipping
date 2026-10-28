namespace Globomantics.Shipping.Shipments;

/// <summary>
/// Keeps shipments in memory. Restarting the app resets everything to the sample data.
/// </summary>
public sealed class ShipmentStore
{
    private readonly Lock _lock = new();
    private readonly List<Shipment> _shipments;
    private readonly TimeProvider _time;
    private int _nextNumber;

    public ShipmentStore(TimeProvider time)
    {
        _time = time;
        _shipments = SampleShipments.Create(time.GetUtcNow());
        _nextNumber = _shipments.Select(s => int.Parse(s.Reference[3..])).DefaultIfEmpty(104200).Max() + 1;
    }

    public IReadOnlyList<Shipment> All()
    {
        lock (_lock)
        {
            return [.. _shipments];
        }
    }

    public Shipment? Find(string reference)
    {
        lock (_lock)
        {
            return _shipments.FirstOrDefault(s =>
                string.Equals(s.Reference, reference, StringComparison.OrdinalIgnoreCase));
        }
    }

    public Shipment Book(string providerId, string serviceCode, Recipient recipient, decimal weightKg)
    {
        lock (_lock)
        {
            var shipment = new Shipment
            {
                Reference = $"GS-{_nextNumber++}",
                ProviderId = providerId,
                ServiceCode = serviceCode,
                Recipient = recipient,
                WeightKg = weightKg,
                BookedAt = _time.GetUtcNow(),
            };

            _shipments.Add(shipment);
            return shipment;
        }
    }
}
